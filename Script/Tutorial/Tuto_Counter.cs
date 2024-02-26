using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class Tuto_Counter : VisualTarget
{
	[Export] private int _targetStep;
	[Export] private CounterInteractable _targetCounter;

	public override void _Ready()
	{
		CounterEvent.OnInteractableRemoved += OnFoodCooked;
	}

	private void OnFoodCooked(CounterInteractable counter)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep &&
		    _targetCounter == counter)
		{
			TargetEvent.PerformTargetReached();
			CounterEvent.OnInteractableRemoved -= OnFoodCooked;
		}
	}
}
