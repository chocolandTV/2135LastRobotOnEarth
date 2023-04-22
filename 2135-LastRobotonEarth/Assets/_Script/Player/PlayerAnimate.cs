using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    int blendShapeCount = 5;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Mesh skinnedMesh;
    float currentblend = 0f;
    float blendSpeed = 1f;
    bool currentBlendStoped = false;
    private int currentAnim;
    public static PlayerAnimate Instance { get; set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = skinnedMeshRenderer.sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    void FixedUpdate()
    {
        FaceAnim();
        
    }
    private void FaceAnim()
    {
        
            if (currentblend < 100f && !currentBlendStoped)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(currentAnim, currentblend);
                currentblend += blendSpeed;
            }
            else
            {
                if(currentBlendStoped)
                {
                    for (int i = 0; i < blendShapeCount; i++)
                    {
                        skinnedMeshRenderer.SetBlendShapeWeight(i, 0);
                    }
                }
                currentAnim ++;
                if(currentAnim > blendShapeCount)
                    currentAnim =0;
                currentblend = 0;
            }

            
        
    }
    public void PlayerStartAnimateRemote(int value)
    {
        float _currentBlend = 0;
        for (int i = 0; i < 100; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(value, currentAnim);
            _currentBlend += blendSpeed;
        }
    }
}