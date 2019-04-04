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
	
	public Constellation ThisConstellation;
	
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
		var linkScene = ResourceLoader.Load("res://Scenes//Link.tscn") as PackedScene;
		
		ThisConstellation = constellation;
		
		FixedLinks = new List<Link>();
		MovingLinks = new List<Link>[constellation.NumOfSpheres][];
		FreeLinks = new List<Link>();
		
		for (int x = 0; x < constellation.NumOfSpheres; x++)
		{
			OrbitalSphere sphere = constellation.OrbitalSpheres[x];
			
			MovingLinks[x] = new List<Link>[sphere.NumOfOrbits];
			
			for (int y = 0; y < sphere.NumOfOrbits; y++)
			{
				MovingLinks[x][y] = new List<Link>();
				Orbit orbit = sphere.Orbits[y];
				
				for (int i = 0; i < NumOfFixedLinksHalved[x]; i++)
				{
					int forwardOffset = FixedLinkOffsets[x][i][0];
					int sideOffset = FixedLinkOffsets[x][i][1];
					
					for (int z = 0; z < sphere.SatellitesPerOrbit; z++)
					{
						
						Satellite sat = orbit.Satellites[z];
						
						int ynew = y+sideOffset;
						int znew = z+forwardOffset;
						
						//apply translation to account for phase offset
						if (ynew < 0) 
						{
							znew = mod(znew-sphere.PhaseOffset,sphere.SatellitesPerOrbit);
						}
						else if (ynew >= sphere.NumOfOrbits) 
						{
							znew = mod(znew+sphere.PhaseOffset,sphere.SatellitesPerOrbit);
						} else {
							znew = mod(znew,sphere.SatellitesPerOrbit);
						}
						
						ynew = mod(ynew,sphere.NumOfOrbits);
						
						Satellite sat2 = sphere.Orbits[ynew].Satellites[znew];
						
						Link link = linkScene.Instance() as Link;
						
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
	
	public override List<Link> GetAllLinks()
	{
		List<Link> allLinks = new List<Link>();
		
		allLinks.AddRange(FixedLinks);
		
		allLinks.AddRange(FreeLinks);
		
		for (int i = 0; i < ThisConstellation.NumOfSpheres; i++)
		{
			for (int j = 0; j < ThisConstellation.OrbitalSpheres[i].NumOfOrbits; j++)
			{
				allLinks.AddRange(MovingLinks[i][j]);
			}
		}
		
		return allLinks;
	}
}
