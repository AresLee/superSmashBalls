using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayColorCounts : MonoBehaviour {
	
	public Text countText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		countText.text = "" + Grid.colors[Color.blue] + " ";
		
		countText.text += "" + Grid.colors[Color.red] + " ";
		
		countText.text += "" + Grid.colors[Color.green]+ " ";
		
		countText.text += "" + Grid.colors[Color.black]+ " ";
		
	}
}
