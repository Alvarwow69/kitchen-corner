using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class TutoPlateManager : Target
{
	[Export] private int _targetStep;

	public override void EnableTarget()
	{
		PlateEvent.OnDirtyPlateSpawn += OnDirtyPlateSpawn;
	}

	public override void DisableTarget()
	{
		PlateEvent.OnDirtyPlateSpawn -= OnDirtyPlateSpawn;
	}

	private void OnDirtyPlateSpawn(Plate plate)
	{
		if (TutorialManager.GetCurrentStep() == _targetStep)
		{
			TargetEvent.PerformTargetReached();
		}
	}
}
