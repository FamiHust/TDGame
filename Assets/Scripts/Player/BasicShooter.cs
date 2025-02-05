using System;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    public LayerMask shootMask;
    public Animator anim;
    private GameObject target;

    public float coolDown;
    public float range;
    private bool canShoot;
    private bool isAttack; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        canShoot = true;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, range, shootMask);

        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot(); 
        }
        else
        {
            isAttack = false; 
        }

        anim.SetBool("isAttack", isAttack);
    }

    void ResetCoolDown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if (!canShoot)
            return;

        canShoot = false;
        Invoke("ResetCoolDown", coolDown);

        GameObject myArrow = Instantiate(bullet, shootOrigin.position, Quaternion.identity);

        isAttack = true;
    }
}
