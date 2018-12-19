using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeActorUI : MonoBehaviour
{
    [SerializeField] private FinanceManager _financeManager;
    [SerializeField] private float _price;// this is upgrade price
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Actor _actor;
    [SerializeField] private TextMeshProUGUI _upgradeAmount; // purchase
    [SerializeField] private TextMeshProUGUI _capacityAmount; // skill is capacity
    
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
        _upgradeAmount.text = Mathf.Round(_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
    }

    public void UpgradeActor()
    {
        _actor.LevelUp(FinanceManager.Settings);

        FinanceManager.UpdateMoney(-_price);
        _price *= FinanceManager.Settings.ActorUpgradePriceIncrement;
        UpdateUI();

        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _upgradeAmount.text = Mathf.Round(_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
        Shaft shaft = transform.parent.GetComponentInParent<Shaft>();

        if (shaft != null)
        {
            GameSaveDataController.SetShaftState(shaft);
        }

        if (GetGrandparentTag(transform) == "Warehouse")
        {
            GameSaveDataController.SetWarehouseState();
        }

        if (GetGrandparentTag(transform) == "Elevator")
        {

            GameSaveDataController.SetElevatorState();
        }
    }

    public void ResimUpgradeActor(int rounds)
    {
        for(int i = 0; i < rounds; i++)
        {
            _actor.LevelUp(FinanceManager.Settings);
            _price *= FinanceManager.Settings.ActorUpgradePriceIncrement;
            UpdateUI();
            //_purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
            _upgradeAmount.text = Mathf.Round(_price).ToString();
            _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
        }
        
    }

    private string GetGrandparentTag(Transform child)
    {
        if (child.parent.parent)
        {
            return child.parent.parent.tag;
        }   
        return "";
    }

    //internal void SetUpgradePrice(float price)
    //{
    //    _price = price;
    //}

    //internal void SetSkill(float skill)
    //{
    //    _actor.SkillMultiplier = skill;
    //}

    //public float GetActorSkillMultiplierForUI()
    //{
    //    return _actor.SkillMultiplier;
    //}

    //public float GetUpgradePrice()
    //{
    //    return _price;
    //}

    //public float GetSkill()
    //{
    //    return _actor.Settings.Skill;
    //}
}