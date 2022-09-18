using System.Linq;

class FirstTargetSelector : TargetSelectorWithOffset
{
    protected override Enemy SelectEnemy(Enemy first)
    {
        if (first == LastSelected && EnemyContainer.Enemies.Count() > 1)
            first = EnemyContainer.Enemies.ElementAt(1);

        return first;
    }
}