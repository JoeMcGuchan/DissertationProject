using Godot;
using System;

public class AllBlack : ColouringMethod
{
	SpatialMaterial BlackMat;
	SpatialMaterial WhiteMat;
	
	public AllBlack()
	{
		BlackMat = new SpatialMaterial();
		BlackMat.AlbedoColor = new Color("333333");
		
		WhiteMat = new SpatialMaterial();
		WhiteMat.AlbedoColor = new Color("EEEEEE");
	}
	
	public override void ColourSat(Satellite sat)
    {
		if (sat.ID < 33) {sat.ThisMesh.MaterialOverride = BlackMat;}
		else {sat.ThisMesh.MaterialOverride = WhiteMat;}
	}
	
	public override void ColourBaseStation(BaseStation b) 
	{
		b.ThisMesh.MaterialOverride = BlackMat;
	}
	
	public override void ColourLink(Link l) 
	{
		if (((Satellite) l.V1).ID < 33) {l.Line.MaterialOverride = BlackMat;}
		else {l.Line.MaterialOverride = WhiteMat;}
	}
}
