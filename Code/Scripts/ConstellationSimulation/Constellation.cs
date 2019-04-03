using Godot;
using System;
using System.Collections.Generic; 

public class Constellation : Spatial
{
    // This represents a collection of orbital spheres
	public OrbitalSphere[] OrbitalSpheres;
	public int NumOfSpheres;
	public WorldEnvironment ThisWorldEnvironment;
	
	public ColouringMethod colouringMethod;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
    }
	
	//Gives all the data to the Constellation, but doesn't
	//do the work of creating it just yet
	public void Init(
		OrbitalSphere[] orbitalSpheresNew, 
		int numOfSpheresNew,
		WorldEnvironment worldEnvironmentNew
	) {
		ThisWorldEnvironment = worldEnvironmentNew;
		NumOfSpheres = numOfSpheresNew;
		OrbitalSpheres = orbitalSpheresNew;
		
		colouringMethod = new HighlightMarked();
	}
}