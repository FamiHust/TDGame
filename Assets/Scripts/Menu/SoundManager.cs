using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    PLAYERATTACK,
    PLAYERHURT,
    SELECT,
    ENEMYATTACK,
    VICTORY,
    GAMEOVER
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

}