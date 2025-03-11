using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private PlayerController playerController; // To access the players health

    private int pointNumber = 0; //For pathing
    public int enemyDamage = 10; //Setting up enemy damage

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(points[pointNumber].transform.position);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.SetDestination(other.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController == null) // Only checks for the player controller once
            {
                playerController = collision.gameObject.GetComponent<PlayerController>();
            }

            playerController.HandleHealth(enemyDamage); // Takes damage once per hit

        }
    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            pointNumber = (pointNumber + 1) % points.Length;
            _agent.SetDestination(points[pointNumber].transform.position);
        }
    }
}