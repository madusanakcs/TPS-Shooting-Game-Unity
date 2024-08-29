using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSound : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Footstep Source")]

    [SerializeField] private AudioClip[] footStepSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomClip()
    {
              if (footStepSound != null && footStepSound.Length > 0)
        {
            return footStepSound[UnityEngine.Random.Range(0, footStepSound.Length)];
        }
        else
        {
            Debug.LogWarning("Footstep sound array is empty or not assigned.");
            return null;
        }
    }

    private void step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }
}
