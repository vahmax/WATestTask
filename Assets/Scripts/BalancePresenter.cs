using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class BalancePresenter : MonoBehaviour
{
    private Text _balanceTextbox;

    void Awake()
	{
        _balanceTextbox = GetComponent<Text>();
	}

    void Start()
    {
        DrawBalance(0);
    }

    public void DrawBalance(int balance) => _balanceTextbox.text = balance.ToString();
}
