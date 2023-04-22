using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    private Animator animator;
    private void Start() {
        animator  = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("restart"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Drone_killing, gameObject.transform.position);
        }
    }
}
