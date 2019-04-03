using Godot;
using System;

public class HighlightMarked : ColouringMethod
{
	SpatialMaterial RedMat;
	SpatialMaterial BlackMat;
	
	public HighlightMarked()
	{
		RedMat = new SpatialMaterial();
		RedMat.AlbedoColor = new Color("ff0000");
		
		BlackMat = new SpatialMaterial();
		BlackMat.AlbedoColor = new Color("000000");
	}
	
	public override void ColourSat(Satellite sat)
    {
//		if (sat.Marked)
//		{
//			sat.SatMesh.MaterialOverride = RedMat;
//		}
//		else
//		{
//			sat.SatMesh.MaterialOverride = BlackMat;
//		}
//
//		for (int n = 0; n < sat.NumOfLinks; n++)
//		{
//			Link link = sat.Links[n];
//			if (link.Marked)
//			{
//				link.Line.MaterialOverride = RedMat;
//			}
//			else
//			{
//				link.Line.MaterialOverride = BlackMat;
//			}
//		}
	}
}
