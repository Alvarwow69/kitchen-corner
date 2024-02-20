using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Command : Node
{
	#region Properties

	[Export] private double _waitingTime = 30.0;
	[Export] private ProgressBar _progressBar;
	[Export] private StyleBoxFlat _styleBox;
	[Export] public Label Title { get; set; } = null;
	[Export] private TextureRect Image { get; set; } = null;
    
	private List<Ingredient> _ingredients;
	private double _timer;
	private bool _running = false;

	#endregion

	#region Methodes

	public override void _Ready()
	{
		_timer = _waitingTime;
		_running = true;
	}

	public override void _Process(double delta)
	{
		if (!_running)
			return;
		_timer -= delta;
		_progressBar.Value = _timer / _waitingTime * 100;
		if (_progressBar.Value <= 25)
			_progressBar.AddThemeStyleboxOverride("fill", _styleBox);
		if (_progressBar.Value <= 0)
			CloseCommand();
	}

	private void CloseCommand()
	{
		_running = false;
		(GetParent() as CommandManager)?.RemoveCommand(this);
	}

	#endregion
	
}