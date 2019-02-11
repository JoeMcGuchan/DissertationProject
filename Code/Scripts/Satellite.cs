using Godot;
using System;

public class Link
{
	public bool Active;
	public ImmediateGeometry Line;
	public Satellite Sat;
	public float Dist;
}

public class Satellite : Spatial
{
	public Orbit orbit;
	public WorldEnvironment worldEnvironment;
	
	public int numOfLinks;
	
	public Link[] Links;
	
	public MeshInstance satMesh;
	
	//TODO have colour functioning properly
	Color color;
	
	//Uniqe id [i,j,k] indicates the kth sat in the 
	//jth orbit of the ith sphere.
	public int[] id;
	
	public override void _Ready()
	{
		satMesh = (MeshInstance) FindNode("MeshInstance");
	}
	
	public void Init
	(
		int i, 
		int j,
		int k,
		Orbit newOrbit,
		WorldEnvironment newWorldEnvironment) 
	{
		worldEnvironment = newWorldEnvironment;
		
		id = new int[] {i, j, k};
		orbit = newOrbit;
	}
	
	public void SetLinks(Satellite[] linkSats, float[] linkDists)
	{
		for (int i = 0; i < numOfLinks; i++) {SetLink(i,linkSats[i],linkDists[i]);}
	}
	
	public void SetLink(int i, Satellite linkSat, float linkDist)
	{
		Link link = Links[i];
		
		link.Sat = linkSat;
		link.Dist = linkDist;
		link.Active = true;

		DrawLink(i, linkSat);
	}
	
	public void UpdateLink(int i, float linkDist)
	{
		Link link = Links[i];
		Satellite linkSat = link.Sat;
		link.Dist = linkDist;
		link.Active = true;
		
		DrawLink(i, linkSat);
	}
	
	public void ClearLink(int i)
	{
		Link link = Links[i];
		link.Active = false;
		link.Line.Clear();
	}
	
	public void DrawLink(int i, Satellite linkSat)
	{
		ImmediateGeometry link = Links[i].Line;
		link.Clear();
		link.Begin(Mesh.PrimitiveType.Lines);
		link.AddVertex(new Vector3(0,0,0));
		link.SetColor(color);
		link.AddVertex(ToLocal(linkSat.Translation));
		link.End();
	}
	
	public void CreateLinks(int numOfLinksNew)
	{
		numOfLinks = numOfLinksNew;

		Links = new Link[numOfLinks];

		for (int n = 0; n < numOfLinks; n++)
		{
			Link link = new Link();
			link.Active = false;
			ImmediateGeometry line = new ImmediateGeometry();
			link.Line = line;
			Links[n] = link;
			AddChild(line);
		}

		ColorByOrbit();
	}
	
	public void ColorByOrbit()
	{
		Color white = new Color("ffffff");
		Color red = new Color("ff0000");
		float interpolationFactor = (float) Math.Abs(((float) id[1] * 2f) / (orbit.orbitalSphere.numOfOrbits) - 1f);
		color = white.LinearInterpolate(red, interpolationFactor);
		SpatialMaterial newMaterial = new SpatialMaterial();
		newMaterial.AlbedoColor = color;
		satMesh.MaterialOverride = newMaterial;
	}
}
