using UnityEngine;

[CreateAssetMenu(fileName = "NuevoDialogo", menuName = "Dialogos/Sistema de Diálogo")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] frases; 

    [Header("Configuración de Misión")]
    public bool esMision;
    public string idItemRequerido;
    [TextArea(3, 10)]
    public string respuestaMisionIncompleta;
    public string respuestaMisionCompletada;
}