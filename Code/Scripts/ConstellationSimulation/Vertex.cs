using Godot;
using System;

using System.Collections.Generic;

public class Vertex : Spatial
{
	//this base class describes all verticies in the graph
    public bool Marked = false;
	
	public MeshInstance ThisMesh;
	
	public List<Link> Links;
	
	public override void _Ready()
	{
		ThisMesh = (MeshInstance) FindNode("MeshInstance");
	}
	
    public void Init()
    {
       Links = new List<Link>(); 
    }
	
	public void AddLink(Link l)
	{
		Links.Add(l);
	}
	
	public void DeleteLink(Link l)
	{
		Links.Remove(l);
		l.QueueFree();
	}
	
	public void ClearLinks()
	{
		foreach (Link l in Links)
		{
			l.QueueFree();
		}
		
		Links.Clear();
	}
}
