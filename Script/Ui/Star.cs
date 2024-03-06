using Godot;
using System;

public partial class Star : TextureRect
{
	#region Properties

	[Export] private Texture2D _starOn;
	[Export] private Texture2D _starOff;
	[Export] private bool _isOn = false;

	public override void _Ready()
	{
		Texture = _isOn ? _starOn : _starOff;
	}

	public void Activate()
	{
		_isOn = true;
		Texture = _starOn;
	}

	#endregion
}
