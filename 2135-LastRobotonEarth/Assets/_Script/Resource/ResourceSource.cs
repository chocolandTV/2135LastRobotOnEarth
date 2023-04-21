using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceSource : MonoBehaviour
{
    public int quantity;
    private int maxQuantity;
    
    [SerializeField]private GameObject scalableObject;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private TextMeshProUGUI text;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    
    private void Start()
    {
        text.text = "+ " + quantity;
        maxQuantity = quantity;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attractor"))
        {
           
            this.StartCoroutine(Extract());
           
            // Debug.Log("Start Coroutine" + test + "OBEJCT" + other.gameObject,other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Attractor"))
        {
            CancelInvoke();
            // Debug.Log("STOP COROUTINE");
        }
    }
    IEnumerator Extract()
    {
        
        while (ResourceManager.Instance.isSpaceInStorage() && quantity > 0)
        {   
            
            scalableObject.transform.localScale *= (0.9f / VariableManager.Instance.Game_collecting_speed);
            particleSystem.Emit(emitParams, quantity);
            if (ResourceManager.Instance.isSpaceInStorage())
            // ADD RESOURCE
            {
                ResourceManager.Instance.AddResourcePlayer(1);
                quantity--;
                text.text = "+ " + quantity;

            }
            yield return new WaitForSeconds(scalableObject.transform.localScale.x / VariableManager.Instance.Game_collecting_speed);
        }
        if(quantity <=0)
            Destroy(gameObject);

    }



}
