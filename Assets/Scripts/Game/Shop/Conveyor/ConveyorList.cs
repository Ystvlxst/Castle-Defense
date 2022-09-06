using UnityEngine;

public class ConveyorList : ReferenceObjectList<Conveyor>
{
    [SerializeField] private ListProgress _conveyorsProgress;

    protected override void AfterUnlocked(Conveyor reference, bool onLoad, string guid)
    {
        if (_conveyorsProgress.Contains(guid))
            return;

        _conveyorsProgress.Add(guid);
        _conveyorsProgress.Save();
    }
}
