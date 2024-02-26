using Godot;
using System;
using System.Diagnostics;

// TODO Fix Bowl and Pot

public partial class CounterInteractable : SelectionInteractable
{
	[Export] private RigidInteractable _defaultInteractable { get; set; } = null;

	[Export]
	protected Node3D _anchor = null;
	protected RigidInteractable _interactable = null;

	public override void _Ready()
	{
		_anchor = _anchor == null ? GetNode<Node3D>("StaticBody3D/Anchor") : _anchor;
		if (_defaultInteractable != null)
		{
			_interactable = _defaultInteractable;
			_interactable.Freeze();
			_interactable.Reparent(_anchor);
			_interactable.GlobalPosition = _anchor.GlobalPosition;
		}
		base._Ready();
	}
	
	public override void PerformAction(Player player)
	{
		if (_interactable == null)
			PlaceInteractable(player);
		else if (player.HasInteractable() && ((_interactable is Container && (_interactable as Container).CanGetFood()) || player.GetInteractable() is not Container))
			PlaceOnInteractable(player);
		else
			RemoveInteractable(player);
	}

	protected virtual void PlaceOnInteractable(Player player)
	{
		if (player.GetInteractable() is not Container)
			(_interactable as Container)?.AddFood(player.RemoveInteractable() as Ingredient);
		else
			(_interactable as Container)?.AddFood(player.RemoveFromInteractable() as Ingredient);
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
		if (_interactable == null)
			return;
		if (!player.HasInteractable())
		{
			player.AddInteractable(_interactable);
			_interactable.Player = player;
			_interactable = null;
		}
		else
		{
			if (_interactable is Container)
			{
				var ingredient = (_interactable as Cooker)?.RemoveIngredient();
				(player.GetInteractable() as Ingredient)?.AddFood(ingredient);
				return;
			}

			if (player.GetInteractable() is Container)
				player.AddInteractable(_interactable);
			else
				(player.GetInteractable() as Ingredient)?.AddFood(_interactable as Ingredient);
			_interactable = null;
		}

	}
}
