using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    PLAYERATTACK,
    PLAYERHURT,
    SELECT,
    ENEMYATTACK,
    VICTORY,
    GAMEOVER,
    EXPLORE,
    MERGE,
    TILE,
    BOSS,
    BOSSDIE,
    WOODDESTROY,
    TRAP
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private Slider volumeSlider; 
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(AdjustVolume); 
    }

    private void AdjustVolume(float volume)
    {
        audioSource.volume = volume; 
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume * instance.audioSource.volume);
    }
}
