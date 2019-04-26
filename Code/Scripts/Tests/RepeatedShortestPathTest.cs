using Godot;
using System;

public class RepeatedShortestPathTest : Test
{
    // Repeatedly finds the shortest path every second for k seconds
	
	public VertexPath ShortPath;
	
	public Constellation TargetConstellation;
	public Vertex Start;
	public Vertex End;
	
	public ShortestPath ShortestPathGetter;
	
	public int MaxTime;
	public int LastTime;
	//Counts up until max time
	public int TimeCount;
	public int StartTime;
	
	//time gap between each run (realtime, minutes)
	public int T;
	
	public int NumHops;
	public float Dist;

	public RepeatedShortestPathTest(String filePath)
	{
		CreateFile(filePath);
		WriteLine("Time,Distance,Numhops");
	}
	
	public void SetConstellation(Constellation constellation, Vertex start, Vertex end, int n, int t)
	{
		TargetConstellation = constellation; 
		Start = start;
		End = end;
		
		Graph g = new Graph(TargetConstellation.GetAllVertsList().ToArray(),TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray());
		ShortestPathGetter = new ShortestPath(g,Start,End);
		
		TargetConstellation.SetMarkedAll(false);
		TargetConstellation.ThisLinkingMethod.SetMarkedAll(false);
	
		T = t;
		
		StartTime = OS.GetUnixTime() / T;
		LastTime = StartTime;
		MaxTime = n;
		TimeCount = 0;
		
		ShortPath = ShortestPathGetter.Run();
		Dist = ShortPath.GetDist();
		NumHops = ShortPath.NumVerts();
		
		WriteLine("0,"+Dist+","+NumHops);
	}
	
	public override void Run()
	{
		//divide by t to maintain time stem
		int time = OS.GetUnixTime() / T;
		
		if (time != LastTime)
		{
			LastTime = time;
			
			RunShortestPath();
			
			WriteLine((LastTime-StartTime)+","+Dist+","+NumHops);
			
			TimeCount++;
		}
		
		if (TimeCount>MaxTime)
		{
			TargetConstellation.GetTree().Quit();
		}
	} 
	
	public void RunShortestPath()
	{
		ShortestPathGetter.SetLinks(TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray());
		
		ShortPath.SetMarkedAll(false);
		TargetConstellation.ThisColouringMethod.ColourVertexPath(ShortPath);
		
		ShortPath = ShortestPathGetter.Run();
		
		ShortPath.SetMarkedAll(true);
		TargetConstellation.ThisColouringMethod.ColourVertexPath(ShortPath);
		
		Dist = ShortPath.GetDist();
		NumHops = ShortPath.NumVerts();
	}
}
