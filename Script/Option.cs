using Godot;
using System;
using System.Diagnostics;

public partial class Option : Control
{
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
}
