using Godot;
using System;

public partial class RigidInteractable : Interactable
{
	protected RigidBody3D RigidBody;
	protected Area3D HitBox;
	protected uint SaveRigidLayer;
	protected uint SaveRigidMask;
	protected uint SaveHitLayer;
	protected uint SaveHitMask;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RigidBody = GetNode<RigidBody3D>("RigidBody3D");
		HitBox = GetNode<Area3D>("HitBox");
		SaveRigidLayer = RigidBody.CollisionLayer;
		SaveRigidMask = RigidBody.CollisionMask;
		SaveHitLayer = HitBox.CollisionLayer;
		SaveHitMask = HitBox.CollisionMask;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HitBox.GlobalPosition = RigidBody.GlobalPosition;
	}

	public virtual void Freeze()
	{
		RigidBody.Freeze = true;
		RigidBody.GlobalPosition = GlobalPosition;
		RigidBody.CollisionLayer = 0;
		RigidBody.CollisionMask = 0;
		HitBox.CollisionLayer = 0;
		HitBox.CollisionMask = 0;
	}

	public virtual void Activate()
	{
		RigidBody.Freeze = false;
		RigidBody.CollisionLayer = SaveRigidLayer;
		RigidBody.CollisionMask = SaveRigidMask;
		HitBox.CollisionLayer = SaveHitLayer;
		HitBox.CollisionMask = SaveHitMask;
	}
}
