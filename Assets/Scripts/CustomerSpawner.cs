using System.Collections;

using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Way
    {
        public Transform[] Points;
    }

    [SerializeField]
    private GameObject[] _customers;

    [SerializeField]
    private Way[] _ways;

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
            var way = _ways[Random.Range(0, _ways.Length)];
            controller.Init(way.Points);

            yield return new WaitForSeconds(_spawnInterval);
        }
	}
}