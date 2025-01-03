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
		// cleanup scene before reloading
		 foreach (Node child in GetTree().CurrentScene.GetChildren())
        {
            // Check if the child is of type Key
            if (child is Key )
            {
                // Remove the child from the scene
                child.QueueFree();
            }
			else if (child is Lock _lock)
			{
				// Remove the child from the scene
				if (_lock != null) {
				_lock.resetLocksList();
				_lock.QueueFree();
				}
			}
			else if (child is Player player) {
				player.ResetKeys();
				player.QueueFree();	
			}
        }
		
		// reset lock list

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
