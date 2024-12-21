using Godot;
using System;

public partial class Asteroids : MicroGame
{
	public override void _Ready() 
	{ 
		base._Ready();
	}

	public override void _Process(double delta) { }

	public void OnReturnButtonPressed()
	{
		_util.SwitchMicroGame(GameUtil.MicroGameType.HOME);
	}
}
