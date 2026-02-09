using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += NuevaEscenaCargada;
    }

    private void NuevaEscenaCargada(Scene arg0, LoadSceneMode arg1)
    {
        player = FindFirstObjectByType<Player>();
    }

    public void CambiarEstadoPlayer(bool estado)
    {
        player.Interactuando = !estado;
    }
}
