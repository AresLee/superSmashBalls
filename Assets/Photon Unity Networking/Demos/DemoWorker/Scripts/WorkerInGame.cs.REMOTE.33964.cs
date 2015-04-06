// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerInGame.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class WorkerInGame : Photon.MonoBehaviour
{
	public Color mColor;
    public Transform playerPrefab;

    public void Awake()
    {
        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.connected)
        {
            Application.LoadLevel(WorkerMenu.SceneNameMenu);
            return;
        }

        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		Debug.Log ("Room playerCount(test from harry)before:" + PhotonNetwork.room.playerCount);
		Vector3 newPos = transform.position;
		switch (PhotonNetwork.room.playerCount) 
		{
		case 1:
			newPos.x = 2;
			newPos.z = 8;
			break;
		case 2:
			newPos.x = 8;
			newPos.z = 8;
			break;
		case 3:
			newPos.x = 2;
			newPos.z = 2;
			break;
		case 4:
			newPos.x = 8;
			newPos.z = 2;
			break;

		}

		//PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0);
		GameObject newPlayer= PhotonNetwork.Instantiate(this.playerPrefab.name, newPos, Quaternion.identity, 0);



		Debug.Log ("Room playerCount(test from harry):" + PhotonNetwork.room.playerCount);
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Return to Lobby"))
        {
            PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
			Grid.colors[Color.blue]=0;
			Grid.colors[Color.red]=0;
			Grid.colors[Color.yellow]=0;
			Grid.colors[Color.green]=0;
        }
		if (GUILayout.Button ("Start Game")) {
			ThirdPersonNetwork.startGame = true;
		}
    }

    public void OnMasterClientSwitched(PhotonPlayer player)
    {
        Debug.Log("OnMasterClientSwitched: " + player);

        string message;
        InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message

        if (chatComponent != null)
        {
            // to check if this client is the new master...
            if (player.isLocal)
            {
                message = "You are Master Client now.";
            }
            else
            {
                message = player.name + " is Master Client now.";
            }


            chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
        }
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom (local)");
        
        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton");

        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPlayerDisconneced: " + player);
    }

    public void OnFailedToConnectToPhoton()
    {
        Debug.Log("OnFailedToConnectToPhoton");

        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }
}
