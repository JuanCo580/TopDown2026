using UnityEngine;

public class Coin : MonoBehaviour, IVisible2D
{
    int IVisible2D.GetPriority()
    {
        return 0;
    }

    IVisible2D.Side IVisible2D.GetSide()
    {
        return IVisible2D.Side.Neutrals;
    }
}
