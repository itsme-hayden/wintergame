using Godot;
using System;

public partial class Asteroids : MicroGame
{
	public static readonly float[] MeteorVelocities = {-MathF.PI / 32, -MathF.PI / 64, MathF.PI / 64, MathF.PI / 32};

	public const double MeteorSpawnTime = 1.0;

	private Area2D _rocket;
	private PackedScene _meteorScene;

	private int _meteorCount = 5;
	private double _meteorTimer = MeteorSpawnTime;

	public override void _Ready() 
	{ 
		base._Ready();

		_rocket = GetNode<Area2D>("Rocket");
		_meteorScene = GD.Load<PackedScene>("res://assets/asteroids/scenes/meteor.tscn");

		// WHY ARE SPRITES CENTER ANCHORED?
		// MY SPRITE'S X, Y SHOULD BE ITS TOP LEFT CORNER, NOT ITS CENTER!
		// This is going to create so many problems for me T.T
		var viewport = GetViewport().GetVisibleRect();
		_rocket.Position = new Vector2(viewport.Size.X / 2, viewport.Size.Y / 2);
	}

	public override void _Process(double delta) 
	{
		if(_meteorCount > 0)
		{
			_meteorTimer -= delta;
			if(_meteorTimer <= 0)
			{
				_meteorCount--;
				_meteorTimer = MeteorSpawnTime;
				var meteor = _meteorScene.Instantiate<Meteor>();
				var spawn = GetNode<PathFollow2D>("MeteorSpawnPath/MeteorSpawnLocation");
				spawn.ProgressRatio = GD.Randf();
				meteor.Position = spawn.Position;
				meteor.Rotation = spawn.Rotation;
				meteor.MoveTowardCenter(_rocket.Position);
				// The meteors are slowing down for some reason >:(
				meteor.AngularVelocity = MeteorVelocities[GD.Randi() % MeteorVelocities.Length] * 16;
				// meteor.LinearVelocity = new Vector2(0, 0); // Some kind of ray towards the center
				AddChild(meteor);
			}
		}
	}

	public void OnRocketHit()
	{
		EndMicroGame();
	}
}
