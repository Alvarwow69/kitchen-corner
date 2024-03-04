using Godot;
using System;

public partial class NodeLevelSelector : Interactable
{
	#region Properties

	[Export] private PackedScene _newLevel;

	#endregion
	public override void PerformAction(Player player)
	{
		GetTree().ChangeSceneToPacked(_newLevel);
	}
}
