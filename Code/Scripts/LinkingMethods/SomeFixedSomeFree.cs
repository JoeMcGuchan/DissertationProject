using Godot;
using System;

using System.Collections.Generic;

public class SomeFixedSomeFree : LinkingMethod
{
    public int[] NumOfFreeLinks;
	
	// we use HALF as each satellite has one link in and
	// one link out
	public int[] NumOfFixedLinksHalved;
	public int[][][] FixedLinkOffsets;
	
	// Links that will never require updating
	public List<Link> FixedLinks;
	
	// Links that move, by orbitalsphere and orbit number
	public List<Link>[][] MovingLinks;
	
	// Free links
	public List<Link> FreeLinks;
	
	public SomeFixedSomeFree(
		int[] numOfFreeLinks,
		int[] numOfFixedLinksHalved,
		int[][][] fixedLinkOffsets
	) {
		NumOfFreeLinks = numOfFreeLinks;
		NumOfFixedLinksHalved = numOfFixedLinksHalved;
		FixedLinkOffsets = fixedLinkOffsets;
	}
	
	//this method is used by an orbital sphere to initialise the links on all it's sats
	public override void Initialise(Constellation constellation)
	{
		FixedLinks = new List<Link>();
		MovingLinks = new List<Link>[constellation.NumOfSpheres][];
		FreeLinks = new List<Link>();
		
		for (int x = 0; x < constellation.NumOfSpheres; x++)
		{
			OrbitalSphere sphere = constellation.OrbitalSpheres[x];
			
			MovingLinks[x] = new List<Link>[sphere.NumOfOrbits];
			
			for (int i = 0; i < NumOfFixedLinksHalved[x]; i++)
			{
				int forwardOffset = FixedLinkOffsets[x][i][0];
				int sideOffset = FixedLinkOffsets[x][i][1];
				
				for (int y = 0; y < sphere.NumOfOrbits; y++)
				{
					MovingLinks[x][y] = new List<Link>();
					
					Orbit orbit = sphere.Orbits[y];
					
					for (int z = 0; z < orbit.NumOfSatellites; z++)
					{
						Satellite sat = orbit.Satellites[z];
						
						int ynew = mod(y+sideOffset,sphere.NumOfOrbits);
						int znew = mod(z+forwardOffset,orbit.NumOfSatellites);
						
						Satellite sat2 = sphere.Orbits[ynew].Satellites[znew];
						
						Link link = new Link();
						
						link.Init(sat, sat2);
						
						if (sideOffset == 0)
						{
							FixedLinks.Add(link);
						}
						else 
						{
							MovingLinks[x][y].Add(link);
						}
					}
				}
			}
		}
	}
	
	//this method is used by a sphere to update the links on all it's sats
	public override void UpdateConstellation(Constellation constellation)
	{

	}
	
	//Update each of the moving links in the orbit
	public override void UpdateOrbit(Orbit orbit)
	{
		foreach (Link link in MovingLinks[orbit.OrbitalSphere.ID][orbit.ID])
		{
			link.Update();
		}
	}
}
