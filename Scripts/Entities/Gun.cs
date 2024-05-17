using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class Gun : Node3D
{
    [Export] private float _gunPower = 10;
    [Export] private PackedScene _projectile;

    private Marker3D _gunPoint;
    

    public override void _Ready()
    {
        _gunPoint = GetNode<Marker3D>("Marker3D");
    }

    public void Shoot()
    {
        var bullet = (ProjectileBody3D)_projectile.Instantiate();
        bullet.ProjectileSpeed *= _gunPower;
        AddChild(bullet);
        bullet.ApplyImpulse(-_gunPoint.GlobalBasis.Z * bullet.ProjectileSpeed, -_gunPoint.GlobalBasis.Z + _gunPoint.GlobalBasis.X);
    }
}