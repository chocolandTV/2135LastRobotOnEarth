using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScrapBang : MonoBehaviour
{
    [SerializeField] private GameObject scrap;
    [SerializeField] private ParticleSystem emit01;
    [SerializeField] private ParticleSystem emit02;
    
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    public static void RemoveObjectFromAnimator(GameObject gameObject, Animator animator)
        {
            Transform parentTransform = gameObject.transform.parent;
            
            gameObject.transform.parent = null;

            float playbackTime = animator.playbackTime;
            
            animator.Rebind ();
            
            animator.playbackTime = playbackTime;

            gameObject.transform.parent = parentTransform;
        }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ground"))
        {
            // INSTANTIATE SCRAP RANDOM
            for (int i = 0; i < Random.Range(10,30); i++)
            {
                Instantiate(scrap, transform.position,Quaternion.identity);
                
            }
            emit01.Emit(emitParams, 5);
            emit02.Emit(emitParams, 30);
            Destroy(gameObject);
            // PLAY ANIMATION EXPLOSION

        }
        
    }
}
