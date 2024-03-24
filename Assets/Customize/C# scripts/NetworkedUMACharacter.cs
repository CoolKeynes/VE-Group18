using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Spawning;
using UnityEngine;

public class NetworkedUMACharacter : MonoBehaviour, INetworkSpawnable
{
    public NetworkId NetworkId { get; set; }
    public GameObject umaCharacterPrefab;
    void Start()
    {
        // spawn the UMA character to all peer 
        NetworkSpawnManager networkSpawner = NetworkSpawnManager.Find(this);
        if (networkSpawner != null)
        {
            Debug.Log("spawn");
            networkSpawner.SpawnWithPeerScope(umaCharacterPrefab);
        }
    }

}
