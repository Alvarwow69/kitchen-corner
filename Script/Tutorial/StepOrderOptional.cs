using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class StepOrderOptional : Target
{
	[Export] private int _activeStep;
	public override void EnableTarget()
	{
		FoodEvent.OnFoodBurned += OnFoodBurned;
		PlateEvent.OnFoodAddedPlateEvent += OnFoodAddedPlateEvent;
	}

	public override void DisableTarget()
	{
		FoodEvent.OnFoodBurned -= OnFoodBurned;
		PlateEvent.OnFoodAddedPlateEvent -= OnFoodAddedPlateEvent;
	}

	private void OnFoodBurned(Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() != _activeStep)
			return;
	}

	private void OnFoodAddedPlateEvent(Plate plate, Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() != _activeStep)
			return;
	}
}
