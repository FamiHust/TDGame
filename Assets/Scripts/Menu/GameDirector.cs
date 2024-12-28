using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Timer timer;
    private float Phase2;
    public float Phase1;

    bool isPhase2 = false;

    private float upperTime = 0.0f;

    public AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
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
        else
            if (upperTime >= Phase1 && !isPhase2)
        {
            Debug.Log("Phase 2");

            audioSource.pitch = 1.2f;
            isPhase2 = true;
            Phase2 = Random.Range(45.0f, 120.0f);
            RandomTime(Phase2);
            Phase1 += 1.5f * Phase2;
        }
        else if (upperTime >= Phase1 && isPhase2)
        {
            isPhase2 = false;
            audioSource.pitch = 1.0f;

        }
    }
}
