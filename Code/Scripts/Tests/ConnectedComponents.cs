using Godot;
using System;
using System.Collections.Generic;

public class ConnectedComponents : Test
{
    // Simple test, returns the number of connected components
	
	int NumOfComponents;

	public ConnectedComponents(String filePath)
	{
		Init(filePath);
	}
	
	public override void Run(Constellation constellation)
	{
		WriteLine("Run number " + RunNumber++);
		
		NumOfComponents = 0;
		
		List<Satellite> allSats = GetAllSatsList(constellation);
		List<Satellite>.Enumerator em = allSats.GetEnumerator();
		
		while(em.MoveNext())
		{
			Satellite rootSat = em.Current; 
			if (!rootSat.marked)
			{
				NumOfComponents++;
				Queue<Satellite> q = new Queue<Satellite>();
				q.Enqueue(rootSat);
				while(q.Count > 0)
				{
					Satellite sat = q.Dequeue();
					if (!sat.marked)
					{
						sat.marked = true;
						int numOfLinks = sat.numOfLinks;
						Link[] links = sat.Links;
						for (int i = 0; i < numOfLinks; i++)
						{
							Link link = links[i];
							if (link.Active) {q.Enqueue(links[i].Sat);}
						}
					}
				}
			}
		}
		
		List<Satellite>.Enumerator em2 = allSats.GetEnumerator();
		while(em2.MoveNext())
		{
			Satellite sat = em2.Current; 
			sat.marked = false;
		}
		
		WriteLine(""+ NumOfComponents);
		Console.WriteLine(NumOfComponents);
	}
}
