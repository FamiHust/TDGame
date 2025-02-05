using UnityEngine;

public class Wood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.PlaySound(SoundType.WOODDESTROY);
        }
    }
}
