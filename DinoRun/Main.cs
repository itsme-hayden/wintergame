using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Main : Node
{

	

	private Area2D _dino;
	private Camera2D camera;
	private PackedScene _main;
	private float x;
	const float DINO_START_SPEED = 10.0f;
	const float MAX_SPEED = 25.0f;
	float speed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//thanks for the code Joe
		//the tutorial I used only used GDscript
		//4 hours of my life i am never getting back
		base._Ready();

		_dino = GetNode<Area2D>("Dino");
		_main = GD.Load<PackedScene>("res://main.tscn");

		var pov = GetViewport().GetVisibleRect();
		_dino.Position = new Vector2(150.0f,485.0f);
		camera.Position = new Vector2(576, 324);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		speed = DINO_START_SPEED;
		var pos = Vector2.Zero; //double entendre 
		x += speed;
		pos.X += x; //TODO: make this not the worst thing ever
		pos.Y = 485.0f; 
		
		_dino.Position.X += speed;
		pos.Y = 324.0f;
		camera.Position += pos;

		/*
		ERROR my dino's x position is not changing in game!

		possible reasons:
			my tree is screwed because "Dino" is accidentally a child of a random 2Dnode -- would be annoying to fix
			I just don't know how to connect crap in my main script
			help would be appreciated.
			I am too tired for this
		*/

	}
}
