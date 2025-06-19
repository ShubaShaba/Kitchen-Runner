using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, Destroyable
{
    private PlayerController controller;
    void Start () {
        controller = GetComponent<PlayerController>();
    }
    
    public void onContact()
    {
        controller.Release();
        SceneManager.LoadScene("GameOver");
    }

    public void Respawn(Vector3 respawnPos)
    {
        controller.Reposition(respawnPos);
    }
}
