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
    private bool isAttack; // Trạng thái tấn công

    private void Start()
    {
        anim = GetComponent<Animator>();
        canShoot = true;
    }

    private void Update()
    {
        // Kiểm tra xem có mục tiêu trong tầm hay không
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, range, shootMask);

        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot(); // Tấn công nếu có mục tiêu
        }
        else
        {
            isAttack = false; // Không có mục tiêu => không tấn công
        }

        // Cập nhật trạng thái tấn công trong Animator
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

        // Spawn bullet
        GameObject myArrow = Instantiate(bullet, shootOrigin.position, Quaternion.identity);

        // Nếu viên đạn được tạo ra, đặt trạng thái tấn công là true
        if (myArrow != null)
        {
            isAttack = true;
        }
    }
}
