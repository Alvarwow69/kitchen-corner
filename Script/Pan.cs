using Godot;
using System;

public partial class Pan : Ingredient
{
	private bool _cook = false;
	[Export] private GpuParticles3D _particles;

	public override void AddFood(Ingredient ingredient)
	{
		if (ingredient is not CookIngredient || Foods.Count >= 1)
			return;
		base.AddFood(ingredient);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (_cook)
			(Foods[0] as CookIngredient)?.UpdateCookTime(delta);
	}

	public void StartCooking()
	{
		_cook = true;
		_particles.Emitting = true;
	}

	public void StopCooking()
	{
		_cook = false;
		_particles.Emitting = false;
	}
}
