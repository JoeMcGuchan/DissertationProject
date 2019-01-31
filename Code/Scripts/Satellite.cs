using Godot;
using System;

public class Satellite : Spatial
{
	public Orbit orbit;
	public WorldEnvironment worldEnvironment;
	
	//Uniqe id [i,j,k] indicates the kth sat in the 
	//jth orbit of the ith sphere.
	public int[] id;
	
	public void Init(int i, int j, int k, Orbit newOrbit) 
	{
		id = new int[] {i, j, k};
		orbit = newOrbit;
	}
	
	public int numOfLinks;
	public Satellite[] links;
	public float[] linkDists;
}
