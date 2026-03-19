using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] AudioSource musicSource;

    [Header("SFX")]
    [SerializeField] AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip collectSFX;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("AudioManager created");
    }

    public void PlayJump() => sfxSource.PlayOneShot(jumpSFX, 0.8f);
    public void PlayShoot() => sfxSource.PlayOneShot(shootSFX);
    public void PlayHit() => sfxSource.PlayOneShot(hitSFX);
    public void PlayCollect() => sfxSource.PlayOneShot(collectSFX);

    public void SetMusicVolume(float volume) => musicSource.volume = volume;
}