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
		Console.WriteLine(ConstellationDescriptionNew.numOfSpheres);
		ThisConstellationDescription = ConstellationDescriptionNew;
		ThisWorldEnvironment = WorldEnvironemntNew;
	}

	public void OnNumberOfOrbitsValueChanged(int numOfObrits)
	{
		ThisConstellationDescription.orbitalPlaness[0] = numOfObrits;
		EmitSignal("ConstellationChanged");
	}

	public void OnTimeFactorValueChanged(float timeFactorNew)
	{
		ThisWorldEnvironment.TimeFactor = timeFactorNew;
		EmitSignal("ConstellationChanged");
	}
}
