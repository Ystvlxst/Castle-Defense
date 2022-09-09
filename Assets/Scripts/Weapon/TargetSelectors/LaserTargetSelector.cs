using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserTargetSelector : TargetSelector
{
    public override Vector3 SelectTarget() => 
        EnemyContainer.Enemies.ElementAt(Random.Range(0, EnemyContainer.Enemies.Count())).transform.position;
    
    public override Enemy SelectEnemyTarget() => 
        EnemyContainer.Enemies.ElementAt(Random.Range(0, EnemyContainer.Enemies.Count()));
}
