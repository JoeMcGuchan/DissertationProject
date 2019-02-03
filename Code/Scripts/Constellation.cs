using Godot;
using System;
using System.Collections.Generic; 

public class Constellation : Spatial
{
    // This represents a collection of orbital spheres
	public OrbitalSphere[] orbitalSpheres;
	public int numOfSpheres;
	public WorldEnvironment worldEnvironment;

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
		worldEnvironment = worldEnvironmentNew;
		numOfSpheres = numOfSpheresNew;
		orbitalSpheres = orbitalSpheresNew;
	}
}
