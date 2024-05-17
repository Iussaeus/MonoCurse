using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class ProjectileBody3D : FloatingBody3D
{
    [Export] public float Damage = 10f;
    [Export] public float ProjectileSpeed = 70f;

    public override void _Ready()
    {
        base._Ready();
        ContactMonitor = true;
        MaxContactsReported = 100;
        BodyEntered += OnBodyEntered;
    }
    
    private void OnBodyEntered(Node body)
    {
        base._Ready();
        if (body is DamageableBody3D damageableBody3D)
        {
            damageableBody3D.TakeDamage(Damage);
        }
    }
}