using UnityEngine;
using UnityEngine.Events;


public class Building : MonoBehaviour
{
    public UnityEvent<int> OnCustomerServed;

    [SerializeField]
    private BuildingInfo _info;

    private int _currentLevel;

    public void UpgradeLevel()
	{
        if (IsMaximumLevel)
		{
            Debug.Log("Building level is maximum already.");

            return;
		}

        _currentLevel += 1;
	}

    public void HandleCustomerPayment()
	{
        OnCustomerServed?.Invoke(_info.Levels[_currentLevel].IncomePerCustomer);
	}

    private bool IsMaximumLevel => _currentLevel == _info.Levels.Length - 1;
}
