using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; //For UI stamina bar

public class PlayerController : MonoBehaviour
{
    public float lookSpeedX = 2.0f; //Camera movement for left and right
    public float lookSpeedY = 2.0f; //Camera movement for up and down

    public float walkSpeed = 3.0f; //Walking speed
    public float runSpeed = 6.0f; //Running speed
    private bool isRunning = false;
    private Vector3 moveDirection;
    private float cameraVerticalRotation = 0f;

    private float currentStamina;
    public float stamina = 100f; //Max stamina
    public float staminaDrain = 10f; //How much stamina will drain per second while running
    public float staminaRegen = 5f; //Stamina regen per second
    public float staminaThreshold = 10f; //How much stamina is needed in order for player to run again
    public Slider staminaBar; //Setting up stamina bar UI

    public float health = 100f; //Max health
    private float currentHealth; //Current player health
    public Slider healthBar; //Setting up health bar UI


    public Transform playerCamera; //Reference to current player camera (Cinemachine Virtual Camera)

    private float rotationX = 0f; //Current vertical rotation of the camera

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentStamina = stamina; //Initalize player stamina
        currentHealth = health; //Initalize player health
    }

    private void Update()
    {
        HandleMovementInput();
        HandleStamina();
        UpdateStaminaUI();
        UpdateHealthUI();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleMovementInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        transform.Rotate(Vector3.up * mouseX);

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = Camera.main.transform.right;

        moveDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > staminaThreshold;

        if (isRunning)
        {
            moveDirection *= runSpeed;
        }

        else
        {
            moveDirection *= walkSpeed;
        }

        transform.Translate(moveDirection * Time.deltaTime, Space.World);
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 movement = moveDirection * Time.fixedDeltaTime;
            rigidbody.MovePosition(transform.position + movement);
        }
    }
    private void HandleStamina()
    {
        if (isRunning)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
        }

        else
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
    }

    private void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina / stamina; //Update the slider UI with percentage of stamina
        }
    }

    public void HandleHealth(int enemyDamage)
    {
        currentHealth -= enemyDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, health); //Make sure health doesn't go below zero

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Debug.Log("Player has died!");
        //No destroy gameobject yet due to not knowing if we are going to have a title screen or how player is going to die
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / health; //Update the health bar UI with the percentage
        }
    }
}