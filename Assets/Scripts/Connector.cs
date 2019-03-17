using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Connector : MonoBehaviourPunCallbacks
{
	[Tooltip("The maximum number of players per room")]
	[SerializeField]
	byte maxPlayersPerRoom = 4;

	[Header("UI")]
	[SerializeField]
	GameObject mainMenuUI;

	[SerializeField]
	GameObject loadingUI;

	/// <summary>
	/// The clients version number
	/// Users are seperated by game version
	/// </summary>
	string gameVersion = "1";

	/// <summary>
	/// Keep track of whether we are curently in the process of connecting to a room
	/// </summary>
	bool isConnecting;

	private void Awake()
	{
		// Makes sure that scenes are synced when master client calls PhotonNetwork.LoadLevel()
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnConnectedToMaster()
	{
		//If we are trying to connect to a room
		if (isConnecting)
		{
			// Join a random room
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			// We don't want to do anything
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		// We failed to join a random room so let's create one
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
	}

	public override void OnJoinedRoom()
	{
		// Only do this if we are the first player
		if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
		{
			PhotonNetwork.LoadLevel("Game");
		}
	}

	public void Connect()
	{
		// keep track of the user trying to connect to a room
		isConnecting = true;

		//Update the UI
		mainMenuUI.SetActive(false);
		loadingUI.SetActive(true);

		// we check if we are connected or not, we join if we are, else we initiate the connection to the server
		if (PhotonNetwork.IsConnected)
		{
			// If we are connected to the master server try join a random room
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			// We aren't connnected to the master server. Let's connect to it
			PhotonNetwork.GameVersion = gameVersion;
			PhotonNetwork.ConnectUsingSettings();
		}
	}
}
