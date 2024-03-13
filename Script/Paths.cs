using Godot;
using System;
using Godot.Collections;

public partial class Paths : Node3D
{
	#region Properties

	[Export] private Array<Path3D> _paths = new Array<Path3D>();
	[Export] private PackedScene _car;
	[Export] private float _minTimer = 3;
	[Export] private float _maxTimer = 3;
	[Export] private int _maxCars = 3;

	private double _timeBeforSpawn;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	#endregion

	public override void _Ready()
	{
		_timeBeforSpawn = _rng.RandfRange(_minTimer, _maxTimer);
	}

	public override void _Process(double delta)
	{
		if (GameManager.GetGameState() != GameManager.GameState.InGame)
			return;
		_timeBeforSpawn -= delta;
		if (_timeBeforSpawn <= 0)
		{
			for (int i = 0; i < _rng.RandiRange(1, _maxCars); i++)
				SpawnCar();
			_timeBeforSpawn = _rng.RandfRange(_minTimer, _maxTimer);
		}
	}

	private void SpawnCar()
	{
		var path = _paths.PickRandom();
		var car = GD.Load<PackedScene>(_car.ResourcePath).Instantiate() as PathFollow3D;

		path.AddChild(car);
		car.GlobalPosition = path.GlobalPosition;
	}
}
