using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// El script servirá para recopilar los datos necesarios de la base de datos y enviarlos
/// a la UI de Unity, permitiendo mostrarlos en pantalla
/// 
/// </summary>

public class ModifyEnemy : MonoBehaviour
{
    public InputField inputName;    
    public InputField inputHP;
    public InputField inputLevel;

    public Enemy enemyInit;


    public void ModifyName()    // Función para modificar el nombre con el contenido del InputField
    {
        enemyInit._name = inputName.text;
    }

    public void ModifyHP()   // Función para modificar la vida con el contenido del InputField
    {
        if (inputName != null)
        {
            enemyInit._hp = inputHP.text;
        }
    }

    public void ModifyLevel()   // Función para modificar el nivel con el contenido del InputField
    {
        if (inputLevel != null)
        {
            enemyInit._level = inputLevel.text;
        }
    }
}
