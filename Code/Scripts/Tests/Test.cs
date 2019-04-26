using Godot;
using System;
using System.Collections.Generic;

public abstract class Test
{
	public System.IO.StreamWriter WriteOut;
	public int RunNumber;
	
	public void CreateFile(String path)
	{
		WriteOut = new System.IO.StreamWriter(path, true);
		RunNumber = 1;
	}
	
	public void WriteLine(String str)
	{
		WriteOut.WriteLine(str);
		Console.WriteLine(str);
		
	}
	
	public abstract void Run();
	
	protected int mod(int x, int m) {
		return (x%m + m)%m;
	}	
}
