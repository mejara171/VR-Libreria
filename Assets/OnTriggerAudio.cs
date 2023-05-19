using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding GameObject is the player
        // Play the audio source
        audioSource.Play();

        // Start coroutine to delete the GameObject after audio finishes playing
        StartCoroutine(DestroyAfterAudio());
    }

    private System.Collections.IEnumerator DestroyAfterAudio()
    {
        // Wait until the audio has finished playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Destroy the GameObject containing the trigger
        Destroy(gameObject);
    }
}
