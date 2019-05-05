using Godot;
using System;

using System.Collections.Generic;

public class ShortestPathTest : Test
{
	public List<Vertex> VertexPath;
	public int NumHops;
	public float Distance;
	
	public Vertex Start;
	public Vertex End;
	
	public ShortestPath ShortestPathGetter;
	
	public string FilePath;

	public ShortestPathTest(String filePath, Vertex s, Vertex e)
	{
		Start = s;
		End = e;
		FilePath = filePath;
	}
	
	public override void Init(Constellation c)
	{
		TargetConstellation = c;
		Graph g = new Graph(TargetConstellation.GetAllVertsList().ToArray(),TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray());
		ShortestPathGetter = new ShortestPath(g,Start,End);
	
		CreateFile(FilePath);
		WriteLine("Distance,Numhops");
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
