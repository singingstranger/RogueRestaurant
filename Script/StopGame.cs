using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to exit out of a game by using the Escape key.
/// </summary>
public class StopGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }
    }
}
