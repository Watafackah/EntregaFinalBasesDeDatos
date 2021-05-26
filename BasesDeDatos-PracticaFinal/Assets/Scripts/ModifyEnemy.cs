using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyEnemy : MonoBehaviour
{
    public InputField inputName;
    public InputField inputHP;
    public InputField inputLevel;

    public Enemy enemyInit;


    public void ModifyName()
    {
        enemyInit._name = inputName.text;
    }

    public void ModifyHP()
    {
        if (inputName != null)
        {
            enemyInit._hp = inputHP.text;
        }
    }

    public void ModifyLevel()
    {
        if (inputLevel != null)
        {
            enemyInit._level = inputLevel.text;
        }
    }
}
