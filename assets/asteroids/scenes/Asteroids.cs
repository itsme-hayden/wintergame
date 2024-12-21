using Godot;
using System;

public partial class Asteroids : MicroGame
{
	public const double MeteorSpawnTime = 5.0;

	private Area2D _rocket;
	private PackedScene _meteorScene;

	private double _meteorTimer = MeteorSpawnTime;

	public override void _Ready() 
	{ 
		base._Ready();

		_rocket = GetNode<Area2D>("Rocket");
		_meteorScene = GD.Load<PackedScene>("res://assets/asteroids/meteor.tscn");
	}

	public override void _Process(double delta) 
	{ 
		_meteorTimer -= delta;
		if(_meteorTimer <= 0)
		{
			_meteorTimer = MeteorSpawnTime;
			var meteor = _meteorScene.Instantiate<Area2D>();
			AddChild(meteor);
		}
	}

	public void OnRocketHit()
	{
		// End the game
	}
}
