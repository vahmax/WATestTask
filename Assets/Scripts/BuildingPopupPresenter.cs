using UnityEngine;
using UnityEngine.UI;


public class BuildingPopupPresenter : MonoBehaviour
{
    public class CurrentBuildingInfo
	{
        public Building Script;
        public BuildingInfo Details;
        public int CurrentLevel;
	}

    [Header("Text Boxes")]
    [SerializeField] private Text _titleTextbox;
    [SerializeField] private Text _levelTextbox;
    [SerializeField] private Text _incomeTextbox;

    [SerializeField] private Text _upgradeTextbox;
    [SerializeField] private Text _upgradeCostTextbox;

    [Header("Buttons")]
    [SerializeField] private Button _upgradeButton;

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

    public void DrawBuildingInfo(CurrentBuildingInfo info)
	{
         _building = info.Script;

        DrawCurrentInfo(info);
        DrawUpgradeInfo(info);
        
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
        var cost = building.Details.Levels[building.CurrentLevel].UpgradeCost;

        if (BalanceManager.Instance.TrySpendCoins(cost) == false)
		{
            return;
		}

        _building.UpgradeLevel();
	}

    private void DrawCurrentInfo(CurrentBuildingInfo info)
	{
        _titleTextbox.text = info.Details.Title;
        _levelTextbox.text = $"{info.CurrentLevel + 1}";
        _incomeTextbox.text = info.Details.Levels[info.CurrentLevel].UpgradeCost.ToString();
	}

    private void DrawUpgradeInfo(CurrentBuildingInfo info)
	{
        if (_building.HasMaximumLevel)
		{
            _upgradeTextbox.text = "Max";
            _upgradeCostTextbox.text = string.Empty;
            _upgradeButton.enabled = false;

            return;
		}

        _upgradeTextbox.text = "Level Up";
        _upgradeCostTextbox.text = info.Details.Levels[info.CurrentLevel].UpgradeCost.ToString();
        _upgradeButton.enabled = true;
    }
}