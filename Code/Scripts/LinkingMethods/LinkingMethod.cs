using Godot;
using System;

using System.Collections.Generic;

public abstract class LinkingMethod
{
    // This abstract class describes the way that a sattelite forms
	// links with it's nieghtbors
	
	// each orbitalsphere is given a linking method individually (as
	// we might want different spheres to have differentmethods).
	
	//the c sharp mod function is crap for negative ints so I redefnined it
	protected int mod(int x, int m) {
		return (x%m + m)%m;
	}
	
	//this method is used by an orbital sphere to initialise the links on all it's sats
	public abstract void Initialise(Constellation constellation);
	
	//this method is used by a sphere to update the links on all it's sats
	public abstract void UpdateConstellation(Constellation constellation);
	
	public abstract void UpdateBaseStation(BaseStation baseStation);
	
	public abstract void UpdateOrbit(Orbit orbit);
	
	public abstract List<Link> GetAllLinks();
	
	public void SetMarkedAll(bool b)
	{
		foreach (Link l in GetAllLinks())
		{
			l.Marked = b;
		}
	}
}