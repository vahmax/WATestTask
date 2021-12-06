using UnityEngine;
using UnityEngine.Pool;

public class CustomerSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Way
    {
        public Transform[] Points;
    }


    [SerializeField] private GameObject[] _customers;
    [SerializeField] private Way[] _ways;

    [Header("Spawner Settings")]
    [SerializeField] private float _spawnInterval = 1;

    private ObjectPool<GameObject> _pool;

	private void Awake()
	{
        _pool = new ObjectPool<GameObject>(
                    CreateCustomer,
                    InitCustomer,
                    ReleaseCustomer,
                    poolObject => Destroy(poolObject)
                );
	}

	void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), 0, _spawnInterval);
    }

    private void SpawnCustomer()
	{
        _pool.Get();
	}

	private GameObject CreateCustomer()
	{
        return Instantiate(GetRandomCustomer(), transform);
    }

    private void InitCustomer(GameObject customer)
	{
        var controller = customer.GetComponent<CustomerController>();
        controller.OnCustomerLeaved.AddListener(HandleCustomerLeaving);

        customer.transform.position = transform.position;
        customer.SetActive(true);

        controller.Init(GetRandomWaypoints());
    }

    private void ReleaseCustomer(GameObject customer)
	{
        customer.SetActive(false);
    }

    private void HandleCustomerLeaving(CustomerController customerController)
	{
        customerController.OnCustomerLeaved.RemoveListener(HandleCustomerLeaving);

        _pool.Release(customerController.gameObject);
	}

    private GameObject GetRandomCustomer()
    {
        return _customers[Random.Range(0, _customers.Length)];
    }

    private Transform[] GetRandomWaypoints()
	{
        return _ways[Random.Range(0, _ways.Length)].Points;
    }
}