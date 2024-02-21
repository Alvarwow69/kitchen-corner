using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class PlateManager : CounterInteractable
{

	#region properties

	private Dictionary<Plate, double> _waitingPlate = new Dictionary<Plate, double>();
	private Array<Plate> _plates = new Array<Plate>();

	#endregion

	#region Methodes

	public override void _Process(double delta)
	{
		if (_waitingPlate.Count == 0)
			return;
		foreach (var plate in _waitingPlate)
		{
			_waitingPlate[plate.Key] += delta;
			if (plate.Value >= 2)
			{
				SendPlate(plate.Key);
				_waitingPlate.Remove(plate.Key);
			}
		}
	}

	public void AddPlate(Plate plate)
	{
		plate.Visible = false;
		_waitingPlate.Add(plate, 0);
	}

	private void SendPlate(Plate plate)
	{
		plate.Reparent(_anchor);
		plate.GlobalPosition = new Vector3(_anchor.GlobalPosition.X, _anchor.GlobalPosition.Y + _plates.Count * 0.13f, _anchor.GlobalPosition.Z);
		plate.GlobalRotation = _anchor.GlobalRotation;
		plate.Visible = true;
		plate.SetState(Plate.PlateState.Dirty);
		_plates.Add(plate);
	}

	public override void PerformAction(Player player)
	{
		if (player.HasInteractable() && _plates.Count == 0)
			return;
		var plate = _plates[0];
		_plates.Remove(plate);
		player.AddInteractable(plate);
	}

	#endregion
}
