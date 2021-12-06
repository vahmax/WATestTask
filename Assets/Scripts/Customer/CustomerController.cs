using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[RequireComponent(typeof(NavMeshAgent))]
public class CustomerController : MonoBehaviour
{
	public UnityEvent<CustomerController> OnCustomerLeaved;

	[SerializeField]
	[HideInInspector]
	private Transform[] _waypoints;

	private NavMeshAgent _agent;
	private int _currentWaypointIndex;

	public CustomerController(Transform[] waypoints)
	{
		_waypoints = waypoints;
	}

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
		_currentWaypointIndex = 0;

		GoToNextWaypoint();
	}

	private void GoToNextWaypoint()
	{
		if (_currentWaypointIndex == _waypoints.Length)
		{
			OnCustomerLeaved?.Invoke(this);
			return;
		}

		_agent.SetDestination(_waypoints[_currentWaypointIndex].position);
		_currentWaypointIndex += 1;
	}

    private bool IsWaypointReached => !_agent.pathPending && (_agent.remainingDistance <= _agent.stoppingDistance);
}
 