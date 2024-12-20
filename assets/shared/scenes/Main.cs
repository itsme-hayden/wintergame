using Godot;
using System;

public partial class Main : Node
{

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
	}

	public void OnStartButtonPressed()
	{
		// In the future, the start game will be randomized
		// For now, just load Asteroids
		GetNode<GameSwitcher>("/root/GameSwitcher").SwitchMicroGame(GameSwitcher.MicroGameType.ASTEROIDS);
	}
}
