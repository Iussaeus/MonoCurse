using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class PlayerCamera : Node3D 
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GlobalRotation = GlobalRotation with { Y = 0 };
		GlobalRotation = GlobalRotation with { X = 0 };
		GlobalRotation = GlobalRotation with { Z = 0 };
	}
}