using Godot;
using System;

public delegate void TargetReachedHandler();
public delegate void NewCustomStep(string stepName);
public delegate void ResetStep(int stepIndex);

public static class TargetEvent
{	
	public static event TargetReachedHandler TargetReached;
	public static event NewCustomStep OnStartCustomStep;
	public static event ResetStep OnResetToStep;
	public static event TargetReachedHandler OnTutorialFinished;

	public static void PerformTargetReached()
	{
		TargetReached?.Invoke();
	}

	public static void PerformNewCustomStep(string stepName)
	{
		OnStartCustomStep?.Invoke(stepName);
	}

	public static void PerformResetToStep(int stepIndex)
	{
		OnResetToStep?.Invoke(stepIndex);
	}

	public static void PerformTutorialFinished()
	{
		OnTutorialFinished?.Invoke();
	}
}
