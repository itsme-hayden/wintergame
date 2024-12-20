using Godot;
using System;

public partial class GameSwitcher : Node
{
	// I think I hate C# Docstrings >:(
	// Summarize deez
	/// <summary>
	/// The <c>MicroGameType</c> enum helps organize the micro game library 
	/// and ensure only the listed games can be requested
	/// </summary>
	public enum MicroGameType
	{
		ASTEROIDS,
		MAZE_SWIPER,
		DINO_RUNNER
	}

	public MicroGame CurrentGame {get; set;}
	
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		
	}

	public void SwitchMicroGame(MicroGameType gameType)
	{
		// Wait until it is safe to terminate the current game by deferring its destruction
		CallDeferred(nameof(DeferredSwitchMicroGame), GetMicroGameScenePath(gameType));
	}

	private void DeferredSwitchMicroGame(string path)
	{
		CurrentGame?.Free();

		var nextGame = (PackedScene) GD.Load(path);
		CurrentGame = nextGame.Instantiate<MicroGame>();

		GetTree().Root.AddChild(CurrentGame);
		GetTree().CurrentScene = CurrentGame;
	}

	private string GetMicroGameScenePath(MicroGameType gameType)
	{
        return gameType switch
        {
            MicroGameType.ASTEROIDS => "res://assets/asteroids/scenes/asteroids.tscn",
			// MicroGameType.DINO_RUNNER => "res://assets/",
			// MicroGameType.MAZE_SWIPER => "res://assets/",
            _ => null,
        };
    }
}
