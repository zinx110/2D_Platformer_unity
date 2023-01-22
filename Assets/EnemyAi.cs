using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]


public class EnemyAi : MonoBehaviour
{

    //  Target to the AI to chase
    public Transform target;

    //  How many times in seconds we update the path
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb2;

    //  the calculated path
    public Path path;

    // The AI's speed per second
    public float speed = 600f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //  The Max Distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;



    private bool searchingForPlayer = false;


    //  waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2 = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }


        //  Start a new path to the target position and return the result to the on path complete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());

    }

    public void OnPathComplete(Path p) {
    
        path = p;
        currentWaypoint = 0;


    }

    private IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield break;
        }

        //  Start a new path to the target position and return the result to the on path complete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            //   TODO: search player.
            return;
        }

        //  TODO: always loo at player.

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        //  Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        rb2.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }


    IEnumerator SearchForPlayer()
    {
        GameObject sRssult = GameObject.FindGameObjectWithTag("Player") as GameObject;
        if (sRssult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            searchingForPlayer = false;
            target = sRssult.transform;
            StartCoroutine(UpdatePath());
            yield break;
        }
    }

}
