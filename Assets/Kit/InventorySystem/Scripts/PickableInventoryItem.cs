using UnityEngine;

public class PickableInventoryItem : MonoBehaviour
{
    [SerializeField] InventoryItemDefinition itemDefinition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            InventoryUI.instance.NotifyItemPicked(itemDefinition);
            Destroy(gameObject);
        }
    }
}
