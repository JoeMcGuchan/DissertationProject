using Godot;
using System;

public abstract class ColouringMethod
{
	public abstract void ColourSat(Satellite sat);
	
	public abstract void ColourBaseStation(BaseStation b);
	
	public abstract void ColourLink(Link l);
	
	public void ColourConstellation(Constellation c)
	{
		foreach (Link l in c.ThisLinkingMethod.GetAllLinks()) {ColourLink(l);}
		
		foreach (BaseStation b in c.BaseStations) {ColourBaseStation(b);}
		
		for(int i = 0; i < c.NumOfSpheres; i++)
		{
			OrbitalSphere s = c.OrbitalSpheres[i];
			
			for (int j = 0; j < s.NumOfOrbits; j++)
			{
				Orbit o = s.Orbits[j];
				
				for (int k = 0; k < s.SatellitesPerOrbit; k++) {ColourSat(o.Satellites[k]);}
			}
		}
	}
	
	public void ColourVertexPath(VertexPath p)
	{
		foreach(Link l in p.Links)
		{
			ColourLink(l);
		}
		
		foreach(Vertex v in p.Verticies)
		{
			if (v is BaseStation)
			{
				ColourBaseStation((BaseStation) v);
			}
			else if (v is Satellite)
			{
				ColourSat((Satellite) v);
			} 
		}
	}
}