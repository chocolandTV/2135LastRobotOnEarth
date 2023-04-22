using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    int blendShapeCount = 5;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Mesh skinnedMesh;
    float currentblend = 0f;
    float blendSpeed = 3f;
    bool currentBlendStoped = false;
    private bool isBlending = true;
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
            
            // ANIM TO 0
            if (currentblend >= 0f && !currentBlendStoped && !isBlending)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(currentAnim, currentblend);
                currentblend -= blendSpeed;
                if(currentblend <= 1f && !isBlending)
                {
                    isBlending = true;
                    currentblend=0;
                    currentAnim ++;
                    if(currentAnim >= blendShapeCount)
                        currentAnim =0;
                    currentblend = 0;
                }
            }
            // ANIM TO 100 
            if (currentblend < 100f && !currentBlendStoped && isBlending)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(currentAnim, currentblend);
                currentblend += blendSpeed;
                if(currentblend >= 100f && isBlending)
                {
                    isBlending = false;
                    currentblend = 100;
                }
            }
    }
    public void PlayerStartAnimateRemote(int value)
    {
        currentBlendStoped =true;
        for (int i = 0; i < blendShapeCount; i++)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(i, 0);
            }
    
        float _currentBlend = 0;
        for (int i = 0; i < 100; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(value, currentAnim);
            _currentBlend += blendSpeed;
        }
        currentBlendStoped = false;
    }
}