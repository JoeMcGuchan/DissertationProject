using Godot;
using System;

public class RepeatedShortestPath : Test
{
    // Simple test, returns the number of connected components
	
	int LastTime;
	
	//Number of times to delete all sattelites
	int NExperiments;
	
	//Number of times to run each experiment
	int NRepetitions;
	
	//Number of samples to take in each experiment
	int NSamples;
	
	//Time difference (in seconds) between each sample in the experiment
	int TimeBetweenExperiments;
	
	int SatsInConstellationInititially;
	int SatsInConstellationCurent;
	
	//Number of satellites to delete after each experiment
	int SatsRemovedEachExperiment;
	
	int TimeCounter;
	
	int SampleNumber;
	int RepetitionNumber;
	int ExperimentNumber;
	
	bool TestOver = false;
	
	Main M;

	float Lat1;
	float Lat2;
	float Lng1;
	float Lng2;

	public RepeatedShortestPath(String filePath, int nE, int nR, int nS, int t, int x, float lng1, float lat1, float lng2, float lat2)
	{
		CreateFile(filePath);
		
		TimeCounter = 0;
		SampleNumber = 0;
		RepetitionNumber = 0;
		ExperimentNumber = 0;
		
		NExperiments = nE;
		NRepetitions = nR;
		NSamples = nS;
		
		TimeBetweenExperiments = t;
		
		SatsRemovedEachExperiment = x;
		
		LastTime = OS.GetUnixTime();
		
		RunContinually = true;
		
		Lng1 = lng1;
		Lat1 = lat1;
		Lng2 = lng2;
		Lat2 = lat2;
	}
	
	public override void Init(Constellation c)
	{
		TargetConstellation = c;
		
		SatsInConstellationInititially = TargetConstellation.GetAllSatsList().Count;
		SatsInConstellationCurent = SatsInConstellationInititially;
		
		M = (Main) c.GetParent();
		
		WriteLine("NumOfSatsInConstellation,RepetitionNumber,SampleNumber,PathLengthHops,PathLengthDist");
		RunTest();
	}
	
	public override void Run()
	{
		if (LastTime != OS.GetUnixTime() && !TestOver)
		{
			LastTime = OS.GetUnixTime();
			
			TimeCounter++;
			
			if (TimeCounter == TimeBetweenExperiments)
			{
				TimeCounter = 0;
				
				SampleNumber++;
				
				if (SampleNumber == NSamples)
				{
					SampleNumber = 0;
					
					RepetitionNumber++;
					
					if (RepetitionNumber == NRepetitions)
					{
						RepetitionNumber = 0;
						
						ExperimentNumber++;
						
						if (ExperimentNumber == NExperiments) {TestOver = true; return;}
						
						SatsInConstellationCurent -= SatsRemovedEachExperiment;
					} 
					
					//Repeat experiment
					M.RefreshConstellation();
					TargetConstellation.DisableSatellitesNumber(SatsInConstellationInititially - SatsInConstellationCurent);
				}
				
				RunTest();
			}
		}
	}
	
	void RunTest()
	{
		//Take another sample
		
		TargetConstellation.SetMarkedAll(false);
		
		Vertex V1 = TargetConstellation.AddBaseStation(Lng1,Lat1);
		Vertex V2 = TargetConstellation.AddBaseStation(Lng2,Lat2);
		
		VertexPath shortPath = (
		new ShortestPath(
				new Graph(
					TargetConstellation.GetAllSatsList().ToArray(),
					TargetConstellation.ThisLinkingMethod.GetAllLinks().ToArray()
				), 
				V1, 
				V2
			)
		).Run();
		
		WriteLine(SatsInConstellationCurent+","+RepetitionNumber+","+SampleNumber+","+shortPath.NumVerts()+","+shortPath.GetDist());
	}
}
