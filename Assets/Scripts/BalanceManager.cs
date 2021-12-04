using UnityEngine;
using UnityEngine.Events;


public class BalanceManager : MonoBehaviour
{
    private int _balance;

    public UnityEvent<int> OnBalanceChanged;

	private void Start()
	{
		_balance = 0;
	}

	public void AddCoins(int amount)
	{
		_balance += amount;

		OnBalanceChanged?.Invoke(_balance);
	}
}
