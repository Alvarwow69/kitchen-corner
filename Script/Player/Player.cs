using System.Diagnostics;
using Godot;

public partial class Player : CharacterBody3D
{
    #region Properties

    [Export] public float NormalSpeed = 5.0f;
    [Export] public float DashDuration = .2f;
    [Export] public float DashSpeed = 10.0f;
    [Export] public int PlayerNumber { get; set; } = -1;
    [Export] private bool _invertX = false;
    [Export] private bool _invertY = false;
    [Export] private bool _enabled = false;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Dash _dash;
    private RayCast3D _raycast;
    private Node3D _pivot;
    private Interactable _selectionInteractable = null;
    private RigidInteractable _interactable = null;
    private Node3D _anchor;
    private bool _freeze = false;
    public bool IsProcessAction { get; set; } = true;

    #endregion

    #region Methodes

    public override void _Ready()
    {
        _dash = GetNode<Dash>("Dash");
        _raycast = GetNode<RayCast3D>("Pivot/RayCast3D");
        _pivot = GetNode<Node3D>("Pivot");
        _anchor = GetNode<Node3D>("Pivot/Anchor");
    }

    public override void _Process(double delta)
    {
        if (!_enabled)
            return;
        ProcessInteractable();
        ProcessAction();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_enabled)
            return;
        MovePlayer(delta);
    }

    private void ProcessInteractable()
    {
        if (_raycast.IsColliding() && (GameManager.GetGameState() == GameManager.GameState.InGame ||
                                       SelectionManager.GetGameState() == SelectionManager.SelectionState.Waiting))
        {
            if ((_raycast.GetCollider() as Node3D)?.GetParent() is Interactable)
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
        if (GameManager.GetGameState() != GameManager.GameState.InGame && SelectionManager.GetGameState() != SelectionManager.SelectionState.Waiting)
            return;
        if (Input.IsActionJustPressed("player" + PlayerNumber + "_grab"))
        {
            if (_interactable != null)
            {
                if (_selectionInteractable is Ingredient && _interactable is Ingredient)
                {
                    if (_selectionInteractable is Container)
                        (_selectionInteractable as Container)?.AddFood(_interactable as Ingredient);
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
            _interactable?.Throw(this, 30, -_pivot.GlobalTransform.Basis.Z);
            _selectionInteractable?.EndProcessAction(this);
        }

        if (Input.IsActionPressed("player" + PlayerNumber + "_action"))
            if (_interactable == null)
                _selectionInteractable?.ProcessAction(this);
    }

    private void MovePlayer(double delta)
    {
        Vector3 velocity = Velocity;

        if (GameManager.GetGameState() != GameManager.GameState.InGame && SelectionManager.GetGameState() != SelectionManager.SelectionState.Waiting)
            return;
        if (!IsOnFloor())
            velocity.Y -= _gravity * (float)delta;

        if (Input.IsActionJustPressed("player" + PlayerNumber + "_dash"))
            _dash.StartDash(DashDuration);

        float speed = _dash.isDashing() ? DashSpeed : NormalSpeed;

        Vector2 inputDir = Input.GetVector("player" + PlayerNumber + "_left", "player" + PlayerNumber + "_right",
            "player" + PlayerNumber + "_forward", "player" + PlayerNumber + "_backward");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X * (_invertX ? -1.0f : 1.0f), 0, inputDir.Y * (_invertY ? -1.0f : 1.0f))).Normalized();
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

    public bool AddInteractable(RigidInteractable interactable)
    {
        if (_interactable is Container)
            return (_interactable as Container).AddFood(interactable as Ingredient);
        _interactable = interactable;
        _interactable.Freeze();
        _interactable.Reparent(_anchor);
        _interactable.GlobalPosition = _anchor.GlobalPosition;
        _interactable.Player = this;
        return true;
    }

    public RigidInteractable RemoveInteractable()
    {
        RigidInteractable tmp = _interactable;

        _interactable.Reparent(GetTree().Root.GetChild(0));
        _interactable = null;
        return tmp;
    }

    public RigidInteractable RemoveFromInteractable()
    {
        RigidInteractable tmp = (_interactable as Container)?.RemoveIngredient();

        tmp.Reparent(GetTree().Root.GetChild(0));
        return tmp;
    }

    public RigidInteractable GetInteractable()
    {
        return _interactable;
    }

    public void EnablePlayer()
    {
        _enabled = true;
        Visible = true;
    }

    public void DisablePlayer()
    {
        _enabled = false;
        Visible = false;
    }

    #endregion

}