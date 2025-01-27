using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    private Animator anim;
    private bool isAttacking = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(SoundApearCoroutine());
    }

    void Update()
    {
        anim.SetTrigger("isIdle");
    }

    private IEnumerator SoundApearCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SoundManager.PlaySound(SoundType.BOSS);
    }
}
