using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepSink : VisualTarget
{
	[Export] private int _targetStep;

	public override void _Ready()
	{
		PlateEvent.OnPlateCleaned += OnPlateCleaned;
	}

	private void OnPlateCleaned(Plate plate)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformTargetReached();
			PlateEvent.OnPlateCleaned -= OnPlateCleaned;
		}
	}
}
