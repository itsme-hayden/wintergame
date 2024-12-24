using Godot;
using System;

public partial class Meteor : RigidBody2D
{
	public override void _Ready() { }

	public override void _Process(double deltaTime) { }

	public void MoveTowardCenter(Vector2 rocketPosition)
	{
		// Deviate path slightly
		rocketPosition.X = (float) GD.Randfn(rocketPosition.X, 24);
		rocketPosition.Y = (float) GD.Randfn(rocketPosition.Y, 24);

		var dx = rocketPosition.X - Position.X;
		var dy = rocketPosition.Y - Position.Y;
		var dt = GD.RandRange(2, 5);

		LinearVelocity = new Vector2(dx / dt, dy / dt);
	}
}
