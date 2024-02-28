using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepDirtyPlate : VisualTarget
{
	[Export] private int _targetStep;

	public override void _Ready()
	{
		CounterEvent.OnInteractableRemoved += OnInteractableRemoved;
	}

	private void OnInteractableRemoved(CounterInteractable counter)
	{
		if (counter is not PlateManager || TutorialManager.GetCurrentStep() != _targetStep)
			return;
		TargetEvent.PerformTargetReached();
		CounterEvent.OnInteractableRemoved -= OnInteractableRemoved;
	}
}
