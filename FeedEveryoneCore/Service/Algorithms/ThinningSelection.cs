namespace FeedEveryone.Service.Algorithms;

public class ThinningSelection
{
    public ThinningSelection(
        int bigValue, int smallValue, Action<int, int> selection
    )
    {
        this.bigValue = bigValue;
        this.smallValue = smallValue;
        this.selection = selection;
    }

    public void Select()
    {
        int rest = (bigValue - 1) % (smallValue - 1);
        int accum = bigValue;
        for (int index = 0, baseIndex = 0; index < smallValue; baseIndex++)
        {
            if(accum >= bigValue)
            {
                selection(index, baseIndex);

                index++;
                if (rest > 0)
                {
                    baseIndex++;
                    rest--;
                }
                accum -= bigValue;
            }
            accum += smallValue;
        }
    }


    private readonly int bigValue;
    private readonly int smallValue;
    private readonly Action<int, int> selection;
}
