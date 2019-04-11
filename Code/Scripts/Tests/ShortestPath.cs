using Godot;
using System;

using System.Collections.Generic;

public class ShortestPath : Test
{
    // Uses Dijkstra's Algorithm to get a shortest path
	
	/*
	Mark all nodes unvisited. 
	Create a set of all the unvisited nodes called the unvisited set.
	Assign to every node a tentative distance value: set it to zero for our initial node and to infinity for all other nodes.
	Set the initial node as current.
	For the current node, consider all of its unvisited neighbours and calculate their tentative distances through the current node.
	Compare the newly calculated tentative distance to the current assigned value and assign the smaller one.
	For example, if the current node A is marked with a distance of 6, and the edge connecting it with a neighbour B has length 2, then the distance to B through A will be 6 + 2 = 8. If B was previously marked with a distance greater than 8 then change it to 8. Otherwise, keep the current value.
	When we are done considering all of the unvisited neighbours of the current node, mark the current node as visited and remove it from the unvisited set. A visited node will never be checked again.
	If the destination node has been marked visited (when planning a route between two specific nodes) or if the smallest tentative distance among the nodes in the unvisited set is infinity (when planning a complete traversal; occurs when there is no connection between the initial node and remaining unvisited nodes), then stop. The algorithm has finished.
	Otherwise, select the unvisited node that is marked with the smallest tentative distance, set it as the new "current node", and go back to step 3.
	*/
	
	public List<Vertex> VertexPath;
	public int NumHops;
	public float Distance;
	
	public Constellation TargetConstellation;
	public Vertex Start;
	public Vertex End;

	public ShortestPath(String filePath)
	{
		CreateFile(filePath);
	}
	
	public void SetConstellation(Constellation constellation, Vertex start, Vertex end)
	{
		TargetConstellation = constellation; 
		Start = start;
		End = end;
	}
	
	public override void Run()
	{
		WriteLine("Run number " + RunNumber++);
		
		//create dictionary for distances
		System.Collections.Generic.Dictionary<Vertex, float> dists = new System.Collections.Generic.Dictionary<Vertex, float>();
		System.Collections.Generic.Dictionary<Vertex, Vertex> prev = new System.Collections.Generic.Dictionary<Vertex, Vertex>();
		
		foreach (Vertex vert in GetAllVertsList(TargetConstellation))
		{
			prev.Add(vert,null);
			
			if (vert == Start) 
			{
				dists.Add(vert,0f);
			}
			else
			{
				dists.Add(vert,float.MaxValue);
			}
		}
		
		//create a dictionary for previous vertexes

		
		//Create priority queue of all verticies
		VertexPriorityQueue q = new VertexPriorityQueue(GetAllVertsList(TargetConstellation).Count);
		
		foreach (Vertex vert in GetAllVertsList(TargetConstellation))
		{
			q.Enqueue(vert,dists[vert]);
			vert.Marked = false;
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
					prev[vert] = u;
					
					q.decrease_priority(vert,alt);
				}
			}
		}
		
		//Now get the path 
		Distance = dists[End];
		
		NumHops = 0;
		VertexPath = new List<Vertex>();
		
		Vertex v = End;
		
		NumHops++;
		VertexPath.Add(v);
		v.Marked = true;
		
		while (!(prev[v] is null))
		{
			v = prev[v];
			NumHops++;
			VertexPath.Add(v);
			v.Marked = true;
		}
		
		WriteLine("Distance = "+Distance);
		WriteLine("NumHops = "+NumHops);
	}
}
