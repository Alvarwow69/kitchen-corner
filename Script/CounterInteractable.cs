using Godot;
using System;
using System.Diagnostics;

public partial class CounterInteractable : SelectionInteractable
{
	[Export] private RigidInteractable _defaultInteractable { get; set; } = null;
	private Node3D _anchor;
	protected RigidInteractable _interactable = null;

	public override void _Ready()
	{
		_anchor = GetNode<Node3D>("StaticBody3D/Anchor");
		if (_defaultInteractable != null)
		{
			_interactable = _defaultInteractable;
			_interactable.Reparent(_anchor);
			_interactable.GlobalPosition = _anchor.GlobalPosition;
			_interactable.Freeze();
		}
		base._Ready();
	}
	
	public override void PerformAction(Player player)
	{
		if (_interactable == null)
			PlaceInteractable(player);
		else
			RemoveInteractable(player);
	}

	protected virtual void PlaceInteractable(Player player)
	{
		if (_interactable != null || !player.HasInteractable())
			return;
		_interactable = player.RemoveInteractable();
		_interactable.Freeze();
		_interactable.Reparent(_anchor);
		_interactable.GlobalPosition = _anchor.GlobalPosition;
	}

	protected virtual void RemoveInteractable(Player player)
	{
		if (_interactable == null || player.HasInteractable())
			return;
		player.AddInteractable(_interactable);
		_interactable = null;
	}
}
