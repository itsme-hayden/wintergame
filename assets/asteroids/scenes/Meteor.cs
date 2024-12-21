using Godot;
using System;

public partial class Meteor : Area2D
{
	private double _deltaTheta = 0.0;

	public override void _Ready()
	{
		_deltaTheta = Mathf.Pi / 4;
	}

	public override void _Process(double deltaTime)
	{
		Rotate((float)(_deltaTheta / deltaTime));
	}
}
