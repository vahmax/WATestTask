using System.Collections;

using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Way
    {
        public Transform[] Points;
    }


    [SerializeField] private GameObject[] _customers;
    [SerializeField] private Way[] _ways;
    [SerializeField] private float _spawnInterval = 1;
    
    void Start()
    {
        StartCoroutine(SpawnCustomer());
    }

	private IEnumerator SpawnCustomer()
	{
        while (true)
		{
            var customer = GetRandomCustomer();

            InitCustomer(customer);
            Instantiate(customer, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(_spawnInterval);
        }
	}

    private void InitCustomer(GameObject customer)
	{
        var controller = customer.GetComponent<CustomerController>();
        controller.Init(GetRandomWaypoints());
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