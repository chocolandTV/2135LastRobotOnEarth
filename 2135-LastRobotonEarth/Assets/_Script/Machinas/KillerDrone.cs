using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    private AudioSource droneSound;
    private void Start() {
        droneSound = GetComponent<AudioSource>();
    }
    public void StartKillerDroneSound()
    {
        droneSound.Play();
    }
}

