using Godot;
using System;

public partial class Key : Area2D
{

//ref the lock
private Lock _lock;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// get lock node
		_lock = GetNode<Lock>("../Lock");
	}

	public void _on_key_body_entered(Node body)
	{
		if (body is Player player)
		{
			player.HasKey = true;

			// disable physics on lock as player has key
			_lock.disablePhysics();

			QueueFree(); // get rid from scene
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
