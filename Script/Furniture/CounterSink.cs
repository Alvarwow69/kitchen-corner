using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KitchenCorner.Script.Event;

public partial class CounterSink : CounterInteractable
{
	[Export] private DishRack _dishRack;
	[Export] private ProgressBar _progressBar;
	[Export] private double _cleanTime;

	private Stack<Plate> _plates = new Stack<Plate>();
	private double _cleanTimer = 0;
	private bool _cleaning = false;

	public override void _Ready()
	{
		base._Ready();
		_progressBar.Visible = false;
		_progressBar.Value = 0;
	}

	protected override void PlaceInteractable(Player player)
	{
		if (player.GetInteractable() is not Plate || _interactable != null || !player.HasInteractable() ||
		    (player.GetInteractable() as Plate).isClean())
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

	public override void _Process(double delta)
	{
		if (!_cleaning || _plates.Count == 0)
			return;
		_cleanTimer += delta;
		_progressBar.Value = _cleanTimer / _cleanTime * 100;
		if (_progressBar.Value > 0)
			_progressBar.Visible = true;
		if (_cleanTimer < _cleanTime)
			return;
		var plate = _plates.Pop();
		PlateEvent.PerformCleanPlate(plate);
		plate.SetState(Plate.PlateState.Clean);
		_dishRack.AddPlate(plate);
		_cleanTimer = 0;
		_progressBar.Value = 0;
		_progressBar.Visible = false;
	}
}
