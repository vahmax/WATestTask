using UnityEngine;
using UnityEngine.Events;


public class Building : MonoBehaviour
{
	public class CurrentBuildingInfo
	{
		public Building Script;
		public BuildingInfo Details;
		public int CurrentLevel;
	}


	[Header("Messages")]
	[SerializeField] private string HasMaximumLevelAlready = "This building has maximum level already :)";

	[Header("Events")]
	public UnityEvent<int> OnCustomerServed;
	public UnityEvent<CurrentBuildingInfo> OnBuildingUpgraded;
	public UnityEvent<CurrentBuildingInfo> OnBuildingClicked;

	[SerializeField] private BuildingInfo _info;

	private int _currentLevel;

	public bool HasMaximumLevel => _currentLevel == _info.Levels.Length - 1;

	public bool UpgradeLevel()
	{
		if (HasMaximumLevel)
		{
			Debug.LogWarning(HasMaximumLevelAlready);

			return false;
		}

		_currentLevel += 1;

		OnBuildingUpgraded?.Invoke(GetBuildingInfo());

		return true;
	}

	public void HandleCustomerPayment()
	{
		OnCustomerServed?.Invoke(_info.Levels[_currentLevel].IncomePerCustomer);
	}

	public void HandleClick()
	{
		OnBuildingClicked?.Invoke(GetBuildingInfo());
	}

	public CurrentBuildingInfo GetBuildingInfo()
	{
		return new CurrentBuildingInfo
		{
			Details = _info,
			Script = this,
			CurrentLevel = _currentLevel,
		};
	}
}
