using UnityEngine;
using UnityEngine.Events;


public class BalanceManager : MonoBehaviour
{
    private int _balance;

	[Header("Messages")]
	[SerializeField] private string NotEnoughMoney = "You don't have enough money :(";

	[Space]

	[Header("Events")]
	public UnityEvent<int> OnBalanceChanged;

	public static BalanceManager Instance { get; private set; }

	public int Amount => _balance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this) {
			Destroy(gameObject);
		}

		OnBalanceChanged?.Invoke(0);
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
			Debug.LogWarning(NotEnoughMoney);

			return false;
		}

		_balance -= amount;

		OnBalanceChanged?.Invoke(_balance);

		return true;
	}
}
