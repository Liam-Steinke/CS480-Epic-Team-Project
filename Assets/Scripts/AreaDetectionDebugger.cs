using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetectionDebugger : MonoBehaviour
{
    public Material originalMaterial; // Assign your default material here
    public Material detectionMaterial;  // Assign your alert material here

    private Renderer enemyRenderer;

    private void Start()
    {
        // Get the Renderer component
        enemyRenderer = GetComponent<Renderer>();

        // Apply the original material at the start
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger and switch to the detection material
        if (other.CompareTag("Player") && enemyRenderer != null && detectionMaterial != null)
        {
            enemyRenderer.material = detectionMaterial;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Revert to the original material when the player leaves
        if (other.CompareTag("Player") && enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
        }
    }
}