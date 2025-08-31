using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (animator != null) animator.Play("Zombie Walk");
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
    }

    public void Die()
    {
        Destroy(gameObject); // Destroy enemy immediately
        GameObject.Find("Player").GetComponent<Player>().AddScore(10); // Give score
    }
}
