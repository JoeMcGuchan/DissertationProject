using Godot;
using System;

public class HighlightMarked : ColouringMethod
{
	SpatialMaterial RedMat;
	SpatialMaterial GreyMat;
	SpatialMaterial BlackMat;
	
	public HighlightMarked()
	{
		RedMat = new SpatialMaterial();
		RedMat.AlbedoColor = new Color("ff0000");
		
		GreyMat = new SpatialMaterial();
		GreyMat.AlbedoColor = new Color("444444");
		
		BlackMat = new SpatialMaterial();
		BlackMat.AlbedoColor = new Color("000000");
	}
	
	public override void ColourSat(Satellite sat)
    {
		if (sat.Marked)
		{
			sat.SatMesh.MaterialOverride = RedMat;
		}
		else
		{
			sat.SatMesh.MaterialOverride = GreyMat;
		}
	}
	
	public override void ColourLink(Link l) 
	{
		if (l.Marked)
		{
			l.Line.MaterialOverride = RedMat;
		}
		else
		{
			l.Line.MaterialOverride = BlackMat;
		}
	}
}
