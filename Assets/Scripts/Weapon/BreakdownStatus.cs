using UnityEngine;

public class BreakdownStatus : MonoBehaviour
{
    public bool Broken { get; private set; }

    public void Break()
    {
        if(Broken)
            return;
        
        Broken = true;
    }

    public void Repair()
    {
        if(!Broken)
            return;

        Broken = false;
    }
}