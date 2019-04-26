using Godot;
using System;
using System.Collections.Generic;

public class ClosestToPoint : Test
{
    // Simple test, gets the closest satellite to a given point
	
	List<Vertex> AllSatellites;
	Vector3 ClosePoint;
	float CurrentDistance;
	Satellite ClosestSatCurrent;
	
	Constellation ThisConstellation;
	
	public void Init(String filePath, Constellation constellation, Vector3 point)
	{
		CreateFile(filePath);
		
		AllSatellites = constellation.GetAllVertsList();
		ClosePoint = point;
		
		ThisConstellation = constellation;
	}
	
	public void SetPoint(Vector3 point)
	{
		ClosePoint = point;
	}
	
	public Satellite GetClosestSat()
	{
		return ClosestSatCurrent;
	}
	
	public override void Run()
	{
		WriteLine("Run number " + RunNumber++);

		CurrentDistance = float.MaxValue;

		foreach (Satellite sat in AllSatellites)
		{
			Vector3 point = sat.Translation;
			float dist = point.DistanceTo(ClosePoint);

			if (dist < CurrentDistance)
			{
				CurrentDistance = dist;
				ClosestSatCurrent = sat;
			}
			
			sat.Marked = false;
		}
		
		ClosestSatCurrent.Marked = true;

		WriteLine("Closest sat to: "+ClosePoint+" is ["+ClosestSatCurrent.ID
			+","+ClosestSatCurrent.Orbit.ID
			+","+ClosestSatCurrent.Orbit.OrbitalSphere.ID+
			"] at distance "+CurrentDistance);
			
		ThisConstellation.ApplyColouringMethod();
	}
	
	
}
