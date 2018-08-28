using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyShaftUI : MonoBehaviour
{
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private TextMeshProUGUI _purchaseText;
    [SerializeField] private float _price;
    [SerializeField] private FinanceManager _financeFinanceManager;

    public FinanceManager FinanceManager
    {
        get { return _financeFinanceManager; }
        set { _financeFinanceManager = value; }
    }

    private void Start()
    {
        _price = FinanceManager.NextShaftPrice;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _purchaseText.text = Mathf.Round(_price).ToString();
    }
}