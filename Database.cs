#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/13 at 09:12
// * Project: MS_Utility
// * --------------------------------
// * File: Database.cs
// * Edited on 2024/12/22 at 13:12:56
// --------------------------------

#endregion

#region

using System.Data;
using Microsoft.Data.SqlClient;

#endregion

namespace MSSTU.DB.Utility
{
    /// <summary>
    ///     Classe che implementa l'interfaccia <see cref="IDatabase" /> e permette di interagire con un database SQL Server
    /// </summary>
    public class Database : IDatabase
    {

        private readonly string _connectionString;
        private SqlConnection _connection;
        /// <summary>
        ///     Costruttore della classe <see cref="Database" />. Inizializza la connessione al database
        /// </summary>
        /// <param name="dbName">
        ///     Nome del database a cui connettersi
        /// </param>
        /// <param name="server">
        ///     Nome del server a cui connettersi. Default: "MSSTU"
        /// </param>
        public Database(string dbName, string server = "MSSTU")
        {
            _connectionString = $"Data Source={server};Initial Catalog={dbName};Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            _connection = new SqlConnection(_connectionString);
        }

        private SqlConnection Connection
        {
            get
            {
                if (_connection.State is ConnectionState.Closed or ConnectionState.Broken)
                {
                    _connection = new SqlConnection(_connectionString);
                }
                return _connection;
            }
        }

        /// <inheritdoc cref="IDatabase.ReadDb"/>
        public List<Dictionary<string, string>>? ReadDb(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();
                    using (SqlCommand cmd = new(query, connection))
                    {
                        if (parameters != null)
                            AddParameters(cmd, parameters);
                        using (var dr = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> response = new();
                            while (dr.Read())
                            {
                                Dictionary<string, string> line = new();
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    var valore = dr.GetValue(i)?.ToString() ?? string.Empty;
                                    line.Add(dr.GetName(i).ToLower(), valore);
                                }
                                response.Add(line);
                            }
                            connection.Close();
                            return response;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"{e.Message}\n {query} ");
                _connection.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _connection.Close();
                return null;
            }
        }

        /// <inheritdoc cref="IDatabase.UpdateDb"/>
        public bool UpdateDb(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();
                    using (SqlCommand cmd = new(query, connection))
                    {
                        if (parameters != null)
                            AddParameters(cmd, parameters);
                        var response = cmd.ExecuteNonQuery();
                        connection.Close();
                        return response > 0;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"{e.Message}\n {query} ");
                _connection.Close();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _connection.Close();
                return false;
            }
        }

        /// <inheritdoc cref="IDatabase.ReadOneDb"/>
        public Dictionary<string, string>? ReadOneDb(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                var result = ReadDb(query, parameters);
                return result?.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        private void AddParameters(SqlCommand cmd, Dictionary<string, object>? parameters)
        {
            if (parameters == null) return;
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }
}
