using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
	 
public class Grid : MonoBehaviour {
	public List<Cell> cellList = new List<Cell>();
	public float cellWidth;
	public Cell cell;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 6; i++) 
		{
			for(int j = 0; j < 6; j++)
			{  
				Cell c;
				c = Instantiate(cell) as Cell;
				c.Modify(i, j, cellWidth, cellWidth);
				cellList.Add (c);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
