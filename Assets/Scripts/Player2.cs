using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public Camera playerCam;
    public float walkSpeed = 6;
    public float runSpeed = 12;
    public float jumpForce = 5;
    public float gravity = 10;
    public float lookSpeed = 2;
    public float lookXLimit = 45;
    public float playerHeight = 2;
    public float crouchHeight = 1;
    public float crouchSpeed = 3;

    public int maxHealth = 100;
    public int currentHealth;

    public GameObject gameOver;
    public Slider healthBar;

    CharacterController cc;
    Vector3 moveDirection;
    float rotationX;
    bool isGameOver = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;

        UpdateHealthUI();

        if (gameOver != null)
            gameOver.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = running ? runSpeed : walkSpeed;

        Vector3 move = transform.forward * moveZ + transform.right * moveX;
        moveDirection.x = move.x * speed;
        moveDirection.z = move.z * speed;

        if (cc.isGrounded)
        {
            moveDirection.y = -1f;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R))
            cc.height = crouchHeight;
        else
            cc.height = playerHeight;

        cc.Move(moveDirection * Time.deltaTime);

        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        float rotationY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, rotationY, 0);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    void Die()
    {
        isGameOver = true;
        cc.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (gameOver != null)
            gameOver.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
