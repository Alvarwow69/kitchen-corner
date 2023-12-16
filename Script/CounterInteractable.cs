using Godot;
using System;
using System.Diagnostics;

public partial class CounterInteractable : SelectionInteractable
{
	[Export] private Interactable _defaultInteractable { get; set; } = null;
	private Node3D _anchor;
	private Interactable _interactable = null;

	public override void _Ready()
	{
		_anchor = GetNode<Node3D>("Anchor");
		if (_defaultInteractable != null)
		{
			_interactable = _defaultInteractable;
			_interactable.Reparent(_anchor);
			_interactable.GlobalPosition = _anchor.GlobalPosition;
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

	private void PlaceInteractable(Player player)
	{
		if (_interactable != null || !player.HasInteractable())
			return;
		_interactable = player.RemoveInteractable();
		_interactable.Reparent(_anchor);
		_interactable.GlobalPosition = _anchor.GlobalPosition;
	}

	private void RemoveInteractable(Player player)
	{
		if (_interactable == null || player.HasInteractable())
			return;
		player.AddInteractable(_interactable);
		_interactable = null;
	}
}
