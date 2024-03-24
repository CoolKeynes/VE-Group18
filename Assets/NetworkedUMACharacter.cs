using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Spawning;
using UnityEngine;

public class NetworkedUMACharacter : MonoBehaviour, INetworkSpawnable
{
    // Start is called before the first frame update
    public NetworkId NetworkId { get; set; }
    public GameObject umaCharacterPrefab;
    void Start()
    {
        // ���ɽ�ɫ
        //NetworkSpawnManager.Find(this).SpawnWithPeerScope(umaCharacterPrefab);
        NetworkSpawnManager networkSpawner = NetworkSpawnManager.Find(this);
        if (networkSpawner != null)
        {
            Debug.Log("spawn");
            networkSpawner.SpawnWithPeerScope(umaCharacterPrefab);
            // ����
            // networkSpawner.SpawnWithRoomScope(umaCharacterPrefab);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
