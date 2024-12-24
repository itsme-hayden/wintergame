using Godot;
using System;

public partial class MicroGame : Node
{
	[Signal]
	public delegate void MicroGameEndedEventHandler();

	/// <summary>
	/// How many points are awarded for winning this micro game
	/// </summary>
	[Export]
	public int CompletionScore;

	protected GameUtil _util;

    public override void _Ready()
    {
        _util = GetNode<GameUtil>("/root/GameUtil");
    }

	protected void EndMicroGame()
	{
		_util.SwitchMicroGame(GameUtil.MicroGameType.HOME);
	}
}
