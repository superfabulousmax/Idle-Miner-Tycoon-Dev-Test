﻿using UnityEngine;

public class NextShaft : MonoBehaviour
{
    public Shaft Shaft;

    public void BuyShaft()
    {
        Shaft.Manager.BuildNextShaft();
    }
}