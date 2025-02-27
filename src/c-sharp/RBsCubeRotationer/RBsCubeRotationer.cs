using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 90f; // Controls the rotation speed

    void Update()
    {
        // Detects when the W key is pressed and rotates around the X axis
        if (Input.GetKey(KeyCode.W)) // Rotates around the X axis when W is pressed
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
}