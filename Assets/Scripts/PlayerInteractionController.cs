using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("UI & Stats")]
    public int coins = 0;
    public TextMeshProUGUI coinText;

    [Header("Combat")]
    public int attackDamage = 20;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    private GameObject currentInteractable;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { Attack(); }

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {HandleInteraction();}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            UpdateCoinUI();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("NPC") || other.CompareTag("Door")) { currentInteractable = other.gameObject; }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC") || other.CompareTag("Door")) { currentInteractable = null; }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            if (eh != null) eh.TakeDamage(attackDamage);
        }
    }
    void HandleInteraction()
    {
        if (currentInteractable.CompareTag("NPC"))
        {
            Debug.Log("Iniciando Diálogo...");
        }
        else if (currentInteractable.CompareTag("Door"))
        {
            string nextScene = "NombreDeTuEscena"; 
            SceneManager.LoadScene(nextScene);
        }
    }

    void UpdateCoinUI() { if (coinText != null) coinText.text =  coins.ToString(); }
}
