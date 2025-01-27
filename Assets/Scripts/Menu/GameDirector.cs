// using DG.Tweening;
// using UnityEngine;
// using System.Collections;

// public class GameDirector : MonoBehaviour
// {
//     public Timer timer;

//     [SerializeField] private float Phase1;

//     private float Phase2;
//     private float upperTime = 0.0f;
//     private bool isPhase2 = false;

//     public AudioSource audioSource;
//     public GameObject WarningPanel;
//     private CanvasGroup warningCanvasGroup;


//     void Awake()
//     {
//         audioSource = GetComponent<AudioSource>();
//     }
//     private void Start()
//     {
//         WarningPanel.SetActive(false);
//         warningCanvasGroup = WarningPanel.GetComponent<CanvasGroup>();
//         if (warningCanvasGroup == null)
//         {
//             warningCanvasGroup = WarningPanel.AddComponent<CanvasGroup>();
//         }
//         if (timer == null)
//         {
//             timer = FindObjectOfType<Timer>();
//         }
//     }

//     void RandomTime(float time)
//     {
//         timer.speedManage = 2.0f;
//     }

//     void BossFight()
//     {
//         // timer.speedManage = 0.5f;
//     }
//     private void Update()
//     {
//         upperTime += Time.deltaTime;

//         if (upperTime >= Phase1 * 5)
//         {
//             BossFight();
//         }
//         else if (upperTime >= Phase1 && !isPhase2)
//         {
//             Debug.Log("Phase 2");
//             StartCoroutine(Warning());

//             audioSource.pitch = 1.2f;
//             isPhase2 = true;
//             Phase2 = 300;
//             RandomTime(Phase2);
//             Phase1 += 2f * Phase2;
//         }
//     }

//     private IEnumerator Warning()
//     {
//         WarningPanel.SetActive(true);
//         warningCanvasGroup.alpha = 0f;

//         // Initial fade in
//         warningCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutSine);

//         // Create flash sequence
//         Sequence flashSequence = DOTween.Sequence();
//         flashSequence.Append(WarningPanel.transform.DOScale(1.1f, 0.3f))
//                 .Append(WarningPanel.transform.DOScale(1.0f, 0.3f))
//                 .SetLoops(-1);

//         yield return new WaitForSeconds(5.0f);

//         flashSequence.Kill();

//         warningCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutSine)
//         .OnComplete(() => WarningPanel.SetActive(false));
//     }
// }

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // Import UI namespace
using System.Collections;

public class GameDirector : MonoBehaviour
{
    public Timer timer;
    public Slider volumeSlider; // Add a reference for the slider

    [SerializeField] private float Phase1;

    private float Phase2;
    private float upperTime = 0.0f;
    private bool isPhase2 = false;

    public AudioSource audioSource;
    public GameObject WarningPanel;
    private CanvasGroup warningCanvasGroup;

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
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }

        // Initialize Slider and its event
        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume; // Set slider to current volume
            volumeSlider.onValueChanged.AddListener(AdjustVolume); // Listen for changes
        }
    }

    void AdjustVolume(float volume)
    {
        audioSource.volume = volume; // Adjust audio source volume
    }

    void RandomTime(float time)
    {
        timer.speedManage = 2.0f;
    }

    void BossFight()
    {
        // timer.speedManage = 0.5f;
    }

    private void Update()
    {
        upperTime += Time.deltaTime;

        if (upperTime >= Phase1 * 5)
        {
            BossFight();
        }
        else if (upperTime >= Phase1 && !isPhase2)
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

        // Initial fade in
        warningCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutSine);

        // Create flash sequence
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
