using System.Diagnostics;
using Godot;

public partial class Player : CharacterBody3D
{
    [Export] public float NormalSpeed = 5.0f;
    [Export] public float DashDuration = .2f;
    [Export] public float DashSpeed = 10.0f;
    [Export] public int PlayerNumber { get; set; } = -1;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Dash _dash;
    private RayCast3D _raycast;
    private Node3D _pivot;
    private Interactable _selectionInteractable = null;
    private RigidInteractable _interactable = null;
    private Node3D _anchor;
    private bool _freeze = false;

    public override void _Ready()
    {
        _dash = GetNode<Dash>("Dash");
        _raycast = GetNode<RayCast3D>("Pivot/RayCast3D");
        _pivot = GetNode<Node3D>("Pivot");
        _anchor = GetNode<Node3D>("Pivot/Anchor");
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessInteractable();
        ProcessAction();
        MovePlayer(delta);
    }

    private void ProcessInteractable()
    {
        if (_raycast.IsColliding())
        {
            if ((_raycast.GetCollider() as Node3D).GetParent() is Interactable)
            {
                if (_raycast.GetCollider() == _selectionInteractable)
                    return;
                _selectionInteractable?.HoverExit(this);
                _selectionInteractable = (_raycast.GetCollider() as Node3D)?.GetParent() as Interactable;
                _selectionInteractable?.HoverEnter(this);
            }
        }
        else if (_selectionInteractable != null)
        {
            _selectionInteractable.HoverExit(this);
            _selectionInteractable = null;
        }
    }

    private void ProcessAction()
    {
        if (Input.IsActionJustPressed("player" + PlayerNumber + "_grab"))
        {
            if (_interactable != null)
            {
                if (_selectionInteractable is Ingredient && _interactable is Ingredient)
                {
                    if (_selectionInteractable is Plate || _selectionInteractable is Plate)
                        (_selectionInteractable as Plate)?.AddFood(_interactable as Ingredient);
                    else
                        (_interactable as Ingredient)?.AddFood(_selectionInteractable as Ingredient);
                }
                else if (_selectionInteractable == null)
                    _interactable.Drop(this);
                else
                    _selectionInteractable?.PerformAction(this);
            }
            else if (_selectionInteractable != null)
                _selectionInteractable?.PerformAction(this);
        }

        if (Input.IsActionPressed("player" + PlayerNumber + "_action"))
            _freeze = true;
        else if (Input.IsActionJustReleased("player" + PlayerNumber + "_action"))
        {
            _freeze = false;
            if (_interactable != null)
            {
                if (_interactable is RigidInteractable)
                    (_interactable as RigidInteractable).Throw(this, 30, -_pivot.GlobalTransform.Basis.Z);
                else
                    _interactable.Drop(this);
            }
        }
    }

    private void MovePlayer(double delta)
    {
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
            velocity.Y -= _gravity * (float)delta;

        if (Input.IsActionJustPressed("player" + PlayerNumber + "_dash"))
            _dash.StartDash(DashDuration);

        float speed = _dash.isDashing() ? DashSpeed : NormalSpeed;

        Vector2 inputDir = Input.GetVector("player" + PlayerNumber + "_left", "player" + PlayerNumber + "_right",
            "player" + PlayerNumber + "_forward", "player" + PlayerNumber + "_backward");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * speed * (_freeze ? 0 : 1);
            velocity.Z = direction.Z * speed * (_freeze ? 0 : 1);
            _pivot.LookAt(Position + direction, Vector3.Up);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public bool HasInteractable()
    {
        return _interactable != null;
    }

    public void AddInteractable(RigidInteractable interactable)
    {
        _interactable = interactable;
        _interactable.Reparent(_anchor);
        _interactable.GlobalPosition = _anchor.GlobalPosition;
    }

    public RigidInteractable RemoveInteractable()
    {
        RigidInteractable tmp = _interactable;

        _interactable.Reparent(GetTree().Root.GetChild(0));
        _interactable = null;
        return tmp;
    }
}