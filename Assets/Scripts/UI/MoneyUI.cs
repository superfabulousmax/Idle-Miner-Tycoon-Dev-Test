using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private FinanceManager financeManager;

    private void Start()
    {
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = financeManager.TotalMoney.ToString();
    }
}