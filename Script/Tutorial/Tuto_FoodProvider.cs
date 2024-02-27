using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class Tuto_FoodProvider : VisualTarget
{
	[Export] private string _targetIngredient;
	[Export] private int _targetStep;

	public override void _Ready()
	{
		FoodEvent.OnFoodCreated += OnFoodCreated;
	}

	private void OnFoodCreated(Ingredient ingredient)
	{
		if (ingredient.Name == _targetIngredient && TutorialManager.GetCurrentStep() == _targetStep)
			TargetEvent.PerformTargetReached();
	}
}
