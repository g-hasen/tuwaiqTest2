using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }

        Destroy(gameObject);
    }
}