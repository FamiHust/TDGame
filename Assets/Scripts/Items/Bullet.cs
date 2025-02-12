using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 0.8f;
    public bool freeze;
    public bool isFireBullet;
    private bool isStopped = false;
    private Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.Hit(damage, freeze);

            if (!isFireBullet)
            {
                isStopped = true;
                SoundManager.PlaySound(SoundType.PLAYERATTACK);
                StartCoroutine(DestroyBullet());
            }
            else
            {
                isStopped = false;
                SoundManager.PlaySound(SoundType.PLAYERATTACK);
                Destroy(gameObject, 3f);
            }
        }

        if (other.CompareTag("Bound"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        anim.SetTrigger("isDestroy");
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
}
