using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaArmRespawner : MonoBehaviour
{
    [SerializeField]GameObject BigScrapObject;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Respawn"))
        {
            Debug.Log(" TRIGGER WILL SPAWNING BIG SCRAP");
            GameObject obj = Instantiate(BigScrapObject, transform.position,Quaternion.identity);
            obj.transform.parent = other.gameObject.transform;
            SoundManager.Instance.PlaySound(SoundManager.Sound.Ambience, other.gameObject.transform.position);
        }
    }
}
