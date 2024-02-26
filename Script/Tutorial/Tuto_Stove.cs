using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class Tuto_Stove : VisualTarget
{
	[Export] private int _targetStep;

	public override void _Ready()
	{
		FoodEvent.OnFoodCooked += OnFoodCooked;
	}

	private void OnFoodCooked(Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformTargetReached();
			FoodEvent.OnFoodSliced -= OnFoodCooked;
		}
	}
}
