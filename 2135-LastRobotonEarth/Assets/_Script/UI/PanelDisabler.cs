using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDisabler : MonoBehaviour
{
    [SerializeField] private int seconds= 2;
    private void OnEnable() {
        StartCoroutine(DisableDroneHud());
    }
    IEnumerator DisableDroneHud()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
