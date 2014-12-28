using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


namespace ssa.cashlesspayment.it.DataAcces
{
    public class DAKassa
    {
        private const string CONNECTIONSTRING = "DefaultConnection";

        //Get kassas
        public static List<Kassa> GetKassas()
        {
            List<Kassa> kassas = new List<Kassa>();

            string sql = "SELECT * FROM Kassas";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while(reader.Read())
            {
                Kassa k = new Kassa()
                {
                    Id = int.Parse(reader["ID"].ToString()),
                    KassaNaam = reader["KassaNaam"].ToString(),
                    Toestel = reader["Toestel"].ToString(),
                    Aankoopdatum = DateTime.Parse(reader["Aankoopdatum"].ToString()),
                    Vervaldatum = DateTime.Parse(reader["Vervaldatum"].ToString())
                };
                kassas.Add(k);
            }

            reader.Close();
            return kassas;
        }

        //Nieuwe kassa in de database opslaan.
        public static int SaveKassa(string kassanaam, string toestel, string aankoopdatum, string vervaldatum)
        {
            string sql = "INSERT INTO Kassa VALUES(@KassaNaam, @Toestel, @Aankoopdatum, @Vervaldatum)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@KassaNaam", kassanaam);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Toestel", toestel);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Aankoopdatum", aankoopdatum);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Vervaldatum", vervaldatum);

            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        //TODO
        //Update kassas
    }
}