using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayColorCounts : MonoBehaviour {
	
	public Text countText;
	public int playerNum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		string[] playerNames = new string[4];
		foreach(PhotonPlayer a in PhotonNetwork.playerList){
			if(a.ID == 1){
				playerNames[0] = a.name;
			}
			if(a.ID == 2){
				playerNames[1] = a.name;
			}
			if(a.ID == 3){
				playerNames[2] = a.name;
			}
			if(a.ID == 4){
				playerNames[3] = a.name;
			}
		}

		switch (playerNum) {
		case 1:
			countText.text = playerNames[0] + " : " + Grid.colors[Color.blue] + " ";
			countText.color = Color.blue;
			break;
		case 2:
			countText.text = playerNames[1] + " : " + Grid.colors[Color.red] + " ";
			countText.color = Color.red;
			break;
		case 3:
			countText.text = playerNames[2] + " : " + Grid.colors[Color.green] + " ";
			countText.color = Color.green;
			break;
		case 4:
			countText.text = playerNames[3] + " : " + Grid.colors[Color.yellow] + " ";
			countText.color = Color.yellow;
			break;
		}


//		countText.text = "" + Grid.colors[Color.blue] + " ";
//		
//		countText.text += "" + Grid.colors[Color.red] + " ";
//		
//		countText.text += "" + Grid.colors[Color.green]+ " ";
//		
//		countText.text += "" + Grid.colors[Color.yellow]+ " ";
	}
}
