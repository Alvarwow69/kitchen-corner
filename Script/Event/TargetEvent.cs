using Godot;
using System;

public delegate void TargetReachedHandler();

public static class TargetEvent
{	
	public static event TargetReachedHandler TargetReached;

	public static void PerformTargetReached()
	{
		TargetReached?.Invoke();
	}
}
