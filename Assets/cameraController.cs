using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3(4.4f,11.4f,4.96f);
		Camera.main.transform.localEulerAngles = new Vector3 (90, 0, 0);
	}
}
