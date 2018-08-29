using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeActorUI : MonoBehaviour
{
    [SerializeField] private FinanceManager _financeManager;
    [SerializeField] private float _price;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Actor _actor;
    [SerializeField] private TextMeshProUGUI _purchaseText;
    [SerializeField] private TextMeshProUGUI _skillText;
    
    public ShaftManager ShaftManager;

    public FinanceManager FinanceManager
    {
        get { return _financeManager; }
        set { _financeManager = value; }
    }


    private void Start()
    {
        var shaftLevel = ShaftManager.Shafts.Count;
        _price *= Mathf.Pow(FinanceManager.Settings.ActorPriceIncrementPerShaft, shaftLevel);
        _actor.SkillMultiplier = Mathf.Pow(FinanceManager.Settings.ActorSkillIncrementPerShaft, shaftLevel);

        UpdateUI();
    }

    private void UpdateUI()
    {
        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _purchaseText.text = Mathf.Round(_price).ToString();
        _skillText.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
    }

    public void UpgradeActor()
    {
        _actor.LevelUp(FinanceManager.Settings);

        FinanceManager.UpdateMoney(-_price);
        _price *= FinanceManager.Settings.ActorUpgradePriceIncrement;

        UpdateUI();

        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _purchaseText.text = Mathf.Round(_price).ToString();
        _skillText.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
    }
}