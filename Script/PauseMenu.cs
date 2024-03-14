using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private Control _optionMenu;
	[Export] private Control _pauseMenu;

	private bool _isInOption = false;

	public void OnButtonOptionPressed()
	{
		_isInOption = !_isInOption;
		if (_isInOption)
		{
			_pauseMenu.Visible = false;
			_optionMenu.Visible = true;
		}
		else
		{
			_pauseMenu.Visible = true;
			_optionMenu.Visible = false;
		}
	}

	public void OnButtonMenuPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/SC_MainMenu.tscn");
	}

	public void OnButtonExitPressed()
	{
		GetTree().Quit();
	}

	public void Enable()
	{
		Visible = true;
	}

	public void Disable()
	{
		Visible = false;
		_pauseMenu.Visible = true;
		_optionMenu.Visible = false;
	}
}
