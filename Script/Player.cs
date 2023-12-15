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
	private Interactable _interactable = null;

	public override void _Ready()
	{
		_dash = GetNode<Dash>("Dash");
		_raycast = GetNode<RayCast3D>("Pivot/RayCast3D");
		_pivot = GetNode<Node3D>("Pivot");
	}

	public override void _PhysicsProcess(double delta)
	{
		ProcessActionableItem();
		ProcessAction();
		MovePlayer(delta);
	}

	private void ProcessActionableItem()
	{
		if (_raycast.IsColliding() && _raycast.GetCollider() is Interactable)
		{
			if (_raycast.GetCollider() == _interactable)
				return;
			_interactable = _raycast.GetCollider() as Interactable;
			_interactable.Select();
			Debug.Print("oueoueoeuoeueoeuoeu");
		}
		else if (_interactable != null)
		{
			_interactable.Reset();
			_interactable = null;
		}
	}

	private void ProcessAction()
	{
		if (Input.IsActionJustPressed("player" + PlayerNumber + "_action") && _interactable != null)
			_interactable.PerformAction();
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
}
