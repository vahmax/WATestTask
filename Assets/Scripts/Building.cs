using UnityEngine;
using UnityEngine.Events;


public class Building : MonoBehaviour
{
	public UnityEvent<int> OnCustomerServed;
	public UnityEvent<BuildingPopupPresenter.CurrentBuildingInfo> OnBuildingUpgraded;
	public UnityEvent<BuildingPopupPresenter.CurrentBuildingInfo> OnBuildingClicked;

	[SerializeField]
	private BuildingInfo _info;

	private int _currentLevel;

	public void UpgradeLevel()
	{
		if (IsMaximumLevel)
		{
			Debug.Log("Building level is maximum already :)");

			return;
		}

		if (HasEnoughMoney == false)
		{
			Debug.Log("You don't have enough money :(");

			return;
		}

		BalanceManager.Instance.SpendCoins(_info.Levels[_currentLevel].UpgradeCost);

		_currentLevel += 1;

		OnBuildingUpgraded?.Invoke(CreateBuildingInfo());
	}

	public void HandleCustomerPayment()
	{
		OnCustomerServed?.Invoke(_info.Levels[_currentLevel].IncomePerCustomer);
	}

	public void HandleClick()
	{
		OnBuildingClicked?.Invoke(CreateBuildingInfo());
	}

	private BuildingPopupPresenter.CurrentBuildingInfo CreateBuildingInfo()
	{
		return new BuildingPopupPresenter.CurrentBuildingInfo
		{
			Info = _info,
			Building = this,
			CurrentLevel = _currentLevel,
		};
	}

	private bool HasEnoughMoney => BalanceManager.Instance.Amount >= _info.Levels[_currentLevel].UpgradeCost;

	private bool IsMaximumLevel => _currentLevel == _info.Levels.Length - 1;
}
