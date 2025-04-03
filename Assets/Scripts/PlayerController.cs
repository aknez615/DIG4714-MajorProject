using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //For UI health bar
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3.0f; //Walking speed
    private Vector2 move;

    [SerializeField] private Animator animator;

    public float health = 100f; //Max health
    private float currentHealth; //Current player health
    public Slider healthBar; //Setting up health bar UI

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Start()
    { 
        currentHealth = health; //Initalize player health
    }

    private void Update()
    {
        movePlayer();
        //UpdateHealthUI();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * Speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleHealth(float enemyDamage)
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