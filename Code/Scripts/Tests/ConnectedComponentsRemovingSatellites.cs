using Godot;
using System;

public class ConnectedComponentsRemovingSatellites : Test
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

	public ConnectedComponentsRemovingSatellites(String filePath, int nE, int nR, int nS, int t, int x)
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
	}
	
	public override void Init(Constellation c)
	{
		TargetConstellation = c;
		
		SatsInConstellationInititially = TargetConstellation.GetAllSatsList().Count;
		SatsInConstellationCurent = SatsInConstellationInititially;
		
		M = (Main) c.GetParent();
		
		WriteLine("NumOfSatsInConstellation,RepetitionNumber,SampleNumber,NumOfComponents");
	}
	
	public override void Run()
	{
		if (LastTime != OS.GetUnixTime() && !TestOver)
		{
			LastTime = OS.GetUnixTime();
			
			TimeCounter++;
			
			if (TimeCounter >= TimeBetweenExperiments)
			{
				TimeCounter = 0;
				
				if (SampleNumber >= NSamples)
				{
					SampleNumber = 0;
					
					if (RepetitionNumber >= NRepetitions-1)
					{
						RepetitionNumber = 0;
						
						if (ExperimentNumber >= NExperiments-1) 
						{
							TestOver = true;
							Close();
							M.LoadNext();
						}
						else
						{
							SatsInConstellationCurent -= SatsRemovedEachExperiment;
							ExperimentNumber++;
							M.RefreshConstellation();
							TargetConstellation.DisableSatellitesNumber(SatsInConstellationInititially - SatsInConstellationCurent);
						}
					}
					else
					{
						//Repeat experiment
						RepetitionNumber++;
						M.RefreshConstellation();
						TargetConstellation.DisableSatellitesNumber(SatsInConstellationInititially - SatsInConstellationCurent);
					}
				} 
				else 
				{
					SampleNumber++;
					RunTest();
				}
			}
		}
	}
	
	void RunTest()
	{
		//Take another sample
		
		TargetConstellation.SetMarkedAll(false);
		
		int numOfComponents = (
		new ConnectedComponentsAlgorithm(
				TargetConstellation.GetAllSatsList().ToArray()
			)
		).Run();
		
		WriteLine(SatsInConstellationCurent+","+RepetitionNumber+","+SampleNumber+","+numOfComponents);
	}
}
