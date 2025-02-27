using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubikCubeControllerUD : MonoBehaviour
{
    private Transform[] topLayer;
    private Transform[] bottomLayer;
    private bool isRotating = false;

    void Start()
    {
        topLayer = FindLayerCubes(1);   // Get small cubes at y = 1 (top layer)
        bottomLayer = FindLayerCubes(-1); // Get small cubes at y = -1 (bottom layer)

        if (topLayer.Length != 9 || bottomLayer.Length != 9)
        {
            Debug.LogError("Failed to find 9 cubes in the top or bottom layer!");
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isRotating)
        {
            StartCoroutine(RotateLayer(topLayer, Vector3.up, 90));
        }
        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            StartCoroutine(RotateLayer(topLayer, Vector3.up, -90));
        }
        if (Input.GetKeyDown(KeyCode.J) && !isRotating)
        {
            StartCoroutine(RotateLayer(bottomLayer, Vector3.up, 90));
        }
        if (Input.GetKeyDown(KeyCode.U) && !isRotating)
        {
            StartCoroutine(RotateLayer(bottomLayer, Vector3.up, -90));
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
            float step = angle * (Time.deltaTime / duration); // Calculate the rotation step per frame

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
    /// Get the small cubes of a specified Y-axis layer
    /// </summary>
    Transform[] FindLayerCubes(float yLevel)
    {
        Transform[] allCubes = GetComponentsInChildren<Transform>();
        List<Transform> layerList = new List<Transform>();

        foreach (var cube in allCubes)
        {
            if (Mathf.Abs(cube.position.y - yLevel) < 0.1f) // Find cubes at y = 1 or y = -1
            {
                layerList.Add(cube);
            }
        }

        return layerList.ToArray();
    }
}