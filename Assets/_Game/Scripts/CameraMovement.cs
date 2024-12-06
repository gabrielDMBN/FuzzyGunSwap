using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;
    public BotãoFecharAbrir botãoFecharAbrir; // Reference to the BotãoFecharAbrir script

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // Movimento da câmera
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(moveRight, 0, moveForward);

        // Rotação da câmera
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Check for "E" key press to toggle canvas position
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed"); // Check if the E key press is detected
            if (botãoFecharAbrir != null)
            {
                botãoFecharAbrir.ToggleCanvasPosition();
            }
        }
    }
}