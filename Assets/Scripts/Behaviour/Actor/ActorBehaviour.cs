using System.Collections.Generic;

public class ActorBehaviour
{
    private readonly Actor owner;
    private readonly Movement movement;
    private readonly List<Node> JobNodes;

    private Node _currentNode;
    private int _currentNodeIndex;

    public ActorBehaviour(Actor owner, List<Node> nodes, float speed)
    {
        this.owner = owner;
        JobNodes = nodes;
        movement = new Movement(speed, owner);
        Initialize();
    }

    public void Behave()
    {
        if (_currentNode.transform.position != owner.transform.position)
        {
            movement.Move();
            return;
        }

        owner.TriggerWorking(true);
        _currentNode.Reach(owner);
        NextNode();
    }

    private void NextNode()
    {
        if (JobNodes.Count - 1 <= _currentNodeIndex)
        {
            _currentNodeIndex = 0;
        }
        else
        {
            _currentNodeIndex++;
        }

        SetNode(_currentNodeIndex);
    }

    private void SetNode(int index)
    {
        _currentNode = JobNodes[index];
        movement.Destination = _currentNode.transform.position;
    }

    private void Initialize()
    {
        _currentNode = JobNodes[_currentNodeIndex];
        SetNode(_currentNodeIndex);
    }
}