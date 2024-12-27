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

	public static GameUtil Instance {get; private set;}

	public static Node CurrentScene {get; private set;}

	public static Vector2 ViewportDimensions {get; private set;}


	public static void SwitchMicroGame(MicroGameType gameType)
	{
		// Wait until it is safe to terminate the current game by deferring its destruction
		Instance.CallDeferred(nameof(DeferredSwitchMicroGame), Instance.GetMicroGameScenePath(gameType));
	}

	private static void DeferredSwitchMicroGame(string path)
	{
		CurrentScene.Free();

		/*
			if(transition)
			{
				// Eventually, this will switch to a transition state first, 
				// then it will move to the desired state
			}
		*/

		var nextGame = GD.Load<PackedScene>(path);
		CurrentScene = nextGame.Instantiate<Node>();

		Instance.GetTree().Root.AddChild(CurrentScene);
		Instance.GetTree().CurrentScene = CurrentScene;
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

	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		GameUtil.Instance = GetNode<GameUtil>("/root/GameUtil");
		GameUtil.CurrentScene = root.GetChild(root.GetChildCount() - 1);
		GameUtil.ViewportDimensions = GetViewport().GetVisibleRect().Size;
	}

	public override void _Process(double delta) { }
}
