using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Libreria añadida para utilizar las funciones de UI de Unity
using Mono.Data.Sqlite; // Libreria añadida para utilizar las funciones de SQLite
using System.Data;

public class SQLite_DB : MonoBehaviour
{
    private string DbLocation = "URI=file:TestDatabase1.db";    // Damos una localización a la base de datos y la nombramos
    public InputField nameInput;    // Creamos un objeto InputField y lo nombramos
    public InputField priceInput;   // Creamos un objeto InputField y lo nombramos
    public Text productList;    // Creamos un objeto Text y lo nombramos

    void Start()
    {
        CreateData();   //Iniciamos la función [CreateData()] para cargar al arranque del programa
        DisplayProducts();  //Iniciamos la función [DisplayProducts()] para cargar al arranque del programa
    }

    public void CreateData()    // Creamos la función [CreateData()], que servirá para crear una base de datos
    {
        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS productsTable (productName VARCHAR(20), productPrice FLOAT);";    //Crea una tabla si no existe una previa, llamada productsTable que contiene en su interior un VARCHAR de 20 caracteres ASCII y un float para determinar el precio.
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void AddProduct()
    {
        CreateData();   // Utilizamos la función [CreateData()]

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();
            using(var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "INSERT INTO productsTable (productName, productPrice) VALUES ('" + nameInput.text + "', '" + priceInput.text + "');";    // Inserta en la tabla los valores de los InputFields en Unity
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
        DisplayProducts(); // Muestra los productos
    }

    public void DisplayProducts()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "SELECT * FROM productsTable;";   // Lee todo de la tabla productsTable

                using (IDataReader reader = command.ExecuteReader())    // Utilizamos el lector de datos de System.Data para ejecutar un comando de lectura
                {
                    while (reader.Read())   // Mientras existan datos para leer, ejecutará la siguiente linea
                        productList.text += reader["productName"] + "\t\t" +  reader["productPrice"] + "\n"; // Introduce los valores del lector en el texto a mostrar por pantalla en Unity
                    reader.Close(); //Cierra el lector de SQL
                }
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void CleanProducts()
    {
        productList.text = "";  // Vaciamos el texto mostrado en Unity

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "SELECT NULL FROM productsTable;";
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void DeleteProducts()
    {
        productList.text = "";  // Vaciamos el texto mostrado en Unity

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "DROP TABLE productsTable;";
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
        DisplayProducts();  // Muestra los productos
    }

    public void OrderByName()
    {
        productList.text = "";  // Vaciamos el texto mostrado en Unity

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "SELECT * FROM productsTable ORDER BY productName;";  // Lee todo de la tabla productsTable ordenado por orden alfabético de los productos
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())   // Mientras existan datos para leer, ejecutará la siguiente linea
                        productList.text += reader["productName"] + "\t\t" + reader["productPrice"] + "\n"; // Introduce los valores del lector en el texto a mostrar por pantalla en Unity
                    reader.Close(); //Cierra el lector de SQL
                }
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void OrderByPrice()
    {
        productList.text = "";  // Vaciamos el texto mostrado en Unity

        using (var connection = new SqliteConnection(DbLocation))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "SELECT * FROM productsTable ORDER BY productPrice;"; //Lee todo de la tabla productsTable en orden de precio
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())   // Mientras existan datos para leer, ejecutará la siguiente linea
                        productList.text += reader["productName"] + "\t\t" + reader["productPrice"] + "\n"; // Introduce los valores del lector en el texto a mostrar por pantalla en Unity
                    reader.Close(); //Cierra el lector de SQL
                }
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }
}
