using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AdaptiveMusicController : MonoBehaviour
{
    public static AdaptiveMusicController Instance { get; private set; }

    [SerializeField] EventReference normalMusicRef;

    EventInstance currentMusic;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayNormal();
    }

    public void PlayNormal()
    {
        SwitchTo(normalMusicRef);
    }

    void SwitchTo(EventReference eventRef)
    {
        currentMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic.release();
        currentMusic = RuntimeManager.CreateInstance(eventRef);
        currentMusic.start();
    }

    void OnDestroy()
    {
        currentMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic.release();
    }
}