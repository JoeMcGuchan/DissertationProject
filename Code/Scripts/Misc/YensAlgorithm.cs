using Godot;
using System;

public class YensAlgorithm
{
	Graph G;
	Vertex Source;
	Vertex Sink;
	int K;
	
	public YensAlgorithm(Graph g, Vertex source, Vertex sink, int k)
	{
		G = g;
		Source = source;
		Sink = sink;
		K = k;
	}
	
	//run Yen's Algoritm
	private VertexPath[] Run()
	{
		//Clone graph (as we will be editing it)
		
		Graph g = new Graph(G);
		
		VertexPath[] A = new VertexPath[K];
		A[0] = (new ShortestPath(g, Source, Sink)).Run();
		
		PathSetSortable B = new PathSetSortable();
		
		for (int k = 1; k < K; k++)
		{
			for (int i = 0; i < A[k-1].NumVerts() - 2; i++)
			{
				Vertex spur = A[k-1].GetVertex(i);
				
				//VertexPath rootpath = A[k-1].
			}
		}
		
		return null;
	}
}
