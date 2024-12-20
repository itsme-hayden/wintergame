using Godot;
using System;

// I may delete the MicroGame scene because it's just a bunch of properties
public partial class MicroGame : Node
{
	[Signal]
	public delegate void MicroGameEndedEventHandler();

	/// <summary>
	/// How many points are awarded for winning this micro game
	/// </summary>
	[Export]
	public int CompletionScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void _endMicroGame()
	{
		
	}
}
