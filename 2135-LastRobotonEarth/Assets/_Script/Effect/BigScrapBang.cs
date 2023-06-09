using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScrapBang : MonoBehaviour
{
    // [SerializeField] private GameObject scrap;
    [SerializeField] private ParticleSystem emit01;
    [SerializeField] private ParticleSystem emit02;
    [SerializeField]private GameVariables gameVariables;
    
    
    
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    public static void RemoveObjectFromAnimator(GameObject gameObject, Animator animator)
        {
            Transform parentTransform = gameObject.transform.parent;
            
            gameObject.transform.parent = null;

            float playbackTime = animator.playbackTime;
            
            animator.Rebind();
            
            animator.playbackTime = playbackTime;

            gameObject.transform.parent = parentTransform;
            Debug.Log(" DO REMOVEOBJECTFROMANIMATOR");
        }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ground") || other.CompareTag("Scrap"))
        {
            // INSTANTIATE SCRAP RANDOM
            for (int i = 0; i < Random.Range(1,gameVariables.SpawningMaxObjects); i++)
            {
                GameObject obj = Instantiate(ScrapContainer.Instance.GetRandomScrapObject(), transform.position,Quaternion.identity);
                
                int scale = Random.Range(1,10);
                int scaleQuantity = Random.Range(gameVariables.SpawningQuantity.x*scale,gameVariables.SpawningQuantity.y*scale);
                
                obj.transform.localScale=(Vector3.one *scale);
                obj.GetComponent<ResourceSource>().quantity = scaleQuantity;
                Vector3 impulse = new Vector3(Random.Range(-1,1),1,(Random.Range(-1,1)));
                // OBJECT SPLIT UP AND FLY ACROSS THE IMPACT RADIUS
                obj.GetComponent<Rigidbody>().AddForce(impulse* Random.Range(10,50),ForceMode.Impulse);
            }
            emit01.Emit(emitParams, 5);
            emit02.Emit(emitParams, 30);
            Destroy(gameObject);
            // PLAY ANIMATION EXPLOSION

        }
        
    }
    
}
