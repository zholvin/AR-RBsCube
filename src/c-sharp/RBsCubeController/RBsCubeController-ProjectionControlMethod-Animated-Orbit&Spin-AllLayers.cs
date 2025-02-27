using System.Collections.Generic;
using UnityEngine;

public class RubikCubeController : MonoBehaviour
{
    public List<Transform> allCubes = new List<Transform>(); // Store all small cubes
    private bool isRotating = false;

    void Start()
    {
        allCubes.Clear(); // Ensure the list is empty
        foreach (Transform child in transform)
        {
            allCubes.Add(child); // Add all small cubes of the Rubik's Cube to the list
        }
    }

    // Get the small cubes on the specified face
    public List<Transform> GetFaceCubes(Vector3 axis, float targetValue)
    {
        List<Transform> faceCubes = new List<Transform>();

        foreach (Transform cube in allCubes)
        {
            Vector3 pos = cube.position;
            float projection = Vector3.Dot(pos, axis); // Project position along the axis

            // Check if the cube belongs to the specified face
            if (Mathf.Abs(projection - targetValue) < 0.1f)
            {
                faceCubes.Add(cube);
            }
        }

        return faceCubes;
    }

    // Rotate the specified face
    public void RotateFace(List<Transform> faceCubes, Vector3 rotationAxis, float rotationAngle)
    {
        if (isRotating) return;  // Prevent triggering rotation while already rotating

        StartCoroutine(RotateFaceCoroutine(faceCubes, rotationAxis, rotationAngle));
    }

    // Coroutine for the rotation operation
    private IEnumerator<WaitForSeconds> RotateFaceCoroutine(List<Transform> faceCubes, Vector3 rotationAxis, float rotationAngle)
    {
        isRotating = true;

        // Create a temporary parent object
        GameObject tempParent = new GameObject("TempParent");
        tempParent.transform.position = Vector3.zero;

        // Store the original parent of each small cube
        List<Transform> originalParents = new List<Transform>();

        // Set the small cubes as children of the temporary parent object
        foreach (var cube in faceCubes)
        {
            originalParents.Add(cube.parent);
            cube.SetParent(tempParent.transform);
        }

        // Perform rotation
        float duration = 0.5f;
        float elapsed = 0f;
        float targetAngle = 90f; // Fixed 90° rotation

        // Adjust the rotation angle to ensure precise 90° rotation
        float remainingAngle = rotationAngle;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float step = Mathf.Min(remainingAngle, targetAngle * (Time.deltaTime / duration)); // Limit max rotation angle per frame
            tempParent.transform.Rotate(rotationAxis, step, Space.World);
            remainingAngle -= step;

            if (remainingAngle <= 0)
                break;

            yield return null;
        }

        // Restore the original parent of the small cubes
        for (int i = 0; i < faceCubes.Count; i++)
        {
            faceCubes[i].SetParent(originalParents[i]);
        }

        // Destroy the temporary parent object
        Destroy(tempParent);

        isRotating = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))                                // Rotate front face clockwise
        {
            List<Transform> frontFace = GetFaceCubes(Vector3.forward, 1f);
            RotateFace(frontFace, Vector3.forward, 90f); 
        }
        if (Input.GetKeyDown(KeyCode.E))                                // Rotate front face counterclockwise
        {
            List<Transform> frontFace = GetFaceCubes(Vector3.forward, 1f);
            RotateFace(frontFace, Vector3.back, 90f); 
        }
        if (Input.GetKeyDown(KeyCode.K))                                // Rotate back face clockwise
        {   
            List<Transform> backFace = GetFaceCubes(Vector3.back, 1f);      
            RotateFace(backFace, Vector3.back, 90f);
        }
        if (Input.GetKeyDown(KeyCode.I))                                // Rotate back face counterclockwise
        {   
            List<Transform> backFace = GetFaceCubes(Vector3.back, 1f);      
            RotateFace(backFace, Vector3.forward, 90f); 
        }


        if (Input.GetKeyDown(KeyCode.S))                                // Rotate left face clockwise
        {
            List<Transform> leftFace = GetFaceCubes(Vector3.left, -1f);
            RotateFace(leftFace, Vector3.right, 90f); 
        }
        if (Input.GetKeyDown(KeyCode.W))                                // Rotate left face counterclockwise
        {
            List<Transform> leftFace = GetFaceCubes(Vector3.left, -1f);
            RotateFace(leftFace, Vector3.left, 90f);
        }
        if (Input.GetKeyDown(KeyCode.L))                                // Rotate right face clockwise
        {
            List<Transform> rightFace = GetFaceCubes(Vector3.right, -1f);
            RotateFace(rightFace, Vector3.left, 90f);
        }
        if (Input.GetKeyDown(KeyCode.O))                                // Rotate right face counterclockwise
        {
            List<Transform> rightFace = GetFaceCubes(Vector3.right, -1f);
            RotateFace(rightFace, Vector3.right, 90f);  
        }


        if (Input.GetKeyDown(KeyCode.F))                                // Rotate top face clockwise
        {
            List<Transform> topFace = GetFaceCubes(Vector3.up, 1f);
            RotateFace(topFace, Vector3.up, 90f);
        }
        if (Input.GetKeyDown(KeyCode.R))                                // Rotate top face counterclockwise
        {
            List<Transform> topFace = GetFaceCubes(Vector3.up, 1f);
            RotateFace(topFace, Vector3.down, 90f);  
        }
        if (Input.GetKeyDown(KeyCode.J))                                // Rotate bottom face clockwise
        {
            List<Transform> bottomFace = GetFaceCubes(Vector3.down, 1f);
            RotateFace(bottomFace, Vector3.down, 90f); 
        }
        if (Input.GetKeyDown(KeyCode.U))                                // Rotate bottom face counterclockwise
        {
            List<Transform> bottomFace = GetFaceCubes(Vector3.down, 1f);
            RotateFace(bottomFace, Vector3.up, 90f);  
        }
    }
}