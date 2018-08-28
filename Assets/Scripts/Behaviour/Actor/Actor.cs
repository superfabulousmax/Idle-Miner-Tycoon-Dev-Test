using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private List<Node> nodes = new List<Node>();

    public ActorSettings Settings;
    public Inventory Inventory;
    public float SkillMultiplier = 1;

    private ActorBehaviour _behaviour;
    private bool _busy;

    private void Start()
    {
        _behaviour = new ActorBehaviour(this, nodes, Settings.Speed);
    }

    private void Update()
    {
        if (!_busy)
        {
            _behaviour.Behave();
        }
    }

    public void TriggerWorking(bool on)
    {
        _busy = on;
    }

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    public void LevelUp(GameSettings settings)
    {
        SkillMultiplier *= settings.ActorUpgradeSkillIncrement;
    }
}