using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepCounter : VisualTarget
{
	[Export] private int _targetStep;
	[Export] private CounterInteractable _targetCounter;
	[Export] private bool _add = false;

	public override void _Ready()
	{
		CounterEvent.OnInteractableRemoved += OnInteractableRemoved;
		CounterEvent.OnInteractablePlaced += OnInteractablePlaced;
	}

	private void OnInteractableRemoved(CounterInteractable counter)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep &&
		    _targetCounter == counter && !_add)
		{
			TargetEvent.PerformTargetReached();
		}
	}

	private void OnInteractablePlaced(CounterInteractable counter)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep &&
		    _targetCounter == counter && _add)
		{
			TargetEvent.PerformTargetReached();
		}
	}
}
