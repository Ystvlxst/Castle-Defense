using System;
using UnityEngine;

[RequireComponent(typeof(DropableItem))]
public class Detail : Stackable
{
    [field: SerializeField] public bool Taken { get; private set; } =  false;
    
    public override StackableType Type => StackableType.Detail;

    private DropableItem _dropableItem;

    private void Start()
    {
        _dropableItem = GetComponent<DropableItem>();
    }

    public void Take()
    {
        if (Taken)
            throw new InvalidOperationException();
        
        _dropableItem.DisableGravity();
        Taken = true;
    }
}
