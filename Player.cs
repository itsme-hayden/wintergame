using Godot;
public partial class Player : CharacterBody2D

{
    [Export] public float SlideSpeed = 300f;  // Speed of movement
    private const int GridSize = 10;  // Size of each grid cell
    private const float CollisionBuffer = 2f;  // Buffer distance to prevent false invalid checks

    private Vector2 _velocity = Vector2.Zero;
    private bool _isSliding = false;
    private Vector2 _direction = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        if (_isSliding)
        {
            // Keep sliding in the chosen direction
            SlideUntilCollision((float)delta);
        }
        else
        {
            // Input handling to start sliding
            if (Input.IsActionJustPressed("ui_up"))
                StartSliding(Vector2.Up);
            else if (Input.IsActionJustPressed("ui_down"))
                StartSliding(Vector2.Down);
            else if (Input.IsActionJustPressed("ui_left"))
                StartSliding(Vector2.Left);
            else if (Input.IsActionJustPressed("ui_right"))
                StartSliding(Vector2.Right);
        }
    }

    private void StartSliding(Vector2 direction)
    {
        // Set the initial sliding direction
        _direction = direction.Normalized();
        _velocity = _direction * SlideSpeed;

        // Start the sliding process
        _isSliding = true;
    }

    private void SlideUntilCollision(float delta)
    {
        // Calculate the next position based on the velocity and delta time
        Vector2 nextPosition = Position + _velocity * delta;

        // Check if the next position is valid (no collision)
        if (IsPositionValid(nextPosition))
        {
            // Update position if there's no collision
            Position = nextPosition;
        }
        else
        {
            // Stop the sliding if a collision is detected
            _isSliding = false;
            SnapToGrid();  // Snap to the grid at the point of collision
        }
    }

    private void SnapToGrid()
    {
        // Snap to the closest grid cell
        Position = new Vector2(
            Mathf.Round(Position.X / GridSize) * GridSize,
            Mathf.Round(Position.Y / GridSize) * GridSize
        );
    }

    private bool IsPositionValid(Vector2 targetPosition)
    {
        var spaceState = GetWorld2D().DirectSpaceState;

        // Extend the target position by a small buffer to avoid false invalid checks
        Vector2 bufferPosition = targetPosition + _direction * CollisionBuffer;

        // Create a point query parameter
        var query = new PhysicsPointQueryParameters2D
        {
            Position = bufferPosition,
            CollideWithAreas = true,  // Check for area colliders
            CollideWithBodies = true  // Check for body colliders (walls)
        };

        // Perform the point query and get the results
        var result = spaceState.IntersectPoint(query);

        // If no results, the position is valid (no collision)
        return result.Count == 0;
    }
}
