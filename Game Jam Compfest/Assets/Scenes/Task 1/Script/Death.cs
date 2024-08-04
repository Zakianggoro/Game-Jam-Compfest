using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Set this to the player's starting position in the Inspector
    public Transform startPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Move the player to the start position
            other.transform.position = startPosition.position;
        }
    }
}
