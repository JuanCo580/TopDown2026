using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public int targetSceneIndex;

    public GameObject interactionText;
    private bool isPlayerNearby = false;

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => FindInteractionUI();

    private void Start() => FindInteractionUI();

    private void FindInteractionUI()
    {
        interactionText = GameObject.FindWithTag("InteractionUI");

        if (interactionText != null)
        {
            //interactionText.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el Tag 'InteractionUI'.");
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneTransition.instance != null)
                SceneTransition.instance.ChangeScene(targetSceneIndex);
            else
                SceneManager.LoadScene(targetSceneIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && interactionText != null)
        {
            isPlayerNearby = true;
            interactionText.SetActive(true);
            interactionText.GetComponent<TextMeshProUGUI>().text = "Presione E";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && interactionText != null)
        {
            interactionText.GetComponent<TextMeshProUGUI>().text = "";
            isPlayerNearby = false;
        }
    }
}