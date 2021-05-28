using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Data;

/// <summary>   Enemy.cs
/// 
/// El script permite el guardado de ciertas carácteristicas en un objeto
/// de manera que todos posean esos atributos con valores diferentes.
/// 
/// </summary>

public class Enemy : MonoBehaviour
{
    public string _hp;  
    public string _name;
    public string _level;

    public JObject Serialize()      // Sirve para que todos los objetos con este script se serializen y reciban los datos como string
    {
        string jsonString = JsonUtility.ToJson(this);
        JObject retVal = JObject.Parse(jsonString);
        return retVal;
    }

    public void Deserialize(string jsonString)      // Sirve para dejar de guardar un objeto y sobrescribir
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
}
