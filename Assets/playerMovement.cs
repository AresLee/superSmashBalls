using UnityEngine;
using System.Collections;

public class playerMovement : Photon.MonoBehaviour {
	#region private variables
	private float 	m_LastSynchronizationTime 	= 0f;
	private float 	m_SyncDelay 				= 0f;
	private float 	m_SyncTime					= 0f;
	private Vector3 m_SyncStartPosition 		= Vector3.zero;
	private Vector3 m_SyncEndPosition 			= Vector3.zero;
	private Rigidbody theRigidbody;
	#endregion

	void Start () {
		theRigidbody = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame



		void FixedUpdate ()
		{
		if(photonView.isMine)
		{
			MovePlayer();
		}
		else
		{
			m_SyncTime += Time.deltaTime;
			theRigidbody.position = Vector3.Lerp(m_SyncStartPosition, m_SyncEndPosition, m_SyncTime / m_SyncDelay);
		}
			
	
	
	}

	void MovePlayer()
	{
		if (Input.GetKey(KeyCode.UpArrow))
			theRigidbody.MovePosition(theRigidbody.position + Vector3.forward * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.DownArrow))
			theRigidbody.MovePosition(theRigidbody.position - Vector3.forward * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.RightArrow))
			theRigidbody.MovePosition(theRigidbody.position + Vector3.right * 10 * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.LeftArrow))
			theRigidbody.MovePosition(theRigidbody.position - Vector3.right * 10 * Time.deltaTime);
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)									// isWriting means user sending data
		{														
			stream.SendNext(theRigidbody.position);				// user sends the rigidbody’s position
			stream.SendNext(theRigidbody.velocity);				// user sends the rigidbody’s velocity
		}
		else     												// !stream.isWriting
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			Vector3 syncVelocity = (Vector3)stream.ReceiveNext();
			
			m_SyncTime 				 	= 0f;
			m_SyncDelay 				= Time.time - m_LastSynchronizationTime;
			m_LastSynchronizationTime 	= Time.time;
			
			m_SyncEndPosition 	= syncPosition + syncVelocity * m_SyncDelay;
			m_SyncStartPosition = theRigidbody.position;
		}
	}

}
