#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/13 at 09:12
// * Project: MS_Utility
// * --------------------------------
// * File: Entity.cs
// * Edited on 2024/12/22 at 16:12:44
// --------------------------------

#endregion

#region

using System.ComponentModel;
using System.Globalization;

#endregion

namespace MSSTU.DB.Utility
{
    /// <summary>
    ///     Classe astratta che rappresenta un'entità di un database. Gestisce tutte le classi sottostanti e contiene un metodo
    ///     per la categorizzazione dei valori
    /// </summary>
    public abstract class Entity
    {

        /// <summary>
        ///     Costruttore della classe <see cref="Entity" />
        /// </summary>
        public Entity(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Costruttore vuoto della classe <see cref="Entity" />
        /// </summary>
        public Entity() { }

        /// <summary>
        ///     Proprietà che rappresenta l'identificativo dell'oggetto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Metodo che restituisce una rappresentazione testuale dell'oggetto
        /// </summary>
        /// <returns>
        ///     Rappresentazione testuale dell'oggetto <see cref="Id" /> sotto forma di <see cref="string" />
        /// </returns>
        public override string ToString()
        {
            return $"Id: {Id}\n";
        }

        /// <summary>
        ///     Metodo che permette di categorizzare i valori di una riga di un database in un oggetto
        /// </summary>
        /// <remarks>
        ///     Categorizza i valori di ogni cella di una riga in base al tipo di dato che rappresentano
        /// </remarks>
        /// <param name="line">
        ///     Riga del database da categorizzare sotto forma di <see cref="Dictionary{TKey, TValue}" /> dove la key è il nome
        ///     della colonna e il valore è il valore in essa
        /// </param>
        public virtual void TypeSort(Dictionary<string, string> line)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;

            foreach (var property in GetType().GetProperties())
            {
                var pName = property.Name.ToLower();
                if (!line.TryGetValue(pName, out var valore))
                    continue;

                try
                {
                    if (property.PropertyType == typeof(int) &&
                        int.TryParse(valore, NumberStyles.Integer, CultureInfo.CurrentCulture, out var parsedInt))
                    {
                        property.SetValue(this, parsedInt);
                    }
                    else if (property.PropertyType == typeof(float) &&
                             float.TryParse(valore, NumberStyles.Float, CultureInfo.CurrentCulture, out var parsedFloat))
                    {
                        property.SetValue(this, parsedFloat);
                    }
                    else if (property.PropertyType == typeof(double) &&
                             double.TryParse(valore, NumberStyles.Float, CultureInfo.CurrentCulture, out var parsedDouble))
                    {
                        property.SetValue(this, parsedDouble);
                    }
                    else if (property.PropertyType == typeof(decimal) &&
                             decimal.TryParse(valore, NumberStyles.Float, CultureInfo.CurrentCulture, out var parsedDecimal))
                    {
                        property.SetValue(this, parsedDecimal);
                    }
                    else if (property.PropertyType == typeof(DateTime) &&
                             DateTime.TryParse(valore, CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDateTime))
                    {
                        property.SetValue(this, parsedDateTime);
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        var convertedValue = converter.ConvertFromString(null, CultureInfo.CurrentCulture, valore);
                        property.SetValue(this, convertedValue);
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine($"Errore durante la conversione della proprietà '{property.Name}': {ex.Message}");
                }
            }
        }
    }
}
