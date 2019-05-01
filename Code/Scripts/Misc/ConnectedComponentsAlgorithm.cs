using Godot;
using System;

using System.Collections.Generic;

public class ConnectedComponentsAlgorithm
{
	Vertex[] Verticies;
	
	public ConnectedComponentsAlgorithm(Vertex[] g)
	{
		Verticies = g;
	}
	
	public int Run()
	{
		int numOfComponents = 0;
		
		foreach (Vertex root in Verticies)
		{
			if (!root.Marked)
			{
				numOfComponents++;
				Queue<Vertex> q = new Queue<Vertex>();
				q.Enqueue(root);
				while(q.Count > 0)
				{
					Vertex v = q.Dequeue();
					if (!v.Marked)
					{
						v.Marked = true;

						foreach (Vertex u in v.GetLinkedVerticies())
						{
							q.Enqueue(u);
						}
					}
				}
			}
		}

		return numOfComponents;
	}
	
	
}