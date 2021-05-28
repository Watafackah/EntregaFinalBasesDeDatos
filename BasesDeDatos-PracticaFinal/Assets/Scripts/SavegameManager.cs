using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

 /// <summary>  SavegameManager.cs
 /// 
 /// El script realiza la gestión de una base de datos JSON, la cual nos permite guardar y 
 /// cargar información de manera controlada y a su vez nos permite encryptar dicha información
 /// 
 /// </summary>


public class SavegameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public Text enemiesList;

    public void SaveGame()  // Función para guardar el juego en un archivo de guardado llamado "savemultipleobjects.sav" y encriptar dicha información
    {
        JObject jSaveGame = new JObject();  // Crea un objeto JSON

        for (int i = 0; i < enemies.Length; i++)    // Recorre el array de enemigos y los serializa.
        {
            Enemy curEnemy = enemies[i];
            JObject serializedEnemy = curEnemy.Serialize(); // Utiliza la función Serialize del script Enemy
            jSaveGame.Add(curEnemy.name, serializedEnemy);  // Inserta el nombre
        }

        string filePath = Application.persistentDataPath + "/savemultipleobjects.sav";

        byte[] encryptedMessage = Encrypt(jSaveGame.ToString());    // Encrypta los datos del string jSaveGame
        File.WriteAllBytes(filePath, encryptedMessage);     // Escribe la información
    }

    public void LoadGame()  // Función para cargar los datos
    {
        enemiesList.text = "";  // Vacía el texto

        string filePath = Application.persistentDataPath + "/savemultipleobjects.sav";
        Debug.Log("Loading from: " + filePath);
        enemiesList.text += filePath;


        byte[] decryptedMessage = File.ReadAllBytes(filePath);  // Desencrypta el mensaje y procede a leerlos
        string jsonString = Decrypt(decryptedMessage);  // Almacena en un String los datos desencryptados


        JObject jSaveGame = JObject.Parse(jsonString);  
        enemiesList.text += jsonString;     // Escribe en un texto de Unity los valores

        for (int i = 0; i < enemies.Length; i++)    // Recorre el array enemigos
        {
            Enemy curEnemy = enemies[i];
            string enemyJsonString = jSaveGame[curEnemy.name].ToString();   // Escribe la información en un string
            curEnemy.Deserialize(enemyJsonString);  // Deserializa la información de los enemigos
        }
    }

    public void CleanList() // Función para limpiar el texto mostrado en Unity UI
    {
        enemiesList.text = "";
    }

    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};    // Clave que cifra el texto
    byte[] _inicializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };  // Vector por el que se multiplica la Key

    byte[] Encrypt(string message)  // Función para encryptar la información
    {
        AesManaged aes = new AesManaged();  // Creamos el método aes para utilizar las funciones encryptadoras.
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _inicializationVector);  // Se utiliza la Key y el Vector Inicial para encryptar el mensaje

        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        streamWriter.WriteLine(message);    // Escribe el mensaje del archivo

        streamWriter.Close();   //  Cerramos el escritor
        cryptoStream.Close();   // Cerramos el encryptador
        memoryStream.Close();   // Cerramos memoria

        return memoryStream.ToArray();  // Devolvemos el valor de memoria en formato Array
    }

    string Decrypt(byte[] message)  // Función para desencryptar la información
    {
        AesManaged aes = new AesManaged();  // Creamos el método aes para utilizar las funciones encryptadoras.
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _inicializationVector);  // Se utiliza la Key y el Vector Inicial para desencryptar el mensaje

        MemoryStream memoryStream = new MemoryStream(message);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader(cryptoStream);

        string decryptedMessage = streamReader.ReadToEnd();     // Lee el mensaje del archivo

        memoryStream.Close();   // Cerramos memoria
        cryptoStream.Close();   //  Cerramos el encryptador
        streamReader.Close();   //  Cerramos el lector

        return decryptedMessage;
    }
}
