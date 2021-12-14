using Photon.Pun;
using UnityEngine;
using Cinemachine;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab; //*

    public float minX, maxX;
    public float minZ, maxZ;

    private void Start()
    {
        minZ = .1f;
        maxZ = 2;
        minX = 0.1f;
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        GameObject temp = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity); //*
        if (temp.GetComponent<PhotonView>().IsMine)
        {
            temp.GetComponent<PlayerController>().SetJoysticks(Instantiate(cameraPrefab, randomPosition, Quaternion.identity)); //*
         

        }
    }
}