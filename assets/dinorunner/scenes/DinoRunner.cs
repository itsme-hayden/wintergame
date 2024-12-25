using Godot;
using System;

public partial class DinoRunner : MicroGame
{
	public const float DINO_START_SPEED = 10.0f;
	public const float MAX_SPEED = 25.0f;

	// Pixels per second per second
	public const float Acceleration = 0.5f;

	private CharacterBody2D _dino;
	
	// Pixels per second
	private float _speed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//thanks for the code Joe
		//the tutorial I used only used GDscript
		//4 hours of my life i am never getting back
		base._Ready();

		// Set object variables
		_dino = GetNode<CharacterBody2D>("Dino");
		_speed = DINO_START_SPEED;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// You have to get, set, then reset the position values
		// It would be really cool if you could just directly change x and y

		var pos = _dino.Position; //double entendre 
		pos.X += _speed * (float) delta;  // Conversion: (Pixels / seconds) * seconds
		_dino.Position = pos;

		if(_speed < MAX_SPEED)
		{
			_speed += Acceleration * (float) delta; // Conversion: (Pixels / seconds^2) * seconds
		}

		GD.Print(_speed);
	}
}
