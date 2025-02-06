using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin Instance { get; private set; }

    [SerializeField] private float speed = 2f;
    public float dropToYPos;
    private bool hasStopped = false;
    private Animator anim;

    private void Awake() 
    {
        anim = GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Destroy(gameObject, Random.Range(0, 20f));
    }

    void Update()
    {
        // Nếu đồng xu chưa chạm đến dropToYPos, thì tiếp tục rơi
        if (!hasStopped && transform.position.y > dropToYPos)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if (!hasStopped)
        {
            // Đảm bảo đồng xu dừng tại đúng vị trí
            transform.position = new Vector3(transform.position.x, dropToYPos, transform.position.z);
            hasStopped = true; // Đánh dấu là đồng xu đã dừng
        }
    }

    public void AnimCoin()
    {
        anim.SetTrigger("Collect");
    }
}
