using Godot;
using System;

using System.Collections.Generic;

public class KShortestPaths : Test
{
    // get k shortests paths with Yen's algoritm
	
	public List<Vertex>[] VertexPaths;
	public int[] NumHops;
	public float[] Distance;
	
	public int K;
	
	public Constellation TargetConstellation;
	public Vertex Start;
	public Vertex End;
	
	//Times for running repeatedly
	public int StartTime;
	public int LastTime;
	
	
	public KShortestPaths(String filePath, int k)
	{
		K = k;
		
		StartTime = OS.GetUnixTime();
		LastTime = StartTime;
		
		String str = "Time";
		
		WriteLine(str);
	}
	
	public void SetConstellation(Constellation constellation, Vertex start, Vertex end)
	{
		TargetConstellation = constellation; 
		Start = start;
		End = end;
	}
	
	public override void Run()
	{
		int time = OS.GetUnixTime();
			
		if (time != LastTime)
		{
			LastTime = time;
			
			String str = "" + (time - StartTime);
			

			WriteLine(str);
		}
	}
}
