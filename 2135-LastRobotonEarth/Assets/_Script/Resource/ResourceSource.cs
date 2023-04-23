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
    // [SerializeField]private GameObject deathOverTextObject;
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
            
            if (ResourceManager.Instance.isSpaceInStorage())
            // ADD RESOURCE
            {
                scalableObject.transform.localScale *= (1.0f - (VariableManager.Instance.Game_collecting_speed/20));
                particleSystem.Emit(emitParams, quantity);
                // REST CHECK
                int extracValue = (int)VariableManager.Instance.Game_collecting_speed;
                if(quantity < (int)VariableManager.Instance.Game_collecting_speed){
                    extracValue = quantity;
                }
                quantity -= extracValue;
                ResourceManager.Instance.AddResourcePlayer(extracValue);
                text.text = "+ " + quantity;

            }
            yield return new WaitForSeconds(1);
        }
        if(quantity <=0){

            // GameObject obj = Instantiate(deathOverTextObject, gameObject.transform.position, Quaternion.identity);
            // obj.GetComponent<ResourceUILookAtCamera>().text.text =maxQuantity.ToString();
            // StartCoroutine(Moveup(obj));
            SoundManager.Instance.PlaySound(SoundManager.Sound.object_collect, PlayerController.Instance.gameObject.transform.position);
            Destroy(gameObject);
        }

    }
    // IEnumerator Moveup(GameObject obj)
    // {
    //     for (int i = 0; i < 20; i++)
    //     {
    //         obj.GetComponent<RectTransform>().position += Vector3.up;
    //         yield return new WaitForSeconds(0.5f);
    //     }
    //     Destroy(gameObject);
    // }


}
