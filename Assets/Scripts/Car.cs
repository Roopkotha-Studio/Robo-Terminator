using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = new Transform[0];

    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    private int waypoint = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (waypoint < waypoints.Length)
        {
            if (Vector3.Distance(transform.position, waypoints[waypoint].position) > 0.1f)
            {
                navMeshAgent.SetDestination(waypoints[waypoint].position);
            } else
            {
                ++waypoint;
            }
        } else
        {
            if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
            {
                navMeshAgent.SetDestination(initialPosition);
            } else
            {
                waypoint = 0;
            }
        }
    }
}
