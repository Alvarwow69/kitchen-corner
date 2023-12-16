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
	private SelectionInteractable _selectionInteractable = null;
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
		ProcessActionableItem();
		ProcessAction();
		MovePlayer(delta);
	}

	private void ProcessActionableItem()
	{
		if (_raycast.IsColliding() && _raycast.GetCollider() is SelectionInteractable)
		{
			if (_raycast.GetCollider() == _selectionInteractable)
				return;
			_selectionInteractable?.Reset();
			_selectionInteractable = _raycast.GetCollider() as SelectionInteractable;
			_selectionInteractable?.Select();
		}
		else if (_selectionInteractable != null)
		{
			_selectionInteractable.Reset();
			_selectionInteractable = null;
		}
	}

	private void ProcessAction()
	{
		if (Input.IsActionJustPressed("player" + PlayerNumber + "_action") && _selectionInteractable != null)
			_selectionInteractable.PerformAction(this);
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

		_interactable = null;
		return tmp;
	}
}
