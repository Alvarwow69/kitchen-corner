using Godot;
using System;

public partial class TutoEmpty : Target
{
	#region Properties

	[Export] private double _delay = 0.5;

	private bool _activate;
	private double _currentTimer;

	#endregion

	#region Methodes

	public override void EnableTarget()
	{
		_activate = false;
		Visible = true;
		foreach (var child in GetChildren())
			(child as StepDetails)?.EnableDetail();
	}

	public override void DisableTarget()
	{
		Visible = false;
		foreach (var child in GetChildren())
			(child as StepDetails)?.DisableDetail();
	}

	#endregion
}
