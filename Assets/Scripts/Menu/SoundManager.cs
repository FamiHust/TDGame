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
    BOSSDIE
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioClip[] soundList;

    private AudioSource audioSource;

    [SerializeField] private Slider volumeSlider; // Thêm Slider để điều chỉnh âm lượng

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Nếu Slider được gán, đặt giá trị mặc định và lắng nghe sự thay đổi
        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume; // Gán giá trị mặc định của slider
            volumeSlider.onValueChanged.AddListener(AdjustVolume); // Lắng nghe sự thay đổi
        }
    }

    // Phương thức để điều chỉnh âm lượng
    private void AdjustVolume(float volume)
    {
        audioSource.volume = volume; // Gán giá trị Slider vào volume
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume * instance.audioSource.volume);
    }
}
