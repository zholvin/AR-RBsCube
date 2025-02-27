using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Controls the camera movement speed
    private bool isRotating = false;   // Flag to check if the camera is rotating
    private Vector3 lastMousePosition; // Stores the last mouse position

    void Update()
    {
        // Mouse control for moving the camera position
        if (Input.GetMouseButtonDown(0)) // When the left mouse button is pressed
        {
            // Record the mouse position
            lastMousePosition = Input.mousePosition;
            isRotating = true;
        }

        if (isRotating)
        {
            // Get the current mouse position
            Vector3 currentMousePosition = Input.mousePosition;
            // Calculate the mouse movement delta
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            // Horizontal movement: Mouse X-axis movement affects the camera's left/right position
            float horizontalMove = -mouseDelta.x * moveSpeed * Time.deltaTime; // Invert horizontal movement
            // Vertical movement: Mouse Y-axis movement affects the camera's up/down position
            float verticalMove = -mouseDelta.y * moveSpeed * Time.deltaTime; // Invert vertical movement

            // Move the camera
            transform.position += transform.right * horizontalMove;  // Horizontal movement
            transform.position += transform.up * verticalMove;       // Vertical movement

            // Update the last mouse position
            lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // When the left mouse button is released
        {
            isRotating = false;
        }
    }
}