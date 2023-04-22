using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{  
    int blendShapeCount;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    float blendOne = 0f;
    float blendTwo = 0f;
    float blendSpeed = 1f;
    bool blendOneFinished = false;
    private float currentAnim;
    public static PlayerAnimate Instance {get;set;}
    void Awake ()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = skinnedMeshRenderer.GetComponent<SkinnedMeshRenderer> ().sharedMesh;
    }

    void Start ()
    {
        blendShapeCount = skinnedMesh.blendShapeCount; 
    }

    void FixedUpdate ()
    {
        if (blendShapeCount > 2) {
            if (blendOne < 100f) {
                skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);
                blendOne += blendSpeed;
            } else {
                blendOneFinished = true;
            }

            if (blendOneFinished == true && blendTwo < 100f) {
                skinnedMeshRenderer.SetBlendShapeWeight (1, blendTwo);
                blendTwo += blendSpeed;
            }
        }
    }
    public void PlayerStartAnimateRemote(int value)
    {
        currentAnim = 0;
        for (int i = 0; i < 100; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight (value, currentAnim);
                currentAnim += blendSpeed;
        }
    }
}