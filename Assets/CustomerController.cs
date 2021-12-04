using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class CustomerController : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Transform[] _waypoints;

	private NavMeshAgent _agent;
	private int _currentWaypointIndex;

	void Awake()
	{
        _agent = GetComponent<NavMeshAgent>();
	}

	void Update()
    {
		if (IsWaypointReached)
		{
			GoToNextWaypoint();
		}
    }

	public void Init(Transform[] waypoints)
	{
		_waypoints = waypoints;
	}

	private void GoToNextWaypoint()
	{
		if (_currentWaypointIndex == _waypoints.Length)
		{
			Destroy(gameObject);
			return;
		}

		_agent.SetDestination(_waypoints[_currentWaypointIndex].position);
		_currentWaypointIndex += 1;
	}

    private bool IsWaypointReached => !_agent.pathPending && (_agent.remainingDistance <= _agent.stoppingDistance);
}
