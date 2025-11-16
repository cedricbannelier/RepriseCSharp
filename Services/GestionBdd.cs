using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector; // ou MySql.Data.MySqlClient si tu utilises MySql.Data
using MonSiteMvc.Models;
using BCrypt.Net;

namespace MonSiteMvc.Services
{
    public class GestionBdd : IGestionBdd
    {
        private readonly string _connectionString;

        public GestionBdd(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<(bool Succès, string Message)> TesterConnexionAsync()
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                // Test simple et propre
                await connection.PingAsync();

                return (true, "✅ Connexion MariaDB OK !");
            }
            catch (Exception ex)
            {
                return (false, $"❌ Erreur : {ex.Message}");
            }
        }
        // Exemple d'insertion simple — adapte la table/colonnes à ta BDD
        public async Task<(bool Succès, string Message)> InsertAsync(InsertDto dto)
        {
            try
            {
                string nom = dto.Userlogin ?? "";
                string pwd = dto.Userpwd;
                bool enable = dto.Userenable;

                string pwdHash = BCrypt.Net.BCrypt.HashPassword(pwd);

                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                await using var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO USER (USERLOGIN, USERPWD, USERENABLE) VALUES (@nom, @pwd, @enable)";
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@pwd", pwdHash);
                cmd.Parameters.AddWithValue("@enable", enable);

                var nb = await cmd.ExecuteNonQueryAsync();

                return (true, $"✅ Insertion réussie ({nb} ligne ajoutée).");
            }
            catch (Exception ex)
            {
                return (false, $"❌ Erreur insertion : {ex.Message}");
            }
        }
    }
}
