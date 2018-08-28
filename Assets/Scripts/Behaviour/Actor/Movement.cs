using UnityEngine;

public class Movement
{
    private const float SpeedMultiplier = 1;
    
    private readonly Actor owner;
    private readonly float speed;

    public Vector2 Destination { get; set; }

    public Movement(float speed, Actor owner)
    {
        this.owner = owner;
        this.speed = speed;
    }

    public void Move()
    {
        var position = owner.transform.position;
        position.x = Mathf.MoveTowards(position.x, Destination.x, speed * SpeedMultiplier * Time.deltaTime);
        position.y = Mathf.MoveTowards(position.y, Destination.y, speed * SpeedMultiplier * Time.deltaTime);
        owner.transform.position = position;
    }
}