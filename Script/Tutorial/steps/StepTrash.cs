using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepTrash : VisualTarget
{
	[Export] private int _targetStep;
	[Export] private int _resetStep;

	public override void _Ready()
	{
		TrashEvent.OnIngredientThrow += OnIngredientThrow;
	}

	private void OnIngredientThrow(Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformResetToStep(_resetStep);
			PlateEvent.OnPlateCleaned -= OnIngredientThrow;
		}
	}
}
