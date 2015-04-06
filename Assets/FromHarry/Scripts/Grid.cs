using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
	 
public class Grid : MonoBehaviour {
	public static List<Cell> cellList = new List<Cell>();
	public static Dictionary<Color, int> colors = new Dictionary<Color, int>();
	public List<Color> colour;
	public List<int> value;
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

		Grid.colors.Add (Grid.cellList[0].GetComponent<MeshRenderer> ().materials [0].color, 0);
		Grid.colors.Add (Color.blue, 0);
		Grid.colors.Add (Color.red, 0);
		Grid.colors.Add (Color.green, 0);
		Grid.colors.Add (Color.yellow, 0);
		Grid.colors.Add (Color.magenta, 0);
		Grid.colors.Add (Color.cyan, 0);
		Grid.colors.Add (Color.white, 0);
		Grid.colors.Add (Color.black, 0);
	}
	
	// Update is called once per frame
	void Update () {
		colour = new List<Color> ();
		value = new List<int> ();
		foreach(KeyValuePair<Color, int> entry in colors)
		{
			colour.Add(entry.Key);
			value.Add(entry.Value);
		}
	}
}
