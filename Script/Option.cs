using Godot;
using System;
using System.Diagnostics;

public partial class Option : Control
{
	[Export] private Control _optionMenu;
	[Export] private Control _commandMenu;

	private bool _isInCommand = false;

	public void OnSliderMusicChange(float value)
	{
		var bus = AudioServer.GetBusIndex("Music");
		if (value <= -30)
			AudioServer.SetBusVolumeDb(bus, -80);
		else
			AudioServer.SetBusVolumeDb(bus, value);
	}

	public void OnSliderSfxChange(float value)
	{
		var bus = AudioServer.GetBusIndex("Sfx");
		if (value <= -30)
			AudioServer.SetBusVolumeDb(bus, -80);
		else
			AudioServer.SetBusVolumeDb(bus, value);
	}

	public void OnButtonCommandPressed()
	{
		_isInCommand = !_isInCommand;
		if (_isInCommand)
		{
			_optionMenu.Visible = false;
			_commandMenu.Visible = true;
		}
		else
		{
			_optionMenu.Visible = true;
			_commandMenu.Visible = false;
		}
	}
}
