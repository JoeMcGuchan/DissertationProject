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
	
	public void SetLinks(Sattelite[] linkSats, float[] linkDists)
	{
		for (int i; i < numOfLinks; i++) {SetLink(i,linkSats[i],linkDists[i]);}
	}
	
	public void SetLink(int i, Satellite linkSat, float linkDist)
	{
		linkSats[i] = linkSat;
		linkDists[i] = linkDist;
		
		link = links[i];
		link.Begin(1);s
		link.AddVertex(Vector3(0));
		link.AddVertex(ToLocal(linkSat.Translation);
		link.End();
	}
	
	public void CreateLinks(numOfLinks)
	{
		for (int n = 0; n < numOfLinks; n++)
		{
			ImmediateGeometry newLink = new ImmediateGeometry();
			links[n] = newLink;
			AddChild(newLink);
		}
	}
	
	public override _Draw()
	{
		for (int i = 0; i < numOfLinks; i++)
		{
			link = links[i];
			link.Begin(1);
			link.AddVertex(new Vector3(0,0,0));
			link.AddVertex(ToLocal(linkPositions[i]));
		}
	}
}
