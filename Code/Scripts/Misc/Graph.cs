using Godot;
using System;

using System.Collections.Generic;

public class Graph
{
	public Vertex[] Verticies;
	public Link[] Links;
	
	public Graph(Vertex[] v, Link[] l) 
	{
		Verticies = v;
		Links = l;
	}
	
	public Graph(Graph g)
	{
		Verticies = g.Verticies;
		Links = g.Links;
	}
	
	public void RemoveEdge(Link l)
	{
		List<Link> linksList = new List<Link>(Links);
		linksList.Remove(l);
		Links = linksList.ToArray();
	}
}
