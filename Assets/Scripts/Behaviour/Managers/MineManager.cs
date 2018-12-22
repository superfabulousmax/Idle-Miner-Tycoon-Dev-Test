using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour {
    [SerializeField] private GameSettings settings;

    [SerializeField] private List<Mine> mines;

    // Use this for initialization
    void Start () {
        mines = new List<Mine>();
    }
}
