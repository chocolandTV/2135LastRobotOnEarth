using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    [SerializeField]private AudioSource droneSound;
    public void StartKillerDroneSound()
    {
        droneSound.Play();
    }
}

