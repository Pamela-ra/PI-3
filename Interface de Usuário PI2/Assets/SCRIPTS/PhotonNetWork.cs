using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class PhotonNetWork : MonoBehaviourPunCallbacks
{
    private bool hasTriedConnecting = false;

    void Start()
    {
        if (!PhotonNetwork.IsConnected && !hasTriedConnecting)
        {
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.LogLevel = PunLogLevel.Full;
            PhotonNetwork.ConnectUsingSettings();
            hasTriedConnecting = true;
        }
        else
        {
            Debug.LogWarning("Connection attempt skipped. PhotonNetwork is already connecting or connected.");
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 10000;
        Debug.Log("Connected to Master Server!");

        // Adicione uma pequena espera antes de entrar no lobby
        StartCoroutine(DelayedJoinLobby());
    }

    IEnumerator DelayedJoinLobby()
    {
        yield return new WaitForSeconds(10.5f); 

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!");
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20 };
        PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room! Creating characters...");
        SpawnPlayerCharacters();
    }

    void SpawnPlayerCharacters()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-5f, 7f), transform.position.y);

        PhotonNetwork.Instantiate("masculinoCharacterPrefab_2", spawnPosition, Quaternion.identity);
        PhotonNetwork.Instantiate("femininoCharacterPrefab_2", spawnPosition, Quaternion.identity);
    }

    // Lidar com eventos de desconexão, se necessário
    public override void OnDisconnected(DisconnectCause cause)
    {
            Debug.LogWarning($"Disconnected from Photon. Cause: {cause}");
            Debug.LogWarning("Disconnected from Photon. Cause: " + cause);
    }
}
