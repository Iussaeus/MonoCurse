using Godot;
using Godot.Collections;

namespace PirateCurse.Scripts.Entities;

[GlobalClass]
public partial class FloatingBody3D : RigidBody3D
{
	[Export] private float _floatForce = 1.0f;
	[Export] private float _waterDrag = 0.05f;
	[Export] private float _waterAngularDrag = 0.05f;

	private float _gravity;
	private Node _water;

	private Array<Node> _probes;
	private bool _submerged;
	
	public override void _Ready()
	{
		_water = GetNode("/root/World/SubViewportContainer/SubViewport/WaterPlane");
		_gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
		
		var probeContainer = GetNodeOrNull("ProbeContainer");
		if (probeContainer != null)
		{
			_probes = probeContainer.GetChildren(true);
		}
	}

	public override void _Process(double delta)
	{
		_submerged = false;

		if (_probes == null) return;
		{
			var depth = (float)_water.Call("GetHeight", GlobalPosition) - GlobalPosition.Y;
			if (depth > 0)
			{
				_submerged = true;
				ApplyCentralForce(Vector3.Up * _floatForce * _gravity * depth);
			}
		}

		if (_probes == null) return;
		{
			foreach (var node in _probes)
			{
				var probe = (Marker3D)node;
				{
					var depth = (float)_water.Call("GetHeight", probe.GlobalPosition) - probe.GlobalPosition.Y;
					if (depth < 0) continue;
					_submerged = true;
					ApplyForce(Vector3.Up * _floatForce * _gravity * depth, probe.GlobalPosition - GlobalPosition);
				}
			}
		}
	}

	public override void _IntegrateForces(PhysicsDirectBodyState3D state)
	{
		if (!_submerged) return;
		state.LinearVelocity *= (1 - _waterDrag);
		state.AngularVelocity *= (1 - _waterAngularDrag);
	}
}