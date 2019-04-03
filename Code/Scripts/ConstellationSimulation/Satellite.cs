using Godot;
using System;

using System.Collections.Generic;

public class Satellite : Spatial
{
	public Orbit Orbit;
	public WorldEnvironment ThisWorldEnvironment;
	
	public MeshInstance SatMesh;
	
	//number in orbit
	public int ID;
	
	//used for algorithms
	public bool Marked = false;
	
	public bool Active = false;
	
	public List<Link> Links;
	
	public override void _Ready()
	{
		SatMesh = (MeshInstance) FindNode("MeshInstance");
	}
	
	public void Init
	(
		int id,
		Orbit newOrbit,
		WorldEnvironment newWorldEnvironment
	) {
		ThisWorldEnvironment = newWorldEnvironment;
		
		ID = id;
		Orbit = newOrbit;
		
		Links = new List<Link>();
	}
	
	public void AddLink(Link l)
	{
		Links.Add(l);
	}
}
