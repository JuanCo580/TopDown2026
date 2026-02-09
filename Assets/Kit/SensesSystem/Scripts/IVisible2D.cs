using UnityEngine;

public interface IVisible2D 
{
    enum Side
    {
        PlayerFriends,
        Enemies,
        Neutrals,
    }
    public int GetPriority();
    public Side GetSide();
}
