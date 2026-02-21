using System;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    static public InventoryUI instance;

    [SerializeField] PlayerCharacter owner;
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] InputActionReference toggleInventory;
    GridLayoutGroup grid;
    bool isOpen = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        grid = GetComponentInChildren<GridLayoutGroup>();
        inventoryPanel.SetActive(false);
        Debug.Log("Prefab: " + inventoryItemPrefab);
        Debug.Log("Grid: " + grid);
    }
    private void Update()
    {
       
    }
    private void OnEnable()
    {
        toggleInventory.action.Enable();
        toggleInventory.action.performed += OnToggleInventory;
    }

    private void OnDisable()
    {
        toggleInventory.action.performed -= OnToggleInventory;
        toggleInventory.action.Disable();
    }

    private void OnToggleInventory(InputAction.CallbackContext context)
    {
        ToggleInventory();
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void NotifyItemPicked(InventoryItemDefinition itemDefinition)
    {
       GameObject item = Instantiate(inventoryItemPrefab, grid.transform);
       item.GetComponent<InventoryItemUI>().Init(itemDefinition);
    }

    internal void NotifyInventoryItemUsed(InventoryItemDefinition definition)
    {
        owner.NotifyInventoryItemUsed(definition);
    }

    internal bool Contains(InventoryItemDefinition keyDefinition)
    {
        InventoryItemUI[] items = GetComponentsInChildren<InventoryItemUI>();
        return Array.Find(items, x => x.definition.uniqueItemName == keyDefinition.uniqueItemName) != null;
    }

    internal void Consume(InventoryItemDefinition keyDefinition)
    {
        InventoryItemUI[] items = GetComponentsInChildren<InventoryItemUI>();
        InventoryItemUI item = Array.Find(items, x => x.definition.uniqueItemName == keyDefinition.uniqueItemName);
        item.definition.numUses--;
        if (item.definition.numUses <= 0)
        {
            Destroy(item.gameObject);
        }
    }
}
