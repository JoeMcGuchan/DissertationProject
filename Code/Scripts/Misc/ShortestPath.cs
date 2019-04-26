using Godot;
using System;

using System.Collections.Generic;

public class ShortestPath
{
	Graph G;
	Vertex Source;
	Vertex Sink;
	
	public ShortestPath(Graph g, Vertex s, Vertex t)
	{
		G = g;
		Source = s;
		Sink = t;
	}
	
	//It will usually be necessary to refresh the links before running
	public void SetLinks(Link[] l)
	{
		G.Links = l;
	}
	
	public VertexPath Run()
	{
		//create dictionary for distances
		System.Collections.Generic.Dictionary<Vertex, float> dists = new System.Collections.Generic.Dictionary<Vertex, float>();
		
		//vertex linking to on shortest path
		System.Collections.Generic.Dictionary<Vertex, Vertex> prevVert = new System.Collections.Generic.Dictionary<Vertex, Vertex>();
		
		//link to on shortest path
		System.Collections.Generic.Dictionary<Vertex, Link> prevLink = new System.Collections.Generic.Dictionary<Vertex, Link>();

		//initiate dictionaries
		foreach (Vertex vert in G.Verticies)
		{
			prevVert.Add(vert,null);
			prevLink.Add(vert,null);
			
			if (vert == Source) 
			{
				dists.Add(vert,0f);
			}
			else
			{
				dists.Add(vert,float.MaxValue);
			}
		}
		
		//Create priority queue of all verticies
		VertexPriorityQueue q = new VertexPriorityQueue(G.Verticies.Length);
		
		foreach (Vertex vert in G.Verticies)
		{
			q.Enqueue(vert,dists[vert]);
		}
		
		//while Q is not empty
		while (!q.empty())
		{
			Vertex u = q.dequeue_min();
			
			foreach (Link l in u.Links)
			{
				Vertex vert = l.V1;
				
				//check to make sure I've got the right end of the link
				if (vert == u) {vert = l.V2;}
				
				float alt = dists[u] + l.Dist;
				
				if (alt < dists[vert])
				{
					dists[vert] = alt;
					prevVert[vert] = u;
					prevLink[vert] = l;
					
					q.decrease_priority(vert,alt);
				}
			}
		}
		
		List<Vertex> VertexPathVerts = new List<Vertex>();
		List<Link> VertexPathLinks = new List<Link>();
		
		Vertex v = Sink;
		
		VertexPathVerts.Add(v);
		
		while (!(prevVert[v] is null))
		{
			VertexPathLinks.Add(prevLink[v]);
			
			v = prevVert[v];
			VertexPathVerts.Add(v);
		}
		
		return new VertexPath(VertexPathVerts.ToArray(), VertexPathLinks.ToArray(), dists[Sink]);
	} 
}
