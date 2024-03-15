using Godot;
using System;

public partial class GameConfig : Node
{
	private ConfigFile _config;

	public override void _Ready()
	{
		_config = new ConfigFile();
		_config.SetValue("General", "numberPlayer", 1);
	}

	public void UpdatePlayer(int numberPlayer)
	{
		_config.SetValue("General", "numberPlayer", numberPlayer);
	}

	public int GetNumberPlayer()
	{
		return (int)_config.GetValue("General", "numberPlayer");
	}
}
