using Godot;
using System;

using System.Collections.Generic;

public class NoLinks : LinkingMethod
{
	public NoLinks() {}
	
	public override void Initialise(Constellation constellation) {}
	
	public override void UpdateConstellation(Constellation constellation) {}
	
	public override void UpdateOrbit(Orbit orbit) {}
	
	public override List<Link> GetAllLinks() {return new List<Link>();}
}
