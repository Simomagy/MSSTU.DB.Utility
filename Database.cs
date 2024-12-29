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

using Microsoft.Data.SqlClient;

#endregion

namespace MSSTU.DB.Utility
{
    /// <summary>
    ///     Classe che implementa l'interfaccia <see cref="IDatabase" /> e permette di interagire con un database SQL Server
    /// </summary>
    public class Database : IDatabase
    {

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
            Connection = new SqlConnection(
                $"Data Source={server};Initial Catalog={dbName};Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
        }
        SqlConnection Connection { get; }

        /// <summary>
        ///     Metodo che legge i record di una tabella del database
        /// </summary>
        /// <remarks>
        ///     Legge i record e li aggiunge a una <see cref="List{T}" /> di <see cref="Dictionary{TKey, TValue}" /> dove la key è
        ///     il nome della colonna
        ///     e il valore è il valore in essa
        /// </remarks>
        /// <param name="query">
        ///    Query parametrizzata da eseguire
        /// </param>
        /// <param name="parameters">
        ///   Parametri della query
        /// </param>
        /// <returns></returns>
        public List<Dictionary<string, string>>? ReadDb(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    using (SqlCommand cmd = new(query, Connection))
                    {
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
                            return response;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"{e.Message}\n {query} ");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        ///     Esegue una query di inserimento nel database
        /// </summary>
        /// <param name="query">
        ///    Query parametrizzata da eseguire
        /// </param>
        /// <param name="parameters">
        ///   Parametri della query
        /// </param>
        /// <returns><see langword="true" /> se l'inserimento è andato a buon fine, <see langword="false" /> altrimenti</returns>
        public bool UpdateDb(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    using (SqlCommand cmd = new(query, Connection))
                    {
                        AddParameters(cmd, parameters);
                        var response = cmd.ExecuteNonQuery();
                        return response > 0;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"{e.Message}\n {query} ");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        ///     Esegue una query di lettura nel database e restituisce il primo record
        /// </summary>
        /// <param name="query">
        ///    Query parametrizzata da eseguire
        /// </param>
        /// <param name="parameters">
        ///    Parametri della query
        /// </param>
        /// <returns>
        ///     Un <see cref="Dictionary{TKey, TValue}" /> con i valori del primo record, <see langword="null" /> se non ci
        ///     sono record
        /// </returns>
        public Dictionary<string, string>? ReadOneDb(string query, Dictionary<string, object> parameters)
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

        private void AddParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }
}
