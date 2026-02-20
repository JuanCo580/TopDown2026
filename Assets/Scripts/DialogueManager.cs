using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject panelDialogo;
    public TMPro.TextMeshProUGUI textoUI;

    void Awake() => Instance = this;

    public void AbrirPanel(NPCi npc)
    {
        panelDialogo.SetActive(true);
        if (npc.dialogo.esMision && npc.misionCompletada)
        {
            textoUI.text = npc.dialogo.respuestaMisionCompletada;
        }
        else
        {
            textoUI.text = npc.dialogo.frases[0];
        }
    }

    public void CerrarPanel()
    {
        panelDialogo.SetActive(false);
    }
}