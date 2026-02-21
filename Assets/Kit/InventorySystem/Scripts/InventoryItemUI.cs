using System.Buffers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    
    [SerializeField] public InventoryItemDefinition definition;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    //Image image;
    //TextMeshProUGUI text;
    Button[] buttons;
    InventoryUI inventoryUI;

    enum ButtonAction
    {
        Discard,
        Use,
        Give,
        Sell,
    }

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        //image = GetComponentInChildren<Image>();
        //text = GetComponentInChildren<TextMeshProUGUI>();
        inventoryUI = GetComponentInParent<InventoryUI>();

        
    }
    private void OnEnable()
    {
        buttons[(int)ButtonAction.Discard].onClick.AddListener(OnDiscard);
        buttons[(int)ButtonAction.Use].onClick.AddListener(OnUse);
        buttons[(int)ButtonAction.Give].onClick.AddListener(OnGive);
        buttons[(int)ButtonAction.Sell].onClick.AddListener(OnSell);

    }
    private void OnDisable()
    {
        buttons[(int)ButtonAction.Discard].onClick.RemoveListener(OnDiscard);
        buttons[(int)ButtonAction.Use].onClick.RemoveListener(OnUse);
        buttons[(int)ButtonAction.Give].onClick.RemoveListener(OnGive);
        buttons[(int)ButtonAction.Sell].onClick.RemoveListener(OnSell);

    }
    private void Start()
    {
        Init(definition);
    }
    public void Init(InventoryItemDefinition definition)
    {
        this.definition = Instantiate(definition);
        image.sprite = definition.sprite;
        text.text = definition.uniqueItemName;
    }
    void OnDiscard()
    {
        Debug.Log("OnDiscard");
    }
    void OnUse()
    {
        inventoryUI.NotifyInventoryItemUsed(definition);
        definition.numUses--;
        if (definition.numUses <= 0 )
        {
            Destroy(gameObject);
        }
    }
    void OnGive()
    {
        Debug.Log("OnGive");
    }
    void OnSell()
    {
        Debug.Log("OnSell");
    }
}
