using UnityEngine;
public class NPCi : MonoBehaviour
{
    public DialogueData dialogo;
    public bool misionCompletada = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            DialogueManager.Instance.AbrirPanel(this);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            DialogueManager.Instance.CerrarPanel();
    }
}