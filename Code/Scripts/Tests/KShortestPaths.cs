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
	
	public Vertex Start;
	public Vertex End;
	
	//Times for running repeatedly
	public int StartTime;
	public int LastTime;
	
	string FilePath;
	
	float Long1;
	float Lat1;
	float Long2;
	float Lat2;
	
	public KShortestPaths(String filePath, int k, float long1, float lat1, float long2, float lat2)
	{
		K = k;
		
		StartTime = OS.GetUnixTime();
		LastTime = StartTime;
		
		FilePath = filePath;
		
		Long1 = long1;
		Lat1 = lat1;
		Long2 = long2;
		Lat2 = lat2;
		
		RunContinually = false;
	}
	
	public override void Init(Constellation constellation)
	{
		TargetConstellation = constellation; 
		
		Start = constellation.AddBaseStation(Long1,Lat1);
		End = constellation.AddBaseStation(Long2,Lat2);
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
