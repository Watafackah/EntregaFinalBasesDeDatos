using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager1 : MonoBehaviour
{
    public void Load(string scenename)      // Función para cambiar de escena con el nombre de la escena configurable en el editor
    {
        Debug.Log("SceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }

}
