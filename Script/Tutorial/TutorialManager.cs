using Godot;
using System;
using System.Diagnostics;

public partial class TutorialManager : Node
{
	private static int _currentStep = 1;
	private int _maxStep;
	
	public override void _Ready()
	{
		_maxStep = GetChildren().Count;
		TargetEvent.TargetReached += NewStep;
		GetNode<Target>("Step" + AddZero() + _currentStep).EnableTarget();
	}

	public void NewStep()
	{
		GetNode<Target>("Step" + AddZero() + _currentStep).DisableTarget();
		Debug.Print("Step" + AddZero() + _currentStep + ": Disabled");
		_currentStep += 1;
		if (_currentStep > _maxStep)
			return;
		Debug.Print("Step" + AddZero() + _currentStep + ": Enabled");
		GetNode<Target>("Step" + AddZero() + _currentStep).EnableTarget();
	}

	public static int GetCurrentStep()
	{
		return _currentStep;
	}

	private string AddZero()
	{
		return _currentStep < 10 ? "0" : "";
	}

}
