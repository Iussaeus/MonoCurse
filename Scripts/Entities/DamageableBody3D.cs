using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class DamageableBody3D : FloatingBody3D
{
    [Export] public float Health = 100;
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        GD.Print("Took damage:", damage, ", hp:", Health );
        if (Health == 0) QueueFree();
    }
}