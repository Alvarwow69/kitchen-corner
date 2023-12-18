using Godot;
using System;
using System.Diagnostics;

public partial class Food : Interactable
{
	public enum State
	{
		RAW,
		SLICED,
		COOCKED,
		BURNED
	}

	[Export] private State _state = State.RAW;
	private RigidBody3D _rigidBody;
	private uint _saveLayer;
	private uint _saveMask;


	public override void PerformAction(Player player)
	{
		_rigidBody.Freeze = true;
		_rigidBody.GlobalPosition = GlobalPosition;
		_rigidBody.CollisionLayer = 0;
		_rigidBody.CollisionMask = 0;
		player.AddInteractable(this);
	}

	public override void Drop(Player player)
	{
		player.RemoveInteractable();
		_rigidBody.Freeze = false;
		_rigidBody.CollisionLayer = _saveLayer;
		_rigidBody.CollisionMask = _saveMask;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rigidBody = GetNode<RigidBody3D>("RigidBody3D");
		_saveLayer = _rigidBody.CollisionLayer;
		_saveMask = _rigidBody.CollisionMask;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
