using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private float Phase1;
    private float Phase2;
    private float upperTime = 0.0f;
    private bool isPhase2 = false;

    public AudioSource audioSource;
    public GameObject WarningPanel;
    private CanvasGroup warningCanvasGroup;
    public Slider volumeSlider; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        WarningPanel.SetActive(false);
        warningCanvasGroup = WarningPanel.GetComponent<CanvasGroup>();
        if (warningCanvasGroup == null)
        {
            warningCanvasGroup = WarningPanel.AddComponent<CanvasGroup>();
        }

        volumeSlider.value = audioSource.volume; 
        volumeSlider.onValueChanged.AddListener(AdjustVolume); 
    }

    void AdjustVolume(float volume)
    {
        audioSource.volume = volume;
    }

    void RandomTime(float time)
    {
        Timer.Instance.speedManage = 2.0f;
    }

    private void Update()
    {
        upperTime += Time.deltaTime;

        if (upperTime >= Phase1 && !isPhase2)
        {
            Debug.Log("Phase 2");
            StartCoroutine(Warning());

            audioSource.pitch = 1.2f;
            isPhase2 = true;
            Phase2 = 300;
            RandomTime(Phase2);
            Phase1 += 2f * Phase2;
        }
    }

    private IEnumerator Warning()
    {
        WarningPanel.SetActive(true);
        warningCanvasGroup.alpha = 0f;

        warningCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutSine);

        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(WarningPanel.transform.DOScale(1.1f, 0.3f))
                .Append(WarningPanel.transform.DOScale(1.0f, 0.3f))
                .SetLoops(-1);

        yield return new WaitForSeconds(5.0f);

        flashSequence.Kill();

        warningCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutSine)
        .OnComplete(() => WarningPanel.SetActive(false));
    }
}
