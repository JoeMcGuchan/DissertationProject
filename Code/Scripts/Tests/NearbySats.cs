using Godot;
using System;

public class NearbySats : Test
{
    // Gets vector3s of adgacent stellites relative to direction of motion
	
	public Constellation TargetConstellation; 
	
	public Satellite[] Satellites;
	public Satellite CenterSat;
	
	public Vector3[] SatPositions;
	
	public Spatial ThePlayer;
	
	public int StartTime;
	public int LastTime;

	public NearbySats(String filePath, Spatial camera)
	{
		CreateFile(filePath);
		WriteLine("time,sat0x,sat0y,sat0z,sat1x,sat1y,sat1z,sat2x,sat2y,sat2z,sat3x,sat3y,sat3z,sat4x,sat4y,sat4z,sat5x,sat5y,sat5z,sat6x,sat6y,sat6z,sat7x,sat7y,sat7z");
		
		ThePlayer = camera;
		
		StartTime = OS.GetUnixTime();
		LastTime = StartTime;
	}
	
	public void SetConstellation(Constellation constellation)
	{
		TargetConstellation = constellation; 
		
		CenterSat = TargetConstellation.OrbitalSpheres[0].Orbits[0].Satellites[0];
		
		int numOrbits = TargetConstellation.OrbitalSpheres[0].NumOfOrbits;
		int numSats = TargetConstellation.OrbitalSpheres[0].SatellitesPerOrbit;
		
		int phaseOffset = -TargetConstellation.OrbitalSpheres[0].PhaseOffset; 
		
		Satellites = new Satellite[] {
			TargetConstellation.OrbitalSpheres[0].Orbits[numOrbits-1].Satellites[mod(phaseOffset+2,numSats)],
			TargetConstellation.OrbitalSpheres[0].Orbits[0].Satellites[1],
			TargetConstellation.OrbitalSpheres[0].Orbits[1].Satellites[0],
			TargetConstellation.OrbitalSpheres[0].Orbits[numOrbits-1].Satellites[mod(phaseOffset+1,numSats)],
			TargetConstellation.OrbitalSpheres[0].Orbits[1].Satellites[numSats-1],
			TargetConstellation.OrbitalSpheres[0].Orbits[numOrbits-1].Satellites[mod(phaseOffset,numSats)],
			TargetConstellation.OrbitalSpheres[0].Orbits[0].Satellites[numSats-1],
			TargetConstellation.OrbitalSpheres[0].Orbits[1].Satellites[numSats-2]
		};
		
		SatPositions = new Vector3[8];
	}
	
	public override void Run()
	{	
		//find basis to tranform matricies into
		Vector3 y = CenterSat.Translation.Normalized();
		
		//y is in the positive longditude (simple as we are obrit 0)
		float longDitude = TargetConstellation.OrbitalSpheres[0].Orbits[0].LongditudonalOffset;
		float inclination = TargetConstellation.OrbitalSpheres[0].Inclination;
		
		Vector3 x = -new Vector3(0,1,0).Rotated(new Vector3(1,0,0),inclination).Rotated(new Vector3(0,1,0),longDitude);
		
		Vector3 z = x.Cross(y);
		
		Transform transform = new Transform(x, y, z, CenterSat.Translation).Orthonormalized();
		
		ThePlayer.Transform = transform;
		
		int time = OS.GetUnixTime();
		
		if (time != LastTime)
		{
			LastTime = time;
			
			string message = "" + (time - StartTime);
			
			for (int i = 0; i < 8; i++)
			{
				Vector3 v = transform.XformInv(Satellites[i].Translation);
				
				Satellites[i].Marked = true;
				
				message = message + "," + v.x + "," + v.y + "," + v.z;
			}
			
			WriteLine(message);
		}
	}
}
