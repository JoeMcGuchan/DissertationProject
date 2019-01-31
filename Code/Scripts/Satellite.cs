using Godot;
using System;

public class Satellite : Spatial
{
	public Orbit orbit;
	public WorldEnvironment worldEnvironment;
	
	public int numOfLinks;
	
	public ImmediateGeometry[] links;
	public Satellite[] linkSats;
	public float[] linkDists;

	
	//Uniqe id [i,j,k] indicates the kth sat in the 
	//jth orbit of the ith sphere.
	public int[] id;
	
	public void Init(int i, int j, int k, Orbit newOrbit) 
	{
		id = new int[] {i, j, k};
		orbit = newOrbit;
	}
	
//	public override void _Ready()
//	{
//		numOfLinks = orbit.orbitalSphere.linksPerSat;
//
//		for (int n = 0; n < numOfLinks; n++)
//		{
//			ImmediateGeometry newLink = new ImmediateGeometry();
//			links[n] = newLink;
//			AddChild(newLink);
//		}
//	}
//
//	public void setLinks(Satellite[] linkSatsNew, float[] linkDistsNew)
//	{
//		linkSats = linkSatsNew;
//		linkDists = linkDistsNew;
//	}
}
