using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 0.8f;
    private bool freeze = false;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

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
            Destroy(gameObject);
        }
    }

}
