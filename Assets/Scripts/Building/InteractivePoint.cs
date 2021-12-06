using UnityEngine;
using UnityEngine.Events;


public class InteractivePoint : MonoBehaviour
{
    public UnityEvent OnCustomerPayment;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Customer"))
		{
			OnCustomerPayment?.Invoke();
		}
	}
}
