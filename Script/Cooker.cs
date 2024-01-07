using Godot;
using System;

public abstract partial class Cooker : Ingredient
{
	protected bool Cook = false;
	[Export] protected GpuParticles3D Particles;

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
	}
}
