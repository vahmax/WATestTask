using UnityEngine;


public class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingInfo _info;

    [SerializeField]
    private GameObject _interactivePoint;

    private int _currentLevel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpgradeLevel()
	{
        if (IsMaximumLevel)
		{
            Debug.Log("Building level is maximum already.");

            return;
		}

        _currentLevel += 1;
	}

    private bool IsMaximumLevel => _currentLevel == _info.Levels.Length - 1;
}
