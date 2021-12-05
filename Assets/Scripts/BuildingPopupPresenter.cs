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

    [Header("Upgrade Button")]
    [SerializeField] private string MaxLevelText = "Max";
    [SerializeField] private string LevelUpText = "Level Up";
    [SerializeField] private Color CanUpgradeColor;
    [SerializeField] private Color CanNotUpgradeColor;
    [SerializeField] private Color MaxLevelUpgradeColor;
    [SerializeField] private Button _upgradeButton;


    private Building _building;
    private Image _upgradeButtonImage;

	private void Awake()
	{
        _upgradeButtonImage = _upgradeButton.GetComponent<Image>();
	}

	private void Start()
	{
        Hide();
	}

    public void Hide()
	{
        gameObject.SetActive(false);

        BalanceManager.Instance.OnBalanceChanged.RemoveListener(DrawUpgradeAvailability);
    }

	public void Show()
	{
        gameObject.SetActive(true);

        BalanceManager.Instance.OnBalanceChanged.AddListener(DrawUpgradeAvailability);
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
        var building = _building.GetBuildingInfo();
        var cost = building.Details.Levels[building.CurrentLevel].UpgradeCost;

        if (BalanceManager.Instance.TrySpendCoins(cost) == false)
		{
            return;
		}

        if (_building.UpgradeLevel() == false)
		{
            BalanceManager.Instance.AddCoins(cost);
		}
	}

    public void DrawUpgradeAvailability(int balance)
	{
        var building = _building.GetBuildingInfo();
        var cost = building.Details.Levels[building.CurrentLevel].UpgradeCost;

        if (_building.HasMaximumLevel)
		{
            _upgradeButtonImage.color = MaxLevelUpgradeColor;
        }
        else if (balance >= cost)
		{
            _upgradeButtonImage.color = CanUpgradeColor;
		}
        else
		{
            _upgradeButtonImage.color = CanNotUpgradeColor;
        }
	}

    private void DrawCurrentInfo(CurrentBuildingInfo info)
	{
        _titleTextbox.text = info.Details.Title;
        _levelTextbox.text = $"{info.CurrentLevel + 1}";
        _incomeTextbox.text = info.Details.Levels[info.CurrentLevel].IncomePerCustomer.ToString();
	}

    private void DrawUpgradeInfo(CurrentBuildingInfo info)
	{
        if (_building.HasMaximumLevel)
		{
            _upgradeTextbox.text = MaxLevelText;
            _upgradeCostTextbox.text = string.Empty;
            _upgradeButtonImage.color = MaxLevelUpgradeColor;

            _upgradeButton.enabled = false;

            BalanceManager.Instance.OnBalanceChanged.RemoveListener(DrawUpgradeAvailability);

            return;
		}

        DrawUpgradeAvailability(BalanceManager.Instance.Amount);

        _upgradeTextbox.text = LevelUpText;
        _upgradeCostTextbox.text = info.Details.Levels[info.CurrentLevel].UpgradeCost.ToString();
        _upgradeButton.enabled = true;
    }
}