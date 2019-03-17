using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StatusUpdater : MonoBehaviour
{
	TextMeshProUGUI statusText;

	private void Start()
	{
		statusText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		statusText.text = $"Status: {PhotonNetwork.NetworkClientState.ToString()}";
	}
}
