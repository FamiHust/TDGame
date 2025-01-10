using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private float speed;
    private int health;

    private float range;
    private int damage;
    public EnemyType type;
    public float eatCoolDown;
    public LayerMask playerMask;
    public Player targetPlayer;
    private bool canEat = true;
    public Animator anim;
    private Coroutine attackSoundCoroutine;

    // Reference to Timer script
    public Timer timer;

    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCoolDown = type.eatCoolDown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;
        anim = GetComponent<Animator>();

        // Find Timer script if not assigned in the Inspector
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, range, playerMask);

        if (hit.collider)
        {
            targetPlayer = hit.collider.GetComponent<Player>();
            Eat();
        }

        if (health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
    }

    void Eat()
    {
        if (!canEat || !targetPlayer)
            return;
        canEat = false;
        Invoke("ResetEatCoolDown", eatCoolDown);

        targetPlayer.Hit(damage);
    }

    void ResetEatCoolDown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if (!targetPlayer)
        {
            float adjustedSpeed = speed;
            if (timer != null)
            {
                adjustedSpeed *= timer.speedManage;
            }
            transform.position -= new Vector3(0, adjustedSpeed, 0);
        }
    }

    public void Hit(int damage, bool freeze)
    {
        health -= damage;
        if (freeze)
        {
            Freeze();
        }

        StartCoroutine(BlinkEffect());

        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("isDie");
            ScoreManager.Instance.AddScore(type.score); // Update score
            StartCoroutine(DestroyCoroutine());
        }
    }

    void Freeze()
    {
        CancelInvoke("UnFreeze");
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = type.speed / 2f;
        Invoke("UnFreeze", 3);
    }

    void UnFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayer = other.GetComponent<Player>();
            attackSoundCoroutine = StartCoroutine(PlayAttackSound());
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnAttack();
            CancelInvoke("ResetEatCoolDown");
            canEat = true;

            if (attackSoundCoroutine != null)
            {
                StopCoroutine(attackSoundCoroutine);
                attackSoundCoroutine = null;
            }
        }
    }

    private IEnumerator PlayAttackSound()
    {
        while (true) // Infinite loop until stopped
        {
            SoundManager.PlaySound(SoundType.ENEMYATTACK);
            yield return new WaitForSeconds(0.7f); // Adjust delay as needed
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }

    private IEnumerator BlinkEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color hitColor = Color.red; // Color to indicate hit
        float blinkDuration = 0.07f; // Duration for each blink
        int blinkCount = 1; // Number of blinks

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = hitColor; // Change to hit color
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor; // Change back to original color
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    private void UnAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
