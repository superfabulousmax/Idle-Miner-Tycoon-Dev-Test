using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public MineManager mineManager;
    public Shaft startShaft;
    public List<Shaft> shafts;
    public MineData settings;

    void Start ()
    {
        settings = MineData.Instance;
        shafts.Add(startShaft);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    internal void UpdateMineData(double totalMoney)
    {
        Debug.Log("Update mine data" + totalMoney + " shafts" + shafts.Count);
        settings.totalMoney = totalMoney;

        settings.amountOfMineShafts = shafts.Count;
    }
}
