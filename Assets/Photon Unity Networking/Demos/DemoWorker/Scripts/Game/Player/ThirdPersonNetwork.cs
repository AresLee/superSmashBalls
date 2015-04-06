using UnityEngine;
using System.Collections;

public class ThirdPersonNetwork : Photon.MonoBehaviour
{
	#region private variables
	private float 	m_LastSynchronizationTime 	= 0f;
	private float 	m_SyncDelay 				= 0f;
	private float 	m_SyncTime					= 0f;
	private Vector3 m_SyncStartPosition 		= Vector3.zero;
	private Vector3 m_SyncEndPosition 			= Vector3.zero;


	private Rigidbody theRigidbody;
	#endregion
    ThirdPersonCamera cameraScript;
   // ThirdPersonController controllerScript;
	public static int currentViewId;
	private Color mColor;
	private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private Vector3 correctVelocity=Vector3.zero;
	Vector3 syncVelocity ;
	Quaternion syncRotation;
    void Awake()
    {
		theRigidbody = gameObject.GetComponent<Rigidbody> ();
		currentViewId = photonView.viewID;
        cameraScript = GetComponent<ThirdPersonCamera>();
   //     controllerScript = GetComponent<ThirdPersonController>();

         if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraScript.enabled = true;
     //       controllerScript.enabled = true;
        }
        else
        {           
            cameraScript.enabled = false;

         //   controllerScript.enabled = true;
         //   controllerScript.isControllable = false;
        }

        gameObject.name = gameObject.name + photonView.viewID;
		//for assigning colors to remote players
		if (((photonView.viewID - 1) / 1000) % 4 == 1) {
			mColor=Color.blue;
			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.blue;
		
		}
		if (((photonView.viewID - 1) / 1000) % 4 == 2) {
			mColor=Color.red;
			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.red;
		}
		if (((photonView.viewID - 1) / 1000) % 4 == 3) {
			mColor=Color.green;
			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.green;
		}
		if (((photonView.viewID - 1) / 1000) % 4 == 0) {
			mColor=Color.yellow;
			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.yellow;
		}
		Debug.Log ("viewID:" + photonView.viewID + " order: " + ((photonView.viewID - 1) / 1000));
//		switch (PhotonNetwork.room.playerCount) 
//		{
//		case 1:
//			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.yellow;
//			break;
//		case 2:
//			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.green;
//			break;
//		case 3:
//			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.red;
//			break;
//		case 4:
//			gameObject.GetComponent<MeshRenderer>().materials[0].color=Color.blue;
//			break;
//			
//		}

		cameraScript.enabled = false;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		/*
        if (stream.isWriting)
        {
            //We own this player: send the others our data
        //    stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation); 
        }
        else
        {
            //Network player, receive data
          //  controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }

*/
		if (stream.isWriting)									// isWriting means user sending data
		{														
			stream.SendNext(theRigidbody.position);				// user sends the rigidbody’s position
			stream.SendNext(theRigidbody.velocity);				// user sends the rigidbody’s velocity
			stream.SendNext(transform.rotation);
			stream.SendNext(transform.position);
		}
		else     												// !stream.isWriting
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			 syncVelocity = (Vector3)stream.ReceiveNext();
			 syncRotation =(Quaternion)stream.ReceiveNext();
			 correctPlayerPos = (Vector3)stream.ReceiveNext();


			m_SyncTime 				 	= 0f;
			m_SyncDelay 				= Time.time - m_LastSynchronizationTime;
			m_LastSynchronizationTime 	= Time.time;
			
			m_SyncEndPosition 	= syncPosition + syncVelocity * m_SyncDelay;
			m_SyncStartPosition = theRigidbody.position;




		}
    }

  

    void Update()
	{
		/*
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }

*/

		if(photonView.isMine)
		{
			MovePlayer();
		}
		else
		{
			m_SyncTime += Time.deltaTime;
		//	theRigidbody.position = Vector3.Lerp(m_SyncStartPosition, m_SyncEndPosition, m_SyncTime / m_SyncDelay);
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
			theRigidbody.velocity=Vector3.Lerp(theRigidbody.velocity,syncVelocity,Time.deltaTime*5);
			transform.rotation=Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime*5);
		}



    }

	void MovePlayer()
	{
		/*
		if (Input.GetKey(KeyCode.UpArrow))
			theRigidbody.MovePosition(theRigidbody.position + Vector3.forward * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.DownArrow))
			theRigidbody.MovePosition(theRigidbody.position - Vector3.forward * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.RightArrow))
			theRigidbody.MovePosition(theRigidbody.position + Vector3.right * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.LeftArrow))
			theRigidbody.MovePosition(theRigidbody.position - Vector3.right * 10 * Time.deltaTime);
			*/
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal*2, 4*-9.81f*Time.deltaTime, moveVertical*2);
		
		theRigidbody.velocity = movement * 10;
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