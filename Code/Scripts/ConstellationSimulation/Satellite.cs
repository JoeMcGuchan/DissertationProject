using Godot;
using System;

using System.Collections.Generic;

public class Satellite : Vertex
{
	public Orbit Orbit;
	public WorldEnvironment ThisWorldEnvironment;
	
	//number in orbit
	public int ID;
	
	public void Init
	(
		int id,
		Orbit newOrbit,
		WorldEnvironment newWorldEnvironment
	) {
		ThisWorldEnvironment = newWorldEnvironment;
		
		ID = id;
		Orbit = newOrbit;
		
		base.Init();
	}
}
