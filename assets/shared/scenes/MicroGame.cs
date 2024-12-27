using Godot;
using System;

// This class is seeming less and less useful.
// It'll stay for now, but it may not forever.
public partial class MicroGame : Node
{
	[Signal]
	public delegate void MicroGameEndedEventHandler();

	/// <summary>
	/// How many points are awarded for winning this micro game
	/// </summary>
	[Export]
	public int CompletionScore;

    public override void _Ready() { }

	protected void EndMicroGame()
	{
		EmitSignal(SignalName.MicroGameEnded);

		GameUtil.SwitchMicroGame(GameUtil.MicroGameType.HOME);
	}
}
