using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class PlayerBody3D : DamageableBody3D
{
    [Export] public float RotationSpeed = 10;
    [Export] public float Speed = 75;
    [Export] public float CameraRotationSpeed = 1;
    

    public override void _Ready()
    {
        base._Ready();
    }
}