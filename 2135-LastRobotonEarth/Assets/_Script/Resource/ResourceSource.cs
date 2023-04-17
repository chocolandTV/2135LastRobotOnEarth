using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceSource : MonoBehaviour
{
    public int quantity;
    private int maxQuantity;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private TextMeshProUGUI text;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    private bool isExtracting = false;
    private void Start()
    {
        text.text = "+ " + quantity;
        maxQuantity = quantity;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attractor")&& PlayerController.Instance.isRecycling)
        {
           
            StartCoroutine(Extract());

        }
    }
    IEnumerator Extract()
    {
        while (ResourceManager.Instance.isSpaceInStorage() && quantity > 0 && PlayerController.Instance.isRecycling)
        {   
            Debug.Log("Still in coroutine");
            gameObject.transform.localScale *= (0.9f * VariableManager.Instance.Game_collecting_speed);
            particleSystem.Emit(emitParams, quantity*20);
            if (ResourceManager.Instance.isSpaceInStorage())
            // ADD RESOURCE
            {
                ResourceManager.Instance.AddResourcePlayer(1);
                quantity--;
                text.text = "+ " + quantity;

            }
            yield return new WaitForSeconds(1.0f / VariableManager.Instance.Game_collecting_speed);
        }
        if(quantity <=0)
            Destroy(gameObject);

    }



}
