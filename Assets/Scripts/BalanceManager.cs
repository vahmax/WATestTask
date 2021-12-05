using UnityEngine;
using UnityEngine.Events;


public class BalanceManager : MonoBehaviour
{
    private int _balance;

    public UnityEvent<int> OnBalanceChanged;

	public static BalanceManager Instance = null;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this) {
			Destroy(gameObject);
		}
	}

	public void AddCoins(int amount)
	{
		_balance += amount;

		OnBalanceChanged?.Invoke(_balance);
	}

	public bool TrySpendCoins(int amount)
	{
		if (amount > _balance)
		{
			Debug.Log("You don't have enough money :(");

			return false;
		}

		_balance -= amount;

		OnBalanceChanged?.Invoke(_balance);

		return true;
	}
}
