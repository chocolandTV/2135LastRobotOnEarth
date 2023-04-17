using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScrapBang : MonoBehaviour
{
    [SerializeField] private GameObject scrap;
    private Animator animator;
    private new Rigidbody rigidbody;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ground"))
        {
            // INSTANTIATE SCRAP RANDOM
            // PLAY ANIMATION EXPLOSION

        }
        if(other.CompareTag("Player"))
        {
            // PLAYER RESPAWN

        }
    }
}
