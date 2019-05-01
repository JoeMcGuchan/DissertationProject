using Godot;
using System;

using System.Collections.Generic;

public class Satellite : Vertex
{
	public Orbit Orbit;
	public WorldEnvironment ThisWorldEnvironment;
	
	//number in orbit
	public int ID;
	
	//This is how far around it's orbit the satellite is initially
	public float AngleOffset;
	
	public void Init
	(
		int id,
		Orbit newOrbit,
		float angleOffset,
		WorldEnvironment newWorldEnvironment
	) {
		ThisWorldEnvironment = newWorldEnvironment;
		
		ID = id;
		Orbit = newOrbit;
		
		AngleOffset = angleOffset;
		
		base.Init();
	}
	
	new public void Delete()
	{
		Orbit.DeleteSat(this);		
		base.Delete();
	}
}
