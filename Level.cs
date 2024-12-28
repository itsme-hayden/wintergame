using Godot;
using System;

public partial class Level : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// public void _on_exit_body_entered(Node body)
	// {
	// 	//touches star, finished level
	// 	GetTree().ChangeSceneToFile("res://Level2.tscn");
	// }

	public void ResetScene()
    {
		GetTree().ReloadCurrentScene();
        // Get the current scene's path
        // string currentScenePath = GetTree().CurrentScene.Filename;

        // // Reload the current scene
        // GetTree().ChangeSceneToFile(currentScenePath);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("reset"))
		{
			// reset scene
			ResetScene();

		}
	}
}
