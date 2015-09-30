using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	private Transform player; // Reference to the player's position.
	private PlayerHealth playerHealth; // Reference to the player's health.
	private EnemyHealth enemyHealth; // Reference to this enemy's health.
	private NavMeshAgent navMeshAgent; // Reference to the nav mesh agent.

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		navMeshAgent = GetComponent <NavMeshAgent> ();
	}

	void Update ()
	{
		// If the enemy and the player have health left...
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			// ... set the destination of the nav mesh agent to the player.
			navMeshAgent.SetDestination (player.position);
		} 

		// Otherwise...
		else {
			// ... disable the nav mesh agent.
			navMeshAgent.enabled = false;
		}
	}
}
