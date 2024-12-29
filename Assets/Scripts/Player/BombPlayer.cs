using UnityEngine;
using System.Collections;

public class Warrior : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 0.8f;
    public bool freeze;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.Hit(damage, freeze);
            SoundManager.PlaySound(SoundType.PLAYERATTACK);
            StartCoroutine(DestroyCoroutine());
        }

        if (other.CompareTag("Bound"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        anim.SetTrigger("isDie");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}