#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/13 at 09:12
// * Project: MS_Utility
// * --------------------------------
// * File: IDAO.cs
// * Edited on 2024/12/22 at 16:12:43
// --------------------------------

#endregion

namespace MSSTU.DB.Utility
{
    /// <summary>
    ///     Interfaccia che permette di interagire con un database SQL Server tramite operazioni CRUD
    /// </summary>
    public interface IDAO
    {
        /// <summary>
        ///     Restituisce tutti i record presenti nel database
        /// </summary>
        /// <returns>
        ///     Una <see cref="List{T}" /> di <see cref="Entity" /> contenente tutti i record presenti nel database
        /// </returns>
        public List<Entity> GetRecords();
        /// <summary>
        ///     Crea un record nel database
        /// </summary>
        /// <param name="entity">
        ///     Record da creare sotto forma di <see cref="Entity" />
        /// </param>
        /// <returns>
        ///     <see langword="true" /> se il record è stato creato, altrimenti <see langword="false" />
        /// </returns>
        public bool CreateRecord(Entity entity);
        /// <summary>
        ///     Aggiorna un record nel database
        /// </summary>
        /// <param name="entity">
        ///     Record da aggiornare sotto forma di <see cref="Entity" />
        /// </param>
        /// <returns>
        ///     <see langword="true" /> se il record è stato aggiornato, altrimenti <see langword="false" />
        /// </returns>
        public bool UpdateRecord(Entity entity);
        /// <summary>
        ///     Elimina un record dal database
        /// </summary>
        /// <param name="recordId">
        ///     Id del record da eliminare
        /// </param>
        /// <returns>
        ///     <see langword="true" /> se il record è stato eliminato, altrimenti <see langword="false" />
        /// </returns>
        public bool DeleteRecord(int recordId);
        /// <summary>
        ///     Cerca un record nel database
        /// </summary>
        /// <param name="recordId">
        ///     Id del record da cercare
        /// </param>
        /// <returns>
        ///     Un'istanza di <see cref="Entity" /> se il record è stato trovato, altrimenti <see langword="null" />
        /// </returns>
        public Entity? FindRecord(int recordId);
    }
}
