using Godot;
using System;

public abstract partial class Cooker : Container
{
	protected bool Cook = false;
	[Export] protected GpuParticles3D Particles;
	[Export] protected ProgressBar ProgressBar;

	public override void _Ready()
	{
		base._Ready();
		ProgressBar.Visible = false;
		ProgressBar.Value = 0;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		Cooking(delta);
	}

	protected abstract void Cooking(double time);

	public void StartCooking()
	{
		if (Foods.Count <= 0)
			return;
		Cook = true;
		Particles.Emitting = true;
	}

	public void StopCooking()
	{
		Cook = false;
		Particles.Emitting = false;
		ProgressBar.Visible = false;
	}
}
