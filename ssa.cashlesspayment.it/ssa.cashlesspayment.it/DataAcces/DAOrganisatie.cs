using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.Encryptie;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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
                    DbPassword = Cryptography.Decrypt(reader["DbPassword"].ToString()),
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

        //Get organisatie bij Id
        public static Organisatie GetOrganisatie(int id)
        {
            string sql = "SELECT * FROM Organisaties WHERE ID=@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                Organisatie o = new Organisatie()
                {
                    Id = int.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = Cryptography.Decrypt(reader["DbPassword"].ToString()),
                    OrganisatieNaam = reader["OrganisatieNaam"].ToString(),
                    Adres = reader["Adres"].ToString(),
                    Email = reader["Email"].ToString(),
                    Telefoonnr = reader["Telefoonnr"].ToString()
                };
                reader.Close();
                return o;
            }

            return null;
        }

        //Nieuwe organisatie in de database opslaan.
        public static int SaveOrganisatie(string login, string password, string dbname, string dblogin,string dbpassword ,string organisatienaam, string adres, string email, string telefoonnr)
        {
            string sql = "INSERT INTO Organisaties VALUES(@Login, @Password, @DbName, @DbLogin, @DbPassword, @OrganisatieNaam, @Adres, @Email, @Telefoonnr)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", dbname);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", dblogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", Cryptography.Encrypt(dbpassword));
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisatieNaam", organisatienaam);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Adres", adres);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Telefoonnr", telefoonnr);

            int id = Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);

            //Oproepen "aanmaken DB" functie
            GenerateDatabase(login, password, dbname, dblogin, dbpassword, organisatienaam, adres, email, telefoonnr);
          
            return id;
        }

        //Edit organisatie
        public static int EditOrganisatie(string Login, string Password, string DbName, string DbLogin, string DbPassword, string OrganisatieNaam, string Adres, string Email, string Telefoonnr, int Id)
        {
            string sql = "UPDATE Organisaties SET Login=@Login, Password = @Password, DbName = @DbName, DbLogin = @DbLogin, DbPassword = @DbPassword, OrganisatieNaam = @OrganisatieNaam, Adres = @Adres, Email = @Email, Telefoonnr = @Telefoonnr WHERE ID = @Id";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", DbLogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", Cryptography.Encrypt(DbPassword));
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisatieNaam", OrganisatieNaam);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Adres", Adres);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Telefoonnr", Telefoonnr);
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING, "@Id", Id);



            int rowsupdated = Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);

            return rowsupdated;
        }

        //Delete organisatie
        public static void DeleteOrganisatie(int Id)
        {
            string sql = "DELETE FROM Organisaties WHERE ID=@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Id", Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1);
        }

        //Persoonlijke database genereren
        public static void GenerateDatabase(string login, string password, string dbname, string dblogin, string dbpassword, string organisatienaam, string adres, string email, string telefoonnr)
        {
            //Database aanmaken
            //string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt")); only for the web
            //string create = File.ReadAllText(@"..\..\Data\create.txt");//Voor de desktop
            string create = File.ReadAllText(@"E:\School\2014-2015\Server Side Applications\Project-ssa\ssa.cashlesspayment.it\ssa.cashlesspayment.it\Data\create.txt");//Voor de desktop
            string sql = create.Replace("@@DbName", dbname).Replace("@@DbLogin", dblogin).Replace("@@DbPassword", dbpassword);

            //overlopen
            foreach(string commandText in RemoveGo(sql))
            {
                Database.ModifyData(CONNECTIONSTRING, commandText);
            }

            //Login, gebruiker en tabellen aanmaken
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(CONNECTIONSTRING);

                //string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt")); // only for the web
                //string fill = File.ReadAllText(@"..\Data\fill.txt");//Voor desktop
                string fill = File.ReadAllText(@"E:\School\2014-2015\Server Side Applications\Project-ssa\ssa.cashlesspayment.it\ssa.cashlesspayment.it\Data\fill.txt");//Voor desktop
                string sql2 = fill.Replace("@@DbName", dbname).Replace("@@DbLogin", dblogin).Replace("@@DbPassword", dbpassword);

                foreach(string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch(Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        
        //Gebruikt om de file te lezen
        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }

    }
}