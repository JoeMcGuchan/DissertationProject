using Godot;
using System;

public class Link : Spatial
{
	public bool Active;
	public ImmediateGeometry Line;
	//link is the child of Sat1
	
	public Satellite Sat1;
	public Satellite Sat2;
	public float Dist;
	
	ColouringMethod ColouringMethod;
	
	//used for algoritms
	public bool Marked = false;
	
	public void Init(Satellite sat1, Satellite sat2)
	{
		Sat1 = sat1;
		Sat2 = sat2;
		
		sat1.AddLink(this);
		sat1.AddChild(this);
		
		sat2.AddLink(this);
		
		Active = false;
		Dist = sat1.Translation.DistanceTo(sat2.Translation);
		
		Line = new ImmediateGeometry();
		
		Update();
	}
	
	public void Update()
	{
		Draw();
	}
	
	//draw line to sat2
	public void Draw()
	{
		Line.Clear();
		Line.Begin(Mesh.PrimitiveType.Lines);
		Line.AddVertex(new Vector3(0,0,0));
		Line.AddVertex(ToLocal(Sat2.Translation));
		Line.End();
	}
}
