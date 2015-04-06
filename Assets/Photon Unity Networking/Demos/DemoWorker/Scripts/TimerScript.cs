using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public Text timerText;
	public float timer;
	public static float timerr;

	// Use this for initialization
	void Start () {
		timerText.text = timer.ToString ("00.00");
	}
	
	// Update is called once per frame
	void Update () {
		if (ThirdPersonNetwork.startGame) {
			timer -= Time.deltaTime;
			TimerScript.timerr = timer;
			if (timer < 0) {
				timerText.text = "00.00";
			} else {
			
				timerText.text = timer.ToString ("00.00");
			}
		}
	}
}
