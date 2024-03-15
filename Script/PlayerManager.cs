using Godot;
using System;
using Godot.Collections;

public partial class PlayerManager : Control
{
	[Export] private Array<PlayerItem> _playerController = new Array<PlayerItem>();

	private int _playernumber = 1;
	public override void _Ready()
	{
		var tmp = GetNode<GameConfig>("/root/GameConfig").GetNumberPlayer();
		for (int i = 1; i < tmp; i++)
			AddPlayer();
	}

	private void AddPlayer()
	{
		_playernumber += 1;
		GetNode<GameConfig>("/root/GameConfig").UpdatePlayer(_playernumber);
		_playerController[_playernumber - 1].EnablePlayer();
		if (_playernumber + 1 > _playerController.Count)
			return;
		_playerController[_playernumber].Visible = true;
	}

	private void RemovePlayer()
	{
		_playernumber -= 1;
		GetNode<GameConfig>("/root/GameConfig").UpdatePlayer(_playernumber);
		_playerController[_playernumber].DisablePlayer();
		if (_playernumber + 1 >= _playerController.Count)
			return;
		_playerController[_playernumber + 1].Visible = false;
	}
}
