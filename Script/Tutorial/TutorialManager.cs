using Godot;
using System;
using System.Diagnostics;
using KitchenCorner.Script.Save;

public sealed partial class TutorialManager : Node
{
	[Export(PropertyHint.File)] private string _selectionScene;

	private static int _currentStep = 1;
	private int _maxStep;
	private string _currentStepName;
	
	public override void _Ready()
	{
		_maxStep = GetChildren().Count;
		TargetEvent.TargetReached += NewStep;
		TargetEvent.OnStartCustomStep += ActiveCustomStep;
		TargetEvent.OnResetToStep += ResetToStep;
		GetNode<Target>("Step" + AddZero() + _currentStep).EnableTarget();
		_currentStepName = "Step" + AddZero() + _currentStep;
	}

	public void NewStep()
	{
		GetNode<Target>(_currentStepName).DisableTarget();
		_currentStep += 1;
		GetNode<Target>("Step" + AddZero() + _currentStep).EnableTarget();
		_currentStepName = "Step" + AddZero() + _currentStep;
		if (_currentStep >= _maxStep)
			TutorialFinished();
	}

	public static int GetCurrentStep()
	{
		return _currentStep;
	}

	public void ResetToStep(int newStep)
	{
		GetNode<Target>(_currentStepName).DisableTarget();
		_currentStep = newStep;
		if (_currentStep >= _maxStep)
		{
			TargetEvent.PerformTutorialFinished();
			return;
		}
		GetNode<Target>("Step" + AddZero() + _currentStep).EnableTarget();
		_currentStepName = "Step" + AddZero() + _currentStep;
	}

	public void ActiveCustomStep(string stepName)
	{
		GetNode<Target>(_currentStepName).DisableTarget();
		GetNode<Target>("Step" + AddZero() + _currentStep + "/" + stepName).EnableTarget();
		_currentStepName = "Step" + AddZero() + _currentStep + "/" + stepName;
	}

	private string AddZero()
	{
		return _currentStep < 10 ? "0" : "";
	}

	private void TutorialFinished()
	{
		TargetEvent.PerformTutorialFinished();
		var saveLevel = GetNode<SaveLevel>("/root/SaveLevel");
		saveLevel.UpdateScore("Tutorial", 300);
		saveLevel.UpdateActivation("Level01", true);
		saveLevel.Save();
		GetTree().ChangeSceneToFile(_selectionScene);
	}

}
