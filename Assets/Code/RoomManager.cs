using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform playerspawnPos;

    private void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to server.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("MyRoom1", null, null);

        Debug.Log("We are connected and join to a room.");

        Invoke(nameof(SpawnPlayer), 1);
    }

    private void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, playerspawnPos.position, Quaternion.identity);
        MyGameManager.instance.FindPlayer();
    }
}