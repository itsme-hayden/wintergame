using Godot;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Random;


public partial class DinoRunner : MicroGame
{

	public PackedScene bigTree_scene = ResourceLoader.Load<PackedScene>("res://assets/dinorunner/scenes/big_tree.tscn");
	public PackedScene bush_scene = ResourceLoader.Load<PackedScene>("res://assets/dinorunner/scenes/bush.tscn");
	public PackedScene cloud_scene = ResourceLoader.Load<PackedScene>("res://assets/dinorunner/scenes/cloud.tscn");
	public PackedScene tree_scene = ResourceLoader.Load<PackedScene>("res://assets/dinorunner/scenes/tree.tscn");
	private Collection<Node2D> obstacles;
	private Collection<PackedScene> obstacle_types;

	public const float DINO_START_SPEED = 300.0f;
	public const float MAX_SPEED = 1000.0f;

	// Pixels per second per second
	public const float Acceleration = 10.0f;

	public int ground_height;

	private CharacterBody2D _dino;
	private Camera2D _camera;
	private StaticBody2D _ground;
	private Sprite2D _cloud;
	private Node2D last_obs;
	
	// Pixels per second
	private float _speed;
	

	//generates obstacles
	public void generate_obs()
	{
		//GD.Print("START GENERATE");
		if (obstacles.Count == 0 || last_obs.Position.X < _dino.Position.X + GD.RandRange(50,300))
		{
			//GD.Print("IN IF STATEMENT");
			var obs_type = obstacle_types[GD.RandRange(0,3)]; //I don't like upper being inclusive
			Node2D obs;
			int max_obs = 3; 
			for (int i = 0; i < GD.RandRange(0,max_obs); i++)
			{
				obs = (Node2D) obs_type.Instantiate();
				var obs_height = obs.GetNode<Sprite2D>("Sprite2D").Texture.GetHeight();
				var obs_scale = obs.GetNode<Sprite2D>("Sprite2D").Scale;
				var obs_x = GetWindow().Size.X + _dino.Position.X + 100 + (i * 100);
				var obs_y = GetWindow().Size.Y - ground_height - (obs_height * obs_scale.Y / 2) - 90;
				last_obs = obs;
				add_obs(obs,obs_x,obs_y);
			}
			
		}
		
	}

	public void add_obs(Node2D obs, float x, float y)
	{
		var pos = obs.Position;
		pos.X = x;
		pos.Y = y;
		obs.Position = pos;
        obs.Connect("body_entered", new Callable(this, MethodName.Hit_obs));
		AddChild(obs);
		obstacles.Add(obs);
	}

	public void Hit_obs(Node2D body)
	{
		if (body.Name == "Dino")
		{
			game_over();	//TODO: This is where Joe needs to put stuff to connect to larger game
		}
	}

	public void game_over()
	{
		GetTree().Paused = true;
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//thanks for the code Joe
		//the tutorial I used only used GDscript
		//4 hours of my life i am never getting back
		base._Ready();

		obstacle_types = new Collection<PackedScene>{bigTree_scene, bush_scene, cloud_scene, tree_scene};
		obstacles = new Collection<Node2D>{};
		ground_height = GetNode<StaticBody2D>("Ground").GetChild<Sprite2D>(0).Texture.GetHeight();

		// Set object variables
		_dino = GetNode<CharacterBody2D>("Dino");
		_camera = GetNode<Camera2D>("Camera2D");
		_ground = GetNode<StaticBody2D>("Ground");
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
		pos.X += 300;
		_camera.Position = pos;

		generate_obs();

		if(_speed < MAX_SPEED)
		{
			_speed += Acceleration * (float) delta; // Conversion: (Pixels / seconds^2) * seconds
		}

		if (_camera.Position.X - _ground.Position.X > GetWindow().Size.X * 1.5)
		{
			var _groundPos = _ground.Position;
			_groundPos.X += GetWindow().Size.X;
			_ground.Position = _groundPos;
		}
		

	}
}
