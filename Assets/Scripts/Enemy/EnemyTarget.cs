using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public Enemy FollowingEnemy { get; private set; }

    public void Follow(Enemy enemy)
    {
        FollowingEnemy = enemy;
    }
}
