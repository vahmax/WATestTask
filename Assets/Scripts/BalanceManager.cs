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

	public void SpendCoins(int amount)
	{
		_balance -= amount;

		OnBalanceChanged?.Invoke(_balance);
	}

	public int Amount => _balance;
}
