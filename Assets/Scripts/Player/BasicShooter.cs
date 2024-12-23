using System;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    public LayerMask shootMask;
    private GameObject target;
    public Animator anim;

    public float coolDown;
    public float range;
    private bool canShoot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("ResetCoolDown", coolDown);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, range, shootMask);

        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot();
        }
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
    }
}
