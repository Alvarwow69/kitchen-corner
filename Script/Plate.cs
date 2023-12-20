using Godot;
using Godot.Collections;

public partial class Plate : RigidInteractable
{
	public enum State
	{
		Clean,
		Dirty
	}
	
	[Export] private State _state = State.Clean;
	private Node3D _anchor;
	private Array<Food> _foods = new Array<Food>();
	private MeshInstance3D _cleanModel;
	private MeshInstance3D _dirtyModel;

	public override void _Ready()
	{
		base._Ready();
		_anchor = GetNode<Node3D>("RigidBody3D/Anchor");
		_cleanModel = GetNode<MeshInstance3D>("RigidBody3D/Clean");
		_dirtyModel = GetNode<MeshInstance3D>("RigidBody3D/Dirty");
		GetNode<MeshInstance3D>("RigidBody3D/" + State.Clean).Visible = false;
		SetState(_state);
	}

	public override void PerformAction(Player player)
	{
		if (!player.HasInteractable())
		{
			player.AddInteractable(this);
			Freeze();
		}
	}

	public override void Drop(Player player)
	{
		player.RemoveInteractable();
		Activate();
	}

	public void AddFood(Food food)
	{
		if (_state == State.Dirty)
			return;
		foreach (var element in _foods)
			if (!element.IsCompatible(food))
				return;
		food.Reparent(_anchor);
		food.Freeze();
		food.GlobalPosition = _anchor.GlobalPosition;
		food.GlobalRotation = _anchor.GlobalRotation;
		_foods.Add(food);
	}
	
	public void SetState(State newState)
	{
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = false;
		_state = newState;
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = true;
	}
}
