using Godot;
using System;

public class ConnectedComponentsRemovingSatellites : Test
{
    // Simple test, returns the number of connected components
	Constellation Constellation;
	
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
	
	Main M;

	public ConnectedComponentsRemovingSatellites(String filePath, Constellation constellationNew, int nE, int nR, int nS, int t, int x, Main m)
	{
		CreateFile(filePath);
		
		Constellation = constellationNew;
		
		SatsInConstellationInititially = Constellation.GetAllSatsList().Count;
		SatsInConstellationCurent = SatsInConstellationInititially;
		
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
		
		M = m;
		
		WriteLine("NumOfSatsInConstellation,RepetitionNumber,SampleNumber,NumOfComponents");
		RunTest();
	}
	
	public override void Run()
	{
		if (LastTime != OS.GetUnixTime())
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
						
						//New experiment
						
						SatsInConstellationCurent -= SatsRemovedEachExperiment;
					} 
					
					//Repeat experiment
					M.Refresh();
					M.ThisConstellation.DisableSatellitesNumber(SatsInConstellationInititially - SatsInConstellationCurent);
				}
				
				RunTest();
			}
		}
	}
	
	void RunTest()
	{
		//Take another sample
		
		int numOfComponents = (
		new ConnectedComponentsAlgorithm(
				Constellation.GetAllSatsList().ToArray()
			)
				).Run();
		
		Constellation.SetMarkedAll(false);
				
		WriteLine(SatsInConstellationCurent+","+RepetitionNumber+","+SampleNumber+","+numOfComponents);
	}
}
