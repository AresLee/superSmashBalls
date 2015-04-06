using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : Photon.MonoBehaviour {




	private Color mColor;

	void Start () {

	
		if (((ThirdPersonNetwork.currentViewId - 1) / 1000) % 1 == 0) {
			mColor=Color.blue;
			
		}
		if (((ThirdPersonNetwork.currentViewId - 1) / 1000) % 2 == 0) {
			
			mColor=Color.red;
		}
		if (((ThirdPersonNetwork.currentViewId - 1) / 1000) % 3 == 0) {
			
			mColor=Color.green;
		}
		if (((ThirdPersonNetwork.currentViewId - 1) / 1000) % 4 == 0) {
			
			mColor=Color.black;
		}
		Debug.Log ("cell ID:" +ThirdPersonNetwork.currentViewId);
	}
	
	// Update is called once per frame
	void Update () {
		
		}

	// Physic calculations
	void FixedUpdate () {


	}

	void OnTriggerEnter(Collider other) {
//		gameObject.tag = "Player";
//		gameObject.SetActive(false);

		if (other.gameObject.tag == "Cell") {
		//	other.gameObject.SetActive(false);
			//count++;
			//other.gameObject
			//cellRenderer.materials [0].color = Color.red;

			other.gameObject.GetComponent<MeshRenderer>().materials[0].color=mColor;
//			cellRenderer.materials[0].color=Color.red;

		}
		//Destroy(other.gameObject);
	}


}
