using UnityEngine;
using UnityEngine.Rendering;

public class CoinMine : MonoBehaviour
{
    public GameObject coinObject;
    public float coolDown;

    //public Timer timer;

    //private float coolDownTimer;
    private void Start() 
    {
        InvokeRepeating("SpawnCoin", coolDown, coolDown );    
        // if (timer == null)
        // {
        //     timer = FindObjectOfType<Timer>();
        // }
        // coolDownTimer = timer.speedManage;
    }

    void SpawnCoin()
    {
        GameObject myCoin = Instantiate(coinObject, new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(2f, 3f), 0), Quaternion.identity);
        myCoin.GetComponent<Coin>().dropToYPos = transform.position.y - 1;
    }

    // void Update()
    // {
    //     if(coolDownTimer != timer.speedManage)
    //     {
    //         UpdateCoolDown();
    //         coolDownTimer = timer.speedManage;
    //     }
    // }

    // void UpdateCoolDown()
    // {
    //     coolDown = timer.remainingTime / timer.speedManage;
    //     CancelInvoke("SpawnCoin");
    //     InvokeRepeating("SpawnCoin", coolDown, coolDown);
    // }
}
