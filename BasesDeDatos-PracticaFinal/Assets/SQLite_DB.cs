using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class SQLite_DB : MonoBehaviour
{
    private string DbLocation = "URI=file:TestDatabase1.db";
    public InputField nameInput;
    public InputField priceInput;
    public Text productList;

    void Start()
    {
        CreateData();
        //CleanProducts();
        //DeleteProducts();
        DisplayProducts();
    }

    public void CreateData()
    {
        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS productsTable (productName VARCHAR(20), productPrice FLOAT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void AddProduct()
    {
        CreateData();

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO productsTable (productName, productPrice) VALUES ('" + nameInput.text + "', '" + priceInput.text + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        DisplayProducts();
    }

    public void DisplayProducts()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM productsTable;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        productList.text += reader["productName"] + "\t\t" +  reader["productPrice"] + "\n";
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    public void CleanProducts()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT NULL FROM productsTable;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        //DisplayProducts();
    }

    public void DeleteProducts()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DROP TABLE productsTable;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        DisplayProducts();
    }

    public void OrderByName()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM productsTable ORDER BY productName;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        productList.text += reader["productName"] + "\t\t" + reader["productPrice"] + "\n";
                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void OrderByPrice()
    {
        productList.text = "";

        using (var connection = new SqliteConnection(DbLocation))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM productsTable ORDER BY productPrice;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        productList.text += reader["productName"] + "\t\t" + reader["productPrice"] + "\n";
                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
