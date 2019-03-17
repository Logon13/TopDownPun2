using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayersConnectedUI : MonoBehaviour
{
	TextMeshProUGUI connectedText;

	private void Start()
	{
		connectedText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		connectedText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers} Players Connected";
	}
}

