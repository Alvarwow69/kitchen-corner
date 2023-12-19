using Godot;
using System;
using System.Diagnostics;

public partial class Food : RigidInteractable
{
	public enum State
	{
		RAW,
		SLICED,
		COOCKED,
		BURNED
	}

	[Export] private State _state = State.RAW;


	public override void PerformAction(Player player)
	{
		Freeze();
		player.AddInteractable(this);
	}

	public override void Drop(Player player)
	{
		player.RemoveInteractable();
		Activate();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
