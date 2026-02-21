using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class InventoryItemDefinition : ScriptableObject
{
    public Sprite sprite;
    [FormerlySerializedAs("itemName")]
    public string uniqueItemName;
    public float healthRecovery;
    public int bullets;
    public int numUses;
}
