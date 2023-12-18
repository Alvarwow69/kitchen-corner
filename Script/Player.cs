using System.Diagnostics;
using Godot;

public partial class Player : CharacterBody3D
{
	[Export] public const float NormalSpeed = 5.0f;
	[Export] public const float DashDuration = .2f;
	[Export] public const float DashSpeed = 10.0f;
	[Export] public int PlayerNumber { get; set; } = -1;

	private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Dash _dash;
	private RayCast3D _raycast;
	private Node3D _pivot;
	private Interactable _selectionInteractable = null;
	private Interactable _interactable = null;
	private Node3D _anchor;

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
				_selectionInteractable = (_raycast.GetCollider() as Node3D).GetParent() as Interactable;
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
		if (Input.IsActionJustPressed("player" + PlayerNumber + "_action"))
		{
			if (_selectionInteractable != null)
				_selectionInteractable?.PerformAction(this);
			else if (_interactable != null)
				_interactable.Drop(this);

		}

	}

	private void MovePlayer(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (Input.IsActionJustPressed("player" + PlayerNumber + "_dash"))
			_dash.StartDash(DashDuration);

		float Speed = _dash.isDashing() ? DashSpeed : NormalSpeed;

		Vector2 inputDir = Input.GetVector("player" + PlayerNumber + "_left", "player" + PlayerNumber + "_right", "player" + PlayerNumber + "_forward", "player" + PlayerNumber + "_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			_pivot.LookAt(Position + direction, Vector3.Up);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public bool HasInteractable()
	{
		return _interactable != null;
	}

	public void AddInteractable(Interactable interactable)
	{
		_interactable = interactable;
		_interactable.Reparent(_anchor);
		_interactable.GlobalPosition = _anchor.GlobalPosition;
	}

	public Interactable RemoveInteractable()
	{
		Interactable tmp = _interactable;

		_interactable.Reparent(GetTree().Root.GetChild(0));
		_interactable = null;
		return tmp;
	}
}
