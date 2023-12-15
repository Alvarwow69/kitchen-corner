using Godot;
using System;

public partial class Dash : Node3D
{
	private Timer dashTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		dashTimer = GetNode<Timer>("DashTimer");
	}

	public void StartDash(float duration)
	{
		dashTimer.WaitTime = duration;
		dashTimer.Start();
	}

	public bool isDashing()
	{
		return !dashTimer.IsStopped();
	}
}
