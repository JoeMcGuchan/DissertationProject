using Godot;
using System;

using System.Collections.Generic;

public class ShortestPathTest : Test
{
	public List<Vertex> VertexPath;
	public int NumHops;
	public float Distance;
	
	public Constellation TargetConstellation;
	public Vertex Start;
	public Vertex End;
	
	public ShortestPath ShortestPathGetter;

	public ShortestPathTest(String filePath)
	{
		CreateFile(filePath);
		WriteLine("Distance,Numhops");
	}
	
	public void SetConstellation(Constellation constellation, Vertex start, Vertex end)
	{
		TargetConstellation = constellation; 
		Start = start;
		End = end;
		
		Graph g = new Graph(TargetConstellation.GetAllVertsList().ToArray(),TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray());
		ShortestPathGetter = new ShortestPath(g,Start,End);
	}
	
	public override void Run()
	{
		ShortestPathGetter.SetLinks(TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray());
		
		VertexPath result = ShortestPathGetter.Run();
		
		TargetConstellation.SetMarkedAll(false);
		
		TargetConstellation.ThisLinkingMethod.SetMarkedAll(false);
		
		result.SetMarkedAll(true);
		
		TargetConstellation.ApplyColouringMethod();
		
		WriteLine(result.GetDist() + "," + result.NumVerts());
	}
}
