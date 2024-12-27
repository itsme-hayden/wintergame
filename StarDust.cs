using Godot;
using System;

public partial class StarDust : Area2D
{
	private AnimatedSprite2D _exit;
	private Exit _exitInst;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//get the exit node 
		_exit = GetNode<AnimatedSprite2D>("../Exit/Sprite2D");
		_exitInst = GetNode<Exit>("../Exit");
	}

	public void _on_stardust_body_entered(Node2D body)
	{
		// when collided with player,
		// increment exit's frame and get rid of current instance
		
		var currentFrame = _exit.Frame;
		_exit.Frame = (currentFrame + 1);  // Change frame cyclically


		// call instance to increment essense count
		_exitInst.onEssenseCollected();

		QueueFree();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
