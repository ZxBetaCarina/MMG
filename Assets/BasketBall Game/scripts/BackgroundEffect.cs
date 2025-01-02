using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f; // The maximum height the camera will move
    [SerializeField] private float speed = 1f;      // The speed of the movement

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new vertical position using a sine wave for smooth oscillation
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Update the camera's position
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}