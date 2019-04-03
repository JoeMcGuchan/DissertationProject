using Godot;
using System;
using System.Collections.Generic;

public class ClosestToPoint : Test
{
    // Simple test, gets the closest satellite to a given point
	
	List<Satellite> AllSatellites;
	Vector3 ClosePoint;
	float CurrentDistance;
	Satellite ClosestSatCurrent;
	
	public void Init(String filePath, Constellation constellation, Vector3 point)
	{
		CreateFile(filePath);
		
		AllSatellites = GetAllSatsList(constellation);
		ClosePoint = point;
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
//		WriteLine("Run number " + RunNumber++);
//
//		CurrentDistance = float.MaxValue;
//
//		foreach (Satellite sat in AllSatellites)
//		{
//			Vector3 point = sat.Translation;
//			float dist = point.DistanceTo(ClosePoint);
//
//			if (dist < CurrentDistance)
//			{
//				CurrentDistance = dist;
//				ClosestSatCurrent = sat;
//			}
//		}
//
//		WriteLine("Closest sat to: "+ClosePoint+" is ["+ClosestSatCurrent.ID[0]
//			+","+ClosestSatCurrent.ID[1]
//			+","+ClosestSatCurrent.ID[2]+
//			"] at distance "+CurrentDistance);
	}
	
	
}
