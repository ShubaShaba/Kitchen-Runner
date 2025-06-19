using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    [SerializeField]
    private float fireDuration;
    [SerializeField]
    private float idleDuration;
    [SerializeField]
    private GameObject fireVisual;

    private Collider colliderRef;
    private float changeTime;

    void Start() {
        colliderRef = GetComponent<Collider>();
        stopFire();
    }

    void Update () {
        if (fireDuration <= 0 || idleDuration <= 0 || startTime > Time.time) return; 

        if (Time.time - changeTime >= idleDuration && !colliderRef.enabled) {
            changeTime = Time.time;
            startFire();
        } else if (Time.time - changeTime >= fireDuration && colliderRef.enabled) {
            changeTime = Time.time;
            stopFire();
        }
    }

    private void startFire () {
        fireVisual.SetActive(true);
        colliderRef.enabled = true;
    }

    private void stopFire () {
        fireVisual.SetActive(false);
        colliderRef.enabled = false;
    }
}
