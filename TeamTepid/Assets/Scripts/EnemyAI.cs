using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("The players game object.")]
    [SerializeField] private GameObject player;
    [Tooltip("The AI will path between these points in order.")]
    public Vector3[] waypoints;
    [Tooltip("If true the AI will start at waypoint index 0 after it visists all locations, otherwise the AI will just stop.")]
    [SerializeField] bool loopPath = false; 
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float runSpeed = 10;
    [Tooltip("The distance at which the AI will detect the player.")]
    public float DetectionRadius = 5;
    [Tooltip("The time in seconds before the AI will react after seeing the player.")]
    [SerializeField] private float aggroDelay = 0.2f;
    [Tooltip("The range at which the AI will attack the player, if under this range the AI will run towards the player.")]
    public float CombatRange = 10;

    private int waypointIndex = 0;
    private bool inCombat = false;
    private bool shouldChase = false;
    private float aggroTimeElapsed = 0;

    private void Update()
    {
        if(inCombat)
        {
            // Stop/Start chasing depending on the distance to the player
            shouldChase = Vector3.Distance(transform.position, player.transform.position) > CombatRange;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= DetectionRadius)
        {
            // Wait for a number of seconds equal to the aggroDelay before setting the AI to in combat
            aggroTimeElapsed += Time.deltaTime;
            if (aggroTimeElapsed >= aggroDelay)
            {
                inCombat = true;
                shouldChase = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Chase/Fighting Code
        if(inCombat)
        {
            if (shouldChase)
            {
                Vector3 moveVector = player.transform.position - transform.position;
                moveVector.Normalize();
                moveVector *= runSpeed / 50;
                transform.position += moveVector;
                var angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                Debug.Log("PEW");
            }
        }
        // Following waypoints code
        else if(waypoints.Length > 0 && waypointIndex < waypoints.Length)
        {
            // Move the player towards the next waypoint
            Vector3 moveVector = waypoints[waypointIndex] - transform.position;
            moveVector.Normalize();
            moveVector *= walkSpeed / 50;
            transform.position += moveVector;

            var angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // If close to the waypoint, move onto the next one
            if (Vector3.Distance(waypoints[waypointIndex], transform.position) < 0.5f)
            {
                waypointIndex++;
                if(waypointIndex == waypoints.Length && loopPath)
                {
                    waypointIndex = 0;
                }
            }  
        }
    }
}
