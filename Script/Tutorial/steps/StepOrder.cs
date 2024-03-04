using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepOrder : VisualTarget
{
	[Export] private int _targetStep;

	public override void _Ready()
	{
		OrderEvent.OnOrderPlaced += OnOrderPlaced;
	}

	private void OnOrderPlaced(Plate plate)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformTargetReached();
			OrderEvent.OnOrderPlaced -= OnOrderPlaced;
		}
	}
}
