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

	public bool HasMaximumLevel => _currentLevel == _info.Levels.Length - 1;

	public void UpgradeLevel()
	{
		_currentLevel += 1;

		OnBuildingUpgraded?.Invoke(GetBuildingInfo());
	}

	public void HandleCustomerPayment()
	{
		OnCustomerServed?.Invoke(_info.Levels[_currentLevel].IncomePerCustomer);
	}

	public void HandleClick()
	{
		OnBuildingClicked?.Invoke(GetBuildingInfo());
	}

	public BuildingPopupPresenter.CurrentBuildingInfo GetBuildingInfo()
	{
		return new BuildingPopupPresenter.CurrentBuildingInfo
		{
			Details = _info,
			Script = this,
			CurrentLevel = _currentLevel,
		};
	}
}
