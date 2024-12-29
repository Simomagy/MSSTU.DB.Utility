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
        /// <inheritdoc cref="Database.ReadDb" />
        public List<Dictionary<string, string>>? ReadDb(string query, Dictionary<string, object>parameters);
        /// <inheritdoc cref="Database.UpdateDb" />
        public bool UpdateDb(string query, Dictionary<string, object> parameters);
        /// <inheritdoc cref="Database.ReadOneDb" />
        public Dictionary<string, string>? ReadOneDb(string query, Dictionary<string,object> parameters);
    }
}
