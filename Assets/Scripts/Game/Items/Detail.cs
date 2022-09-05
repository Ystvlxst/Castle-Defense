using System;

public class Detail : Stackable
{
    public override StackableType Type => StackableType.Detail;
    public bool Taken { get; private set; } =  false;

    public void Take()
    {
        if (Taken)
            throw new InvalidOperationException();
        
        Taken = true;
    }
}
