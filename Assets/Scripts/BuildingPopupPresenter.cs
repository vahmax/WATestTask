using UnityEngine;
using UnityEngine.UI;


public class BuildingPopupPresenter : MonoBehaviour
{
    public class CurrentBuildingInfo
	{
        public Building Building;
        public BuildingInfo Info;
        public int CurrentLevel;
	}

    [SerializeField]
    private Text _titleTextbox;

    [SerializeField]
    private Text _levelTextbox;

    [SerializeField]
    private Text _incomeTextbox;

    [SerializeField]
    private Text _upgradeCostTextbox;

    private Building _building;

	private void Start()
	{
        Hide();
	}

    public void Hide()
	{
        gameObject.SetActive(false);
	}

	public void Show()
	{
        gameObject.SetActive(true);
	}

    public void DrawBuildingInfo(CurrentBuildingInfo dto)
	{
        _titleTextbox.text = dto.Info.Title;
        _levelTextbox.text = $"{dto.CurrentLevel + 1}";
        _incomeTextbox.text = dto.Info.Levels[dto.CurrentLevel].IncomePerCustomer.ToString();
        _upgradeCostTextbox.text = dto.Info.Levels[dto.CurrentLevel].UpgradeCost.ToString();

        _building = dto.Building;

        Show();
    }

    public void HandleUpgradeButton()
	{
        if (_building.HasMaximumLevel)
		{
            Debug.Log("This building reached maximum level already :)");
            return;
		}

        var building = _building.GetBuildingInfo();
        var cost = building.Info.Levels[building.CurrentLevel].UpgradeCost;

        if (BalanceManager.Instance.TrySpendCoins(cost) == false)
		{
            return;
		}

        _building.UpgradeLevel();
	}
}
