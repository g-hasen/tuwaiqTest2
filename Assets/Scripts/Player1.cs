using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    public CharacterController cc;
    public float speed = 10;
    public float rotate = 100;
    public float jumpForce = 3;

    Vector3 velocity;
    float gravity = -30f;
    bool isGrounded;
    bool isGameOver = false;

    public GameObject gameOver;

    private AudioSource audioSource;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        if (gameOver != null)
            gameOver.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isGameOver) return;

        isGrounded = cc.isGrounded;

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, Horizontal * rotate * Time.deltaTime, 0);

        Vector3 forward = transform.forward * Vertical * speed * Time.deltaTime;

        cc.Move(forward);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        Jump();
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            velocity.y = jumpForce;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Lava")
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOver != null)
            gameOver.SetActive(true);

    }

    IEnumerator RestartAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
