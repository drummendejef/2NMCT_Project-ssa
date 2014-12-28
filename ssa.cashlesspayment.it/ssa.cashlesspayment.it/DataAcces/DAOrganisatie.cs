using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


namespace ssa.cashlesspayment.it.DataAcces
{
    public class DAOrganisatie
    {
        private const string CONNECTIONSTRING = "DefaultConnection";


        //Get organisaties
        public static List<Organisatie> GetOrganisaties()
        {
            List<Organisatie> organisaties = new List<Organisatie>();

            string sql = "SELECT * FROM Organisaties";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while(reader.Read())
            {
                Organisatie o = new Organisatie()
                {
                    Id = int.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    OrganisatieNaam = reader["OrganisatieNaam"].ToString(),
                    Adres = reader["Adres"].ToString(),
                    Email = reader["Email"].ToString(),
                    Telefoonnr = reader["Telefoonnr"].ToString()
                };
                organisaties.Add(o);
            }

            reader.Close();
            return organisaties;
        }

        //Nieuwe organisatie in de database opslaan.
        public static int SaveOrganisatie(string login, string password, string dbname, string dblogin,string dbpassword ,string organisatienaam, string adres, string email, string telefoonnr)
        {
            string sql = "INSERT INTO Organisaties VALUES(@Login, @Password, @DbName, @DbLogin, @DbPassword, @OrganisatieNaam, @Adres, @Email, @Telefoonnr)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", dbname);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", dblogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", dbpassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisatieNaam", organisatienaam);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Adres", adres);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Telefoonnr", telefoonnr);

            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }


        //TODO
        //Update organisaties

    }
}