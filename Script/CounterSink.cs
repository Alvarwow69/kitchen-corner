using Godot;
using System;
using System.Collections.Generic;

public partial class CounterSink : CounterInteractable
{
	[Export] private DishRack _dishRack;
	[Export] private ProgressBar _progressBar;
	[Export] private float _cleanTime;

	private Stack<Plate> _plates = new Stack<Plate>();
	private float _cleanTimer = 0;
	private bool _cleaning = false;

	public override void _Ready()
	{
		base._Ready();
		_progressBar.Visible = false;
		_progressBar.Value = 0;
	}

	protected override void PlaceInteractable(Player player)
	{
		if (player.GetInteractable() is not Plate || _interactable != null || !player.HasInteractable())
			return;
		var plate = player.RemoveInteractable();
		_plates.Push(plate as Plate);
		plate.Freeze();
		plate.Reparent(_anchor);
		plate.GlobalPosition = _anchor.GlobalPosition;
	}

	public override void ProcessAction(Player player)
	{
		_cleaning = true;
	}

	public override void EndProcessAction(Player player)
	{
		_cleaning = false;
	}
}
