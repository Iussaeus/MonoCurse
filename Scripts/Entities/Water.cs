using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class Water : MeshInstance3D
{
	private ShaderMaterial _material;
	private Image _noiseImage;

	private float _noiseScale;
	private float _waveSpeed;
	private float _heightScale;

	private float _time;

	public override void _Ready()
	{
		_material = (ShaderMaterial)Mesh.SurfaceGetMaterial(0);
		var noiseTexture = (NoiseTexture2D)_material.GetShaderParameter("wave");
		_noiseImage = noiseTexture.Noise.GetSeamlessImage(512, 512);
		_waveSpeed = (float)_material.GetShaderParameter("wave_speed");
		_heightScale = (float)_material.GetShaderParameter("height_scale");
		_noiseScale = (float)_material.GetShaderParameter("noise_scale");
	}

	public override void _Process(double delta)
	{
		_time += (float)delta;
		_material.SetShaderParameter("wave_time", _time);
	}
	
	public float GetHeight(Vector3 worldPosition)
	{
		var uvX = Mathf.Wrap(worldPosition.X / _noiseScale + _time * _waveSpeed, 0, 1);
		var uvY = Mathf.Wrap(worldPosition.Z / _noiseScale + _time * _waveSpeed, 0, 1);

		var pixelPosition = new Vector2(uvX * _noiseImage.GetWidth(), uvY * _noiseImage.GetHeight());

		return GlobalTransform.Origin.Y + _noiseImage.GetPixelv((Vector2I)pixelPosition).R  * _heightScale;
	}
}