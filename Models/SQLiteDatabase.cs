using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Text;
using System.Linq;
using System.IO;
using System;

namespace TravelApp
{
    class SQLiteDatabase
    {
        public string _Path { get; set; } = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public string _BaseName { get; set; } = "MyDatabase";
        public string _DataFile { get; set; }
        public SQLiteConnection sqliteConn;

        public SQLiteDatabase()
        {
            _DataFile = _Path + @"\" + _BaseName + ".sqlite";
            sqliteConn = new SQLiteConnection("Data Source=" + _DataFile + ";Version=3;Integrated Security=SSPI");

            if (!File.Exists(_DataFile))
            {
                //CreateSQLiteDataBaseFile(_Path, _BaseName);

                sqliteConn.Open();

                string sql = "CREATE TABLE IF NOT EXISTS Registration (Id          INTEGER     NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                                                                      "UserName    VARCHAR(30) NOT NULL, " +
                                                                      "UserSurname VARCHAR(30), " +
                                                                      "UserBirdth  TEXT        NOT NULL DEFAULT '01.01.1900', " +
                                                                      "UserNick    VARCHAR(30) NOT NULL, " +
                                                                      "Appeal      VARCHAR(30) NOT NULL, " +
                                                                      "UserEmail   VARCHAR(30) NOT NULL)";
                SQLiteCommand command = new SQLiteCommand(sql, sqliteConn);
                command.ExecuteNonQuery();

                //sql = "CREATE TABLE IF NOT EXISTS WeatherInfo (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                //                                              "city_id INTEGER" +  
                //                                              ")";
                //command = new SQLiteCommand(sql, sqliteConn);
                //command.ExecuteNonQuery();

                //sql = "CREATE TABLE IF NOT EXISTS WeatherForcast ()";
                //command = new SQLiteCommand(sql, sqliteConn);
                //command.ExecuteNonQuery();


                sqliteConn.Close();
            }
            /*
            if (File.Exists(_DataFile))
            {
                sqliteConn.Open();

                string sql = "INSERT INTO users (name, surname, nick, appeal, email) VALUES ('Rustam', 'Zak', 'RZAk', 'Mr.', 'rostik055@gmail.com')";

                SQLiteCommand command = new SQLiteCommand(sql, sqliteConn);
                command.ExecuteNonQuery();

                sql = "SELECT * FROM users ORDER BY name DESC";
                command = new SQLiteCommand(sql, sqliteConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    MessageBox.Show("Name: " + reader["name"] +
                                  "\nSurname: " + reader["surname"]);

                sqliteConn.Close();
            }
            */
        }

        void CreateSQLiteDataBaseFile(string path, string filename)
        {
            SQLiteConnection.CreateFile(String.Format("{0}\\{1}.sqlite", path, filename));
        }
    }
}
