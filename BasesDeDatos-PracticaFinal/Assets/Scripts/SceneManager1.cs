using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager1 : MonoBehaviour
{
    public void Load(string scenename) 
    {
        Debug.Log("SceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }

}
