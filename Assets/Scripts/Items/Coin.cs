using UnityEngine;

public class Coin : MonoBehaviour
{
    public float dropToYPos;
    private float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, Random.Range(0, 20));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > dropToYPos)
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);

        
    }
}
