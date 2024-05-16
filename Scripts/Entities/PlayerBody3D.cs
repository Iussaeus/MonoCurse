using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class PlayerBody3D : FloatingBody3D
{
    [Export] public float RotationSpeed = 10;
    [Export] public float Speed = 75;
    [Export] public float CameraRotationSpeed = 1;
}