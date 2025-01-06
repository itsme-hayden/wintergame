using Godot;
using System;

public partial class Asteroids : MicroGame
{
	public static readonly float[] MeteorVelocities = {-MathF.PI / 32, -MathF.PI / 64, MathF.PI / 64, MathF.PI / 32};

	public const double MeteorSpawnTime = 1.0;

	private Rocket _rocket;
	private RigidBody2D _followPoint;
	private PackedScene _meteorScene;

	private int _meteorCount = 5;
	private double _meteorTimer = MeteorSpawnTime;

	public override void _Ready() 
	{
		base._Ready();

		_rocket = GetNode<Rocket>("Rocket");
		_followPoint = GetNode<RigidBody2D>("FollowPoint");
		_meteorScene = GD.Load<PackedScene>("res://assets/asteroids/scenes/meteor.tscn");

		// WHY ARE SPRITES CENTER ANCHORED?
		// MY SPRITE'S X, Y SHOULD BE ITS TOP LEFT CORNER, NOT ITS CENTER!
		// This is going to create so many problems for me T.T
		var viewport = GetViewport().GetVisibleRect();
		_rocket.Position = new Vector2(viewport.Size.X / 2, viewport.Size.Y / 2);
		_followPoint.Position = _rocket.Position;
		_followPoint.LinearVelocity = new Vector2(0, -150);
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
				meteor.AngularVelocity = MeteorVelocities[GD.Randi() % MeteorVelocities.Length] * 16;
				// AddChild(meteor);
			}
		}
	}

    public override void _Input(InputEvent _event)
    {
        base._Input(_event);

		// These will need to be touch events in the future
		if(_event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsReleased())
		{

			if(mouseEvent.Position.X < GameUtil.ViewportDimensions.X / 2)
			{
				_followPoint.LinearVelocity = _followPoint.LinearVelocity.Rotated(-MathF.PI / 8);
				_followPoint.Rotate(-MathF.PI / 8);
				GD.Print("Rotate CCW");
			}
			else
			{
				_followPoint.LinearVelocity = _followPoint.LinearVelocity.Rotated(MathF.PI / 8);
				_followPoint.Rotate(MathF.PI / 8);
				GD.Print("Rotate CW");
			}
		}
    }

	private void UpdateFollowPoint()
	{
		
	}

    public void OnRocketHit(Node2D body)
	{
		GD.Print("GAMEOVER");
		// EndMicroGame();
	}
}
