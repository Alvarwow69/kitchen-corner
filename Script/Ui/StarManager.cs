using Godot;
using System;

public partial class StarManager : Node
{
	public void UpdateStar(int score)
	{
		if (score >= 100)
			GetChild<Star>(0).Activate();
		if (score >= 200)
			GetChild<Star>(1).Activate();
		if (score >= 300)
			GetChild<Star>(2).Activate();
	}
}
