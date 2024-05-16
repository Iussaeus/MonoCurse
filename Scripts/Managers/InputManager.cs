using Godot;
using PirateCurse.Scripts.Entities;

namespace PirateCurse.Scripts.Managers;

public partial class InputManager : Node3D 
{
    [Export] private float _mouseSensitivity = 0.001f;
    
    private static PlayerBody3D _playerBody3D;
    private static Camera3D _playerCamera;
    private static Camera3D _worldCamera;
    private static Node3D _playerCameraMount;

    public override void _Ready()
    {
        _playerBody3D = GetNode<PlayerBody3D>("/root/World/SubViewportContainer/SubViewport/Ship");
        _playerCameraMount = GetNode<Node3D>("/root/World/SubViewportContainer/SubViewport/Ship/CameraMount");
        _playerCamera = GetNode<Camera3D>("/root/World/SubViewportContainer/SubViewport/Ship/CameraMount/Camera3D");
        _worldCamera = GetNode<Camera3D>("/root/World/SubViewportContainer/SubViewport/Camera3D");

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Process(double delta)
    {
    }
    

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("forward")) _playerBody3D.ApplyForce(_playerBody3D.Transform.Basis.Z * -_playerBody3D.Speed);
        if (Input.IsActionPressed("backward")) _playerBody3D.ApplyForce(_playerBody3D.Transform.Basis.Z * _playerBody3D.Speed);
        if (Input.IsActionPressed("left")) _playerBody3D.ApplyTorque(_playerBody3D.Transform.Basis.Y * _playerBody3D.RotationSpeed);
        if (Input.IsActionPressed("right")) _playerBody3D.ApplyTorque(_playerBody3D.Transform.Basis.Y * -_playerBody3D.RotationSpeed);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("camera_one")) _playerCamera.MakeCurrent();
        if (@event.IsActionPressed("camera_two")) _worldCamera.MakeCurrent();
        
        if (@event.IsActionPressed("escape") && Input.MouseMode == Input.MouseModeEnum.Captured) Input.MouseMode = Input.MouseModeEnum.Visible;
        else if (@event.IsActionPressed("escape") && Input.MouseMode == Input.MouseModeEnum.Visible)Input.MouseMode = Input.MouseModeEnum.Captured;
        
        if (@event is InputEventMouseMotion motion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            _playerCameraMount.RotateY(motion.Relative.X * -_mouseSensitivity);
        }
    }
}