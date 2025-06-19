using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class RollingPin : MonoBehaviour
{
    [SerializeField]
    private Vector3 movementDirection = Vector3.zero;
    [SerializeField]
    private float movementSpeed; 

    private void Move() {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other) {
        Destroyable destroyableObject = other.transform.GetComponent<Destroyable>();
        if (destroyableObject != null) {
            destroyableObject.onContact();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
