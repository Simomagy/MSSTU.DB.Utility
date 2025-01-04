#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/13 at 09:12
// * Project: MS_Utility
// * --------------------------------
// * File: IDatabase.cs
// * Edited on 2024/12/22 at 16:12:44
// --------------------------------

#endregion

namespace MSSTU.DB.Utility
{
    /// <summary>
    ///     Interfaccia che permette di interagire con un database SQL Server
    /// </summary>
    public interface IDatabase
    {
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
        public List<Dictionary<string, string>>? ReadDb(string query, Dictionary<string, object>? parameters = null);
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
        public bool UpdateDb(string query, Dictionary<string, object>? parameters = null);
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
        public Dictionary<string, string>? ReadOneDb(string query, Dictionary<string, object>? parameters = null);
    }
}
