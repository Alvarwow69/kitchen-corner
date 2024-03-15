using Godot;
using System;
using System.Diagnostics;

public partial class Option : Control
{
	[Export] private Control _optionMenu;
	[Export] private Control _commandMenu;
	[Export] private Slider _sliderMusic;
	[Export] private Slider _sliderSfx;

	private bool _isInCommand = false;

	public override void _Ready()
	{
		_sliderMusic.Value = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music"));
		_sliderSfx.Value = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sfx"));
	}

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
