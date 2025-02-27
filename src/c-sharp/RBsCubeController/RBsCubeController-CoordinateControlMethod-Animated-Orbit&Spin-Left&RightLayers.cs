using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubikCubeControllerLR : MonoBehaviour
{
    private Transform[] leftLayer;
    private Transform[] rightLayer;
    private bool isRotating = false;

    void Start()
    {
        leftLayer = FindLayerCubes(-1, "x");  // Get small cubes at x = -1 (left layer)
        rightLayer = FindLayerCubes(1, "x");  // Get small cubes at x = 1 (right layer)

        if (leftLayer.Length != 9 || rightLayer.Length != 9)
        {
            Debug.LogError("Failed to find 9 cubes in the left or right layer!");
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateLayer(leftLayer, Vector3.right, 90));
        }
        if (Input.GetKeyDown(KeyCode.A) && !isRotating)
        {
            StartCoroutine(RotateLayer(leftLayer, Vector3.right, -90));
        }
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            StartCoroutine(RotateLayer(rightLayer, Vector3.right, 90));
        }
        if (Input.GetKeyDown(KeyCode.I) && !isRotating)
        {
            StartCoroutine(RotateLayer(rightLayer, Vector3.right, -90));
        }
    }

    /// <summary>
    /// Coroutine - Smoothly rotate a specific layer
    /// </summary>
    IEnumerator RotateLayer(Transform[] layerCubes, Vector3 axis, float angle)
    {
        isRotating = true;
        Vector3 center = CalculateCenter(layerCubes); // Calculate the rotation center

        float duration = 0.2f; // Rotation duration
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float step = angle * (Time.deltaTime / duration);

            foreach (var cube in layerCubes)
            {
                cube.RotateAround(center, axis, step);
            }
            yield return null;
        }

        // Ensure final alignment
        foreach (var cube in layerCubes)
        {
            cube.position = RoundedPosition(cube.position);
            cube.rotation = Quaternion.Euler(RoundedVector(cube.eulerAngles));
        }

        isRotating = false;
    }

    /// <summary>
    /// Calculate the center point of the current layer
    /// </summary>
    Vector3 CalculateCenter(Transform[] cubes)
    {
        Vector3 sum = Vector3.zero;
        foreach (var cube in cubes)
        {
            sum += cube.position;
        }
        return sum / cubes.Length;
    }

    /// <summary>
    /// Round the coordinates to integers to avoid floating-point errors
    /// </summary>
    Vector3 RoundedPosition(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y),
            Mathf.Round(pos.z)
        );
    }

    /// <summary>
    /// Round rotation angles to multiples of 90° 
    /// </summary>
    Vector3 RoundedVector(Vector3 angles)
    {
        return new Vector3(
            Mathf.Round(angles.x / 90) * 90,
            Mathf.Round(angles.y / 90) * 90,
            Mathf.Round(angles.z / 90) * 90
        );
    }

    /// <summary>
    /// Get the small cubes of a specified X-axis layer
    /// </summary>
    Transform[] FindLayerCubes(float value, string axis)
    {
        Transform[] allCubes = GetComponentsInChildren<Transform>();
        List<Transform> layerList = new List<Transform>();

        foreach (var cube in allCubes)
        {
            if (axis == "x" && Mathf.Abs(cube.position.x - value) < 0.1f)
            {
                layerList.Add(cube);
            }
        }

        return layerList.ToArray();
    }
}