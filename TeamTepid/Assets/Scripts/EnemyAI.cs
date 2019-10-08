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
    [SerializeField] private EndOfPathBehaviour loopPath;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float runSpeed = 10;
    [Tooltip("The distance at which the AI will detect the player.")]
    public float DetectionRadius = 5;
    [Tooltip("The time in seconds before the AI will react after seeing the player.")]
    [SerializeField] private float aggroDelay = 0.2f;
    [Tooltip("The range at which the AI will attack the player, if under this range the AI will run towards the player.")]
    public float CombatRange = 10;

    [SerializeField] private GameObject ShotgunShot;
    private float timeSinceFired = 0.0f;
    private bool didShoot = false;

    private int waypointIndex = 0;
    private bool inCombat = false;
    private bool shouldChase = false;
    private float aggroTimeElapsed = 0;

    public bool isDead = false;
    public bool didDebugDeath = false;
    private ContactFilter2D cf;
    [SerializeField] LayerMask raycastTargets;

    private bool backTracking = false;
    private PropInteraction prop;
    enum EndOfPathBehaviour
    {
        DONT_LOOP,
        LOOP,
        BACKTRACK
    }

    private void Start()
    {
        if(GetComponentInChildren<PropInteraction>())
        {
            prop = GetComponentInChildren<PropInteraction>();
        }
        //cf.layerMask = raycastTargets;
    }

    private void Update()
    {
        if (!player)
            return;

        if (inCombat)
        {
            // Stop/Start chasing depending on the distance to the player
            shouldChase = Vector3.Distance(transform.position, player.transform.position) > CombatRange;
        }
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, DetectionRadius, raycastTargets, 0);
        if (ray.collider && ray.collider.CompareTag(player.tag))
        {
            Debug.Log(ray.collider.GetType());
            // Wait for a number of seconds equal to the aggroDelay before setting the AI to in combat
            aggroTimeElapsed += Time.deltaTime;
            if (aggroTimeElapsed >= aggroDelay)
            {
                inCombat = true;
            }
        }
        else if (inCombat)
        {
            DropCombat();
        }

#if UNITY_EDITOR
        if (isDead && !didDebugDeath)
        {
            Debug.Log("ENEMY AI IS DEAD");
            didDebugDeath = true;
        }
#endif
    }

    public void Kill()
    {
        Destroy(gameObject);

    }

    private void DropCombat()
    {
        inCombat = false;
        shouldChase = false;
        aggroTimeElapsed = 0;

        float closestDist = 10000;
        int closestIndex = 0;
        bool clearPathFound = false;
        for (int i = 0; i < waypoints.Length; i++)
        {
            List<RaycastHit2D> rays = new List<RaycastHit2D>();
            // Get all raycast targets between this and the waypoint
            float dist = Vector2.Distance(transform.position, waypoints[i]);
            Physics2D.Raycast(transform.position, waypoints[i] - transform.position,cf, rays, dist);
            bool wallsFound = false;
            foreach (RaycastHit2D ray in rays)
            {
                // If any of the rays are not a player, they are walls
                if (ray.collider.CompareTag("walls"))
                {
                    wallsFound = true;
                }
            }
            //Debug.Log(wallsFound);
            if (!wallsFound && !clearPathFound)
            {
                clearPathFound = true;
                closestDist = dist;
                closestIndex = i;
            }
            else if ((wallsFound && !clearPathFound) || !wallsFound)
            {
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = i;
                }
            }
        }

        waypointIndex = closestIndex;
    }

    private void FixedUpdate()
    {
        // Chase/Fighting Code
        if (inCombat)
        { 
            Vector3 moveVector = player.transform.position - transform.position;
            moveVector.Normalize();
            moveVector *= runSpeed / 50;
            var angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;

            if (prop != null)
            {
                prop.useDirection = moveVector;
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (shouldChase)
            {
                if(prop)
                {
                    prop.StopPropUse();
                }
                transform.position += moveVector;
            }
            else if(prop && prop.canUse)
            {
                prop.UseProp();
            }
        }
        // Following waypoints code
        else if (waypoints.Length > 0 && waypointIndex < waypoints.Length)
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
                if (backTracking)
                {
                    waypointIndex--;
                    if (waypointIndex == -1)
                    {
                        waypointIndex = 0;
                        backTracking = false;
                    }
                }
                else
                {
                    waypointIndex++;
                    if (waypointIndex == waypoints.Length)
                    {
                        switch(loopPath)
                        {
                            case EndOfPathBehaviour.LOOP:
                                {
                                    waypointIndex = 0;
                                    break;
                                }
                            case EndOfPathBehaviour.BACKTRACK:
                                {
                                    backTracking = true;
                                    waypointIndex = waypoints.Length - 1;
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
