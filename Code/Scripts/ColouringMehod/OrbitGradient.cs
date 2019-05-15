using Godot;
using System;

public class OrbitGradient : ColouringMethod
{
	SpatialMaterial[] Materials;
	
public OrbitGradient(int n)
	{
		Materials = new SpatialMaterial[n];
		
		for (int i = 0; i < n; i++)
		{
			float hue = (((float) i) / ((float) n));
			Color c = Color.FromHsv(hue, 1.0f, 1.0f);
			
			SpatialMaterial m = new SpatialMaterial();
			m.AlbedoColor = c;
			Materials[i] = m;
		}
	}
	
	public override void ColourBaseStation(BaseStation b) {}
	
	public override void ColourSat(Satellite sat)
	{
		sat.ThisMesh.MaterialOverride = Materials[sat.Orbit.ID];
	}
	
	public override void ColourLink(Link l) 
	{
		l.Line.MaterialOverride = Materials[((Satellite) l.V1).Orbit.ID];
	}
}