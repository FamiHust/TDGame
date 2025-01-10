using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 0.8f;
    public bool freeze;
    public bool isFireBullet;

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

            // Chỉ phá hủy nếu không phải đạn fire
            if (!isFireBullet)
            {
                SoundManager.PlaySound(SoundType.PLAYERATTACK);
                Destroy(gameObject);
            }
            else
            {
                SoundManager.PlaySound(SoundType.PLAYERATTACK);
                Destroy(gameObject, 3f);
            }
        }

        if (other.CompareTag("Bound"))
        {
            Destroy(gameObject);
        }
    }

}
