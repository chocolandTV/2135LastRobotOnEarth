using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    // ROCKET HOLDER
    public int GameRocketScraps { get; private set; }
    public int GameRocketFigures { get; private set; }
    public int GameRocketTungsten { get; private set; }

    // PLAYER HOLDER
    public int GamePlayerScraps { get; private set; }
    public int GamePlayerFigures { get; private set; }
    public int GamePlayerTungsten { get; private set; }
    // LIMITS 
    public int MaxScraps { get; private set; }
    public int MaxFigures { get; private set; }
    public int MaxTungsten { get; private set; }


    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
