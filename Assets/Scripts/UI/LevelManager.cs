using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    private Transform player;
    private PlayerController playerController;
    [SerializeField]
    private String sceneName;

    void Start() {
        playerController = player.GetComponent<PlayerController>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        playerController.Release();
        SceneManager.LoadScene(sceneName);
    }
}
