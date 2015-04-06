using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

public class TimerScript : MonoBehaviour {

	public Text timerText;
	public Text winningText;
	public float timer;
	public static float timerr;

	bool done = false;
	string winner = "";
	int win = 0;

	// Use this for initialization
	void Start () {
		timerText.text = timer.ToString ("00.00");
		winningText.text = "";
		done = false;
		winner = "";
		win = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (ThirdPersonNetwork.startGame) {
			timer -= Time.deltaTime;
			TimerScript.timerr = timer;
			if (timer < 0) {
				timerText.text = "00.00";
				ThirdPersonNetwork.startGame = false;

				int max = -10000000;
				int i = 0;
				if(!done)
				{
					foreach(KeyValuePair<Color, int> entry in Grid.colors)
					{
						Debug.Log (i + " = " + entry.Key.ToString() + " "+ entry.Value);
						if(entry.Value>max && i>0)
						{
							
							max = entry.Value;
							winner = entry.Key.ToString();
							win = i;
							Debug.Log ("winner: " + win);
						}
						i++;
					}
					done = true;
				}

				foreach(PhotonPlayer a in PhotonNetwork.playerList){
					if(a.ID == win){
						winner = a.name;
					}
				}


				winningText.text = winner + " win";

			} else {
			
				timerText.text = timer.ToString ("00.00");
			}
		}
	}
}
