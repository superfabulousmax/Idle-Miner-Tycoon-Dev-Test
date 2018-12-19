using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNewMineUI : MonoBehaviour {
    [SerializeField] private GameSettings settings;
    private string [] newMineSceneName = { "New Mine", "Main" };
    private int pos = 0;
    private void Update()
    {
    }
}
