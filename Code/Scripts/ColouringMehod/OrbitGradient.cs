using Godot;
using System;

public class OrbitGradient : ColouringMethod
{
    Color Black;
	Color Red;
	
	SpatialMaterial LinkMat;
	
	public OrbitGradient()
	{
		Black = new Color("000000");
		Red = new Color("ff0000");
		
		LinkMat = new SpatialMaterial();
		LinkMat.AlbedoColor = new Color("eeeeee");
	}
	
	public override void ColourSat(Satellite sat)
    {
		float interpolationFactor = (float) Math.Abs(((float) sat.id[1] * 2f) / (sat.orbit.orbitalSphere.numOfOrbits) - 1f);
		Color color = Black.LinearInterpolate(Red, interpolationFactor);
		SpatialMaterial newMaterial = new SpatialMaterial();
		newMaterial.AlbedoColor = color;
		sat.satMesh.MaterialOverride = newMaterial;
		
		for (int n = 0; n < sat.numOfLinks; n++)
		{
			sat.Links[n].Line.MaterialOverride = LinkMat;
		}
	}
}
