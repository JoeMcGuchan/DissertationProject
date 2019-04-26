using Godot;
using System;

using System.Collections.Generic;

public class PathSetSortable
{
	List<VertexPath> Paths;
	
	public PathSetSortable()
	{
		Paths = new List<VertexPath>();
	}

	public void Add(VertexPath p)
	{
		Paths.Add(p);
	}

	public void Sort()
	{
		Paths.Sort(CompareDistances);
	}

	public VertexPath Pop()
	{
		VertexPath v = Paths[0];
		Paths.RemoveAt(0);
		return v;
	}
	
	private static int CompareDistances(VertexPath x, VertexPath y)
	{
		if (x == null)
		{
			if (y == null)
			{
				return 0;
			}
			else
			{
				return -1;	
			}
		}
		else
		{
			if (y == null)
			{
				return 1;
			}
			else
			{
				return x.GetDist().CompareTo(y.GetDist());
			}
		}
	}
}
