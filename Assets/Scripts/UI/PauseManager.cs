using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject player;
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    private PlayerController playerController;

    void Start() {
        playerController = player.GetComponent<PlayerController>();
        Resume();
    }
 
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Resume(){
        pauseMenuUI.SetActive(false);
        playerController.Unfreeze();
        // player.GetComponent<PlayerController>().enabled = true;

        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        playerController.Freeze();
        // player.GetComponent<PlayerController>().enabled = false;
        
        Time.timeScale = 0;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
