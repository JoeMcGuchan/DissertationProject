using Godot;
using System;

public class BaseStation : Vertex
{
    WorldEnvironment ThisWorldEnvironment;
	
	float Longditude;
	float Latitude;
	
	Constellation ThisConstellation;
	LinkingMethod ThisLinkingMethod;
	
	//The position that the satellite would be if 
	//Londitude 0 was on the x axis
	Vector3 BasePos;

    public override void _Process(float delta)
	{
		Translation = BasePos.Rotated(new Vector3(0,1,0),ThisConstellation.RotationOfEarth);
	}
	
	public void SetCoordinates(float lng, float lat)
	{
		Longditude = DegreeToRadian(lng);
		Latitude = DegreeToRadian(lat);
		
		BasePos = new Vector3(0,0,ThisWorldEnvironment.SizeOfEarth);
		BasePos = BasePos.Rotated(new Vector3(1,0,0),-Latitude);
		BasePos = BasePos.Rotated(new Vector3(0,1,0),Longditude);
	}

	public void Init(
		float lng,
		float lat,
		LinkingMethod linkingMethodNew,
		Constellation constellationNew, 
		WorldEnvironment newWorldEnvironment
	) {
		ThisWorldEnvironment = newWorldEnvironment;
		
		SetCoordinates(lng,lat);
		
		ThisLinkingMethod = linkingMethodNew;
		
		ThisConstellation = constellationNew;
		
		Translation = BasePos;
		
		base.Init();
	} 
	
	private float DegreeToRadian(float angle)
	{
   		return (float) Math.PI * angle / 180.0f;
	}
}
