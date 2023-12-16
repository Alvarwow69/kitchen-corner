using Godot;
using System;
using System.Diagnostics;

public partial class Interactable : Node3D
{
	public enum State
	{
		IDLE,
		SELECT,
		ACTION
	}

	protected State _state;

	public virtual void PerformAction(Player player)
	{
		Debug.Print("Action Performed.");
	}

	public virtual void ProcessAction(Player player)
	{
		Debug.Print("Action Performed.");
	}

}
