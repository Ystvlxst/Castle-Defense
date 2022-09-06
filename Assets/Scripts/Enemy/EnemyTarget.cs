using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public int Queue { get; private set; }
    public Enemy FollowingEnemy { get; private set; }

    public void Follow(Enemy enemy)
    {
        FollowingEnemy = enemy;
    }

    public void AddToQueue()
    {
        Queue++;
    }
}
