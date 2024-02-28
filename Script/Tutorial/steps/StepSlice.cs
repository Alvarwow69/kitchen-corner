using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepSlice : VisualTarget
{
	[Export] private int _targetStep;

	public override void _Ready()
	{
		FoodEvent.OnFoodSliced += OnFoodSliced;
	}

	private void OnFoodSliced(Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformTargetReached();
			FoodEvent.OnFoodSliced -= OnFoodSliced;
		}
	}
}
