using Godot;
using System;
using System.Collections.Generic;

public partial class Lock : Area2D
{

	[Export] private string LockColor = "yellow"; // sets default color for lock to yellow
	private bool disabledPhysics = false;
    private static List<Lock> allLocks = new List<Lock>();

	private StaticBody2D _staticBody;

	public void resetLocksList()
	{
		allLocks.Clear();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		allLocks.Add(this);
		_staticBody = GetNode<StaticBody2D>("StaticBody2D");
	}

	public static void NotifyAllLocks(string color)
    {
        foreach (var lockInstance in allLocks)
        {
            lockInstance.disablePhysics(color);
        }
    }

	private void _on_lock_body_entered(Node2D body)
	{
		// if (body is Player player )
		if (disabledPhysics)
		{
			// if (player.HasKey(LockColor)){
			QueueFree();

			// }
		} 

	}
	

	public void disablePhysics(string color)
	{
		//deal with null reference
		if (this == null) return;

		if (color == LockColor){
			_staticBody.QueueFree();
			_staticBody = null;
			disabledPhysics = true;
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
