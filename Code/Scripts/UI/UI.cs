using Godot;
using System;

public class UI : Node
{
	public ConstellationDescription ThisConstellationDescription;
	public WorldEnvironment ThisWorldEnvironment;

	[Signal] 
	public delegate void ConstellationChanged();

	public void Init(ConstellationDescription ConstellationDescriptionNew, WorldEnvironment WorldEnvironemntNew)
	{
		ThisConstellationDescription = ConstellationDescriptionNew;
		ThisWorldEnvironment = WorldEnvironemntNew;
	}

	public void OnNumberOfOrbitsValueChanged(int numOfObrits)
	{
		ThisConstellationDescription.OrbitalPlanesPerSphere[0] = numOfObrits;
		EmitSignal("ConstellationChanged");
	}

	public void OnTimeFactorValueChanged(float timeFactorNew)
	{
		ThisWorldEnvironment.TimeFactor = timeFactorNew;
		EmitSignal("ConstellationChanged");
	}
}
