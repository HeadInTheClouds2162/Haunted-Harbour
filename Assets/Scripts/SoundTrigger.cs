using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false; // Optional: prevents the sound from playing repeatedly

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        // Optional: ensure Play On Awake is off via script
        audioSource.playOnAwake = false; 
    }

    // Called when the Player enters the trigger area
    void OnTriggerEnter2D(Collider other)
    {
        // Check if the entering object is the Player and if the sound hasn't played yet
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true; // Set to true after playing once
        }
    }
    
    // Optional: Use this to reset the sound if the player exits and re-enters
    void OnTriggerExit2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayed = false;
        }
    }
}
