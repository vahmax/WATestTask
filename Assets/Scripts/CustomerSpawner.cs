using System.Collections;


using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _customers;

    [SerializeField]
    private Transform[] _waypoints;

    [SerializeField]
    private float _spawnInterval = 1;

    void Start()
    {
        StartCoroutine(SpawnCustomer());
    }

	private IEnumerator SpawnCustomer()
	{
        while (true)
		{
            var customer = _customers[Random.Range(0, _customers.Length)];
            Instantiate(customer, transform.position, Quaternion.identity);
            
            var controller = customer.GetComponent<CustomerController>();
            controller.Init(_waypoints);

            yield return new WaitForSeconds(_spawnInterval);
        }
	}
}