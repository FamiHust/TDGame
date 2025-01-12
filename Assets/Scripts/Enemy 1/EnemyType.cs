using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New EnemyType", menuName = "Enemy")]
public class EnemyType : ScriptableObject
{
    public int health;
    public float speed;
    public int damage;
    public float range = 0.5f;
    public float eatCoolDown = 1f;
    public Sprite sprite;
    public Sprite deathSprite;

    // Thêm thuộc tính điểm số
    public int score; // Điểm số khi tiêu diệt kẻ thù
}
