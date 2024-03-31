using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateUICamera : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Pobierz wektor kierunku od obiektu UI World do kamery
            Vector3 direction = mainCamera.transform.position - transform.position;
            // Zresetuj rotacjê obiektu UI World
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}