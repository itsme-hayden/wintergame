using Godot;
using System;

public partial class Game : Node
{
	// I think I hate C# Docstrings >:(
	// Summarize deez
	/// <summary>
	/// The <c>MicroGameType</c> enum helps organize the micro game library 
	/// and ensure only the listed games can be requested
	/// </summary>
	public enum MicroGameType
	{
		ASTEROIDS,
		MAZE_SWIPER,
		DINO_RUNNER
	}

	/// <summary>
	/// Piss
	/// </summary>
	[Export]
	public MicroGame current_game;
	
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		
	}

	public void ResetMicroGame(MicroGameType newGame)
	{
		switch(newGame)
		{
			case MicroGameType.ASTEROIDS:
				break; // Load ASTEROIDS micro game
			default:
				break; // Don't do anything
		}
	}
}
