using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Store all cube blocks in a list
    public List<Transform> cubeBlocks;
    private List<Transform> topLayerBlocks;  // All blocks in the top layer
    private List<Transform> bottomLayerBlocks;  // All blocks in the bottom layer
    private List<Transform> frontLayerBlocks;  // All blocks in the front layer
    private List<Transform> backLayerBlocks;  // All blocks in the back layer
    private List<Transform> leftLayerBlocks;  // All blocks in the left layer
    private List<Transform> rightLayerBlocks;  // All blocks in the right layer

    private float tolerance = 0.1f;  // Tolerance value

    void Start()
    {
        // Ensure all blocks are assigned to the list
        cubeBlocks = new List<Transform>(GetComponentsInChildren<Transform>());
        cubeBlocks.Remove(transform); // Remove the parent cube object

        // Initialize and update layers
        UpdateLayers();
    }

    void Update()
    {
        // Listen for keyboard inputs to trigger rotations
        if (Input.GetKeyDown(KeyCode.Q))  // Q key rotates the top layer clockwise
        {
            RotateTopLayer();
        }
        if (Input.GetKeyDown(KeyCode.A))  // A key rotates the top layer counterclockwise
        {
            RotateTopLayer(false);
        }
        if (Input.GetKeyDown(KeyCode.W))  // W key rotates the front layer clockwise
        {
            RotateFrontLayer();
        }
        if (Input.GetKeyDown(KeyCode.S))  // S key rotates the front layer counterclockwise
        {
            RotateFrontLayer(false);
        }
        if (Input.GetKeyDown(KeyCode.E))  // E key rotates the left layer clockwise
        {
            RotateLeftLayer();
        }
        if (Input.GetKeyDown(KeyCode.D))  // D key rotates the left layer counterclockwise
        {
            RotateLeftLayer(false);
        }
        if (Input.GetKeyDown(KeyCode.R))  // R key rotates the bottom layer clockwise
        {
            RotateBottomLayer();
        }
        if (Input.GetKeyDown(KeyCode.F))  // F key rotates the bottom layer counterclockwise
        {
            RotateBottomLayer(false);
        }
        if (Input.GetKeyDown(KeyCode.T))  // T key rotates the back layer clockwise
        {
            RotateBackLayer();
        }
        if (Input.GetKeyDown(KeyCode.G))  // G key rotates the back layer counterclockwise
        {
            RotateBackLayer(false);
        }
        if (Input.GetKeyDown(KeyCode.Z))  // Z key rotates the right layer clockwise
        {
            RotateRightLayer();
        }
        if (Input.GetKeyDown(KeyCode.X))  // X key rotates the right layer counterclockwise
        {
            RotateRightLayer(false);
        }
    }

    // Update all layer blocks
    void UpdateLayers()
    {
        topLayerBlocks = new List<Transform>();
        bottomLayerBlocks = new List<Transform>();
        frontLayerBlocks = new List<Transform>();
        backLayerBlocks = new List<Transform>();
        leftLayerBlocks = new List<Transform>();
        rightLayerBlocks = new List<Transform>();

        // Iterate through all blocks and check which layer they belong to
        foreach (Transform block in cubeBlocks)
        {
            // Check if the block belongs to the top layer (y == 1 with tolerance)
            if (Mathf.Abs(block.position.y - 1f) < tolerance)
            {
                topLayerBlocks.Add(block);
            }

            // Check if the block belongs to the bottom layer (y == -1 with tolerance)
            if (Mathf.Abs(block.position.y + 1f) < tolerance)
            {
                bottomLayerBlocks.Add(block);
            }

            // Check if the block belongs to the front layer (z == -1 with tolerance)
            if (Mathf.Abs(block.position.z + 1f) < tolerance)
            {
                frontLayerBlocks.Add(block);
            }

            // Check if the block belongs to the back layer (z == 1 with tolerance)
            if (Mathf.Abs(block.position.z - 1f) < tolerance)
            {
                backLayerBlocks.Add(block);
            }

            // Check if the block belongs to the left layer (x == -1 with tolerance)
            if (Mathf.Abs(block.position.x + 1f) < tolerance)
            {
                leftLayerBlocks.Add(block);
            }

            // Check if the block belongs to the right layer (x == 1 with tolerance)
            if (Mathf.Abs(block.position.x - 1f) < tolerance)
            {
                rightLayerBlocks.Add(block);
            }
        }
    }

    // Rotate the top layer (clockwise or counterclockwise)
    void RotateTopLayer(bool clockwise = true)
    {
        Debug.Log("Rotating top layer.");

        Vector3 center = Vector3.zero; // Center point of the cube
        float rotationAngle = clockwise ? 90f : -90f; // Rotate 90 degrees clockwise or counterclockwise

        // Rotate top layer blocks
        foreach (Transform block in topLayerBlocks)
        {
            block.RotateAround(center, Vector3.up, rotationAngle); // Rotate around y-axis
        }

        // Update layer information
        UpdateLayers();
    }

    // Rotate the front layer (clockwise or counterclockwise)
    void RotateFrontLayer(bool clockwise = true)
    {
        Debug.Log("Rotating front layer.");

        Vector3 center = Vector3.zero; // Center point of the cube
        float rotationAngle = clockwise ? 90f : -90f;

        foreach (Transform block in frontLayerBlocks)
        {
            block.RotateAround(center, Vector3.forward, rotationAngle); // Rotate around z-axis
        }

        UpdateLayers();
    }

    // Rotate the left layer (clockwise or counterclockwise)
    void RotateLeftLayer(bool clockwise = true)
    {
        Debug.Log("Rotating left layer.");

        Vector3 center = Vector3.zero;
        float rotationAngle = clockwise ? 90f : -90f;

        foreach (Transform block in leftLayerBlocks)
        {
            block.RotateAround(center, Vector3.left, rotationAngle); // Rotate around x-axis
        }

        UpdateLayers();
    }

    // Rotate the bottom layer (clockwise or counterclockwise)
    void RotateBottomLayer(bool clockwise = true)
    {
        Debug.Log("Rotating bottom layer.");

        Vector3 center = Vector3.zero;
        float rotationAngle = clockwise ? 90f : -90f;

        foreach (Transform block in bottomLayerBlocks)
        {
            block.RotateAround(center, Vector3.down, rotationAngle); // Rotate around y-axis
        }

        UpdateLayers();
    }

    // Rotate the back layer (clockwise or counterclockwise)
    void RotateBackLayer(bool clockwise = true)
    {
        Debug.Log("Rotating back layer.");

        Vector3 center = Vector3.zero;
        float rotationAngle = clockwise ? 90f : -90f;

        foreach (Transform block in backLayerBlocks)
        {
            block.RotateAround(center, Vector3.back, rotationAngle); // Rotate around z-axis
        }

        UpdateLayers();
    }

    // Rotate the right layer (clockwise or counterclockwise)
    void RotateRightLayer(bool clockwise = true)
    {
        Debug.Log("Rotating right layer.");

        Vector3 center = Vector3.zero;
        float rotationAngle = clockwise ? 90f : -90f;

        foreach (Transform block in rightLayerBlocks)
        {
            block.RotateAround(center, Vector3.right, rotationAngle); // Rotate around x-axis
        }

        UpdateLayers();
    }
}
