using Godot;

public partial class Player : CharacterBody3D
{
	[Export] public const float NormalSpeed = 5.0f;
	[Export] public const float DashDuration = .2f;
	[Export] public const float DashSpeed = 10.0f;

	private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Dash dash;

	public override void _Ready()
	{
		dash = GetNode<Dash>("Dash");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (Input.IsActionJustPressed("dash"))
		{
			dash.StartDash(DashDuration);
		}

		float Speed = dash.isDashing() ? DashSpeed : NormalSpeed;

		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
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
