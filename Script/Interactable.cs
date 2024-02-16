using Godot;
using System;
using System.Diagnostics;

public partial class Interactable : Node3D
{
	public enum InteractableState
	{
		IDLE,
		SELECT,
		ACTION
	}

	[Export] public CollisionObject3D _hitbox;

	protected InteractableState _InteractableState;
	public Player Player { get; set; } = null;

	public virtual void HoverEnter(Player player)
	{ }

	public virtual void HoverExit(Player player)
	{ }

	public virtual void PerformAction(Player player)
	{ }

	public virtual void ProcessAction(Player player)
	{ }

	public virtual void EndProcessAction(Player player)
	{ }

	public virtual void Drop(Player player)
	{ }

	public virtual void EnableHitBox()
	{ }
	
	public virtual void DisableHitBox()
	{ }
}
