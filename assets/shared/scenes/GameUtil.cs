using Godot;
using System;

public partial class GameUtil : Node
{
	// I think I hate C# Docstrings >:(
	// Summarize deez
	/// <summary>
	/// The <c>MicroGameType</c> enum helps organize the micro game library 
	/// and ensure only the listed games can be requested
	/// </summary>
	public enum MicroGameType
	{
		HOME,
		ASTEROIDS,
		MAZE_SWIPER,
		DINO_RUNNER
	}

	public Node CurrentScene {get; private set;}
	
	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}

	public override void _Process(double delta) { }

	public void SwitchMicroGame(MicroGameType gameType)
	{
		// Wait until it is safe to terminate the current game by deferring its destruction
		CallDeferred(nameof(DeferredSwitchMicroGame), GetMicroGameScenePath(gameType));
	}

	private void DeferredSwitchMicroGame(string path, bool transition = false)
	{
		CurrentScene.Free();

		if(transition)
		{
			// Eventually, this will switch to a transition state first, 
			// then it will move to the desired state
		}

		var nextGame = (PackedScene) GD.Load(path);
		CurrentScene = nextGame.Instantiate<Node>();

		GetTree().Root.AddChild(CurrentScene);
		GetTree().CurrentScene = CurrentScene;
	}

	private string GetMicroGameScenePath(MicroGameType gameType)
	{
        return gameType switch
        {
            MicroGameType.ASTEROIDS => "res://assets/asteroids/scenes/asteroids.tscn",
			// MicroGameType.DINO_RUNNER => "res://assets/",
			// MicroGameType.MAZE_SWIPER => "res://assets/",
            _ => "res://assets/shared/scenes/main.tscn",
        };
    }
}
