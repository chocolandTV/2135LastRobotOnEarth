using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [field: SerializeField] private AudioClip[] audioClips = new AudioClip[12];
    [SerializeField]private List<AudioClip> SongList;
    [SerializeField] private GameVariables gameVariables;
    [SerializeField] private GameObject SoundObject;
    [SerializeField] private AudioSource MusicObject;
    
    public static SoundManager Instance {get;set;}
    private int currentMusicIndex = 0;
    public enum Sound
    {
        Robot_Thruster,
        Robot_Moving,
        Robot_Happy,
        Robot_StorageFull,
        Drone_killing,
        Ambience,
        object_collect,
        object_deposit,
        upgrade_complete,
        upgrade_failed,
        TerraformerPlus,
        MenuClick,
        Alone,
        Alone1,
        Alone2,
        Alone3
    }
    

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start() {
        // MusicObject = GetComponent<AudioSource>();
        PlayMusicNext();
    }
    public void OnChangeMusicVolume(float value)
    {
        gameVariables.MusicVolume  = value;
    }
    public void OnChangeSoundVolume(float value)
    {
        gameVariables.SoundVolume = value;
    }
    public void PlaySound(Sound value,Vector3 position)
    {
        GameObject obj =Instantiate(SoundObject);
        obj.transform.position = position;
        obj.GetComponent<AudioSource>().PlayOneShot(audioClips[(int)value], gameVariables.SoundVolume);
    }
    public void PlayMusicNext()
    {
        MusicObject.PlayOneShot(SongList[currentMusicIndex], gameVariables.MusicVolume);
        
        Invoke(nameof(EventOnEnd),SongList[currentMusicIndex].length);
    }
    void EventOnEnd()
    {
        if(Application.isEditor) Debug.LogWarning("audio finished!");
        currentMusicIndex ++;
        if(currentMusicIndex >= SongList.Count)
            currentMusicIndex = 0;
        PlayMusicNext();
    }
    // PLAY SOUND
    // PLAY BACKGROUND AND FADEEFFECT
    
}
