using Godot;
using System;

public partial class Exit : Area2D
{
	[Export] private int requiredEssences = 2; // Total essences needed

	[Export]
    public string NextLevelPath = ""; // Path to the next level scene
    private int collectedEssences = 0;

	private CollisionShape2D _collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		_collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

	}

 private void OnBodyEntered(Node body)
    {
		GetTree().ChangeSceneToFile(NextLevelPath);
    }

	public void onEssenseCollected() {
		collectedEssences++;

		if (collectedEssences >= requiredEssences)
		{
			_collisionShape.SetDeferred("disabled",false);
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
