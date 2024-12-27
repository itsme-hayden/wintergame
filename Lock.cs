using Godot;
using System;

public partial class Lock : Area2D
{
	private StaticBody2D _staticBody;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_staticBody = GetNode<StaticBody2D>("StaticBody2D");
	}

	private void _on_lock_body_entered(Node2D body)
	{
		if (body is Player player && player.HasKey)
		{
			QueueFree();
			player.HasKey = false;
		}
	}

	public void disablePhysics()
	{
	_staticBody.QueueFree();
	_staticBody = null;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
