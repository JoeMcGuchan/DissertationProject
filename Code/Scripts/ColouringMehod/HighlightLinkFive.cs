using Godot;
using System;

public class HighlightLinkFive : ColouringMethod
{
	SpatialMaterial SatMat;
	SpatialMaterial LinkMat1;
	SpatialMaterial LinkMat2;
	
	public HighlightLinkFive()
	{
		SatMat = new SpatialMaterial();
		SatMat.AlbedoColor = new Color("111111");
		
		LinkMat1 = new SpatialMaterial();
		LinkMat1.AlbedoColor = new Color("eeeeee");
		
		LinkMat2 = new SpatialMaterial();
		LinkMat2.AlbedoColor = new Color("ff0000");
	}
	
	public override void ColourBaseStation(BaseStation b) {}
	
	public override void ColourSat(Satellite sat)
    {
//		sat.SatMesh.MaterialOverride = SatMat;
//
//		for (int n = 0; n < sat.NumOfLinks; n++)
//		{
//			if (n < 4)
//			{
//				sat.Links[n].Line.MaterialOverride = LinkMat1;
//			}
//			else
//			{
//				sat.Links[n].Line.MaterialOverride = LinkMat2;
//			}
//		}
	}
	
	public override void ColourLink(Link l) {}
}
