using UnityEngine;
using System.Collections;

public class Traps : MonoBehaviour
{
    private Coroutine trapDamageCoroutine;
    private Animator anim;
    private bool isAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.SetBool("isAttacking", true);
            trapDamageCoroutine = StartCoroutine(PlayAttackSound());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.SetBool("isAttacking", false);
            StopCoroutine(trapDamageCoroutine);
        }
    }

    private IEnumerator PlayAttackSound()
    {
        while (true)
        {
            SoundManager.PlaySound(SoundType.TRAP);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
