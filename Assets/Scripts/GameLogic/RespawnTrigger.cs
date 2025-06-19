using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    
    private void OnTriggerEnter(Collider other) {
        Player playerToRespawn = other.transform.GetComponent<Player>();
        if (playerToRespawn != null) {
            playerToRespawn.Respawn(respawnPoint.transform.position);
        }
    }
}