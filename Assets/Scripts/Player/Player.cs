using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int health;
    public int price;
    public int sell;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.layer = 9;
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetTrigger("isDie");
            SoundManager.PlaySound(SoundType.PLAYERHURT);
            StartCoroutine(DestroyCoroutine());
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
