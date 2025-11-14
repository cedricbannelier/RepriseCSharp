using System.Threading.Tasks;
using MonSiteMvc.Models;

namespace MonSiteMvc.Services

{
    public interface IGestionBdd
    {
        /// <summary>
        /// Teste la connexion et renvoie (succès, message).
        /// </summary>
        Task<(bool Succès, string Message)> TesterConnexionAsync();


        /// <summary>
        /// Insert et renvoie (succès, message).
        /// </summary>
        Task<(bool Succès, string Message)> InsertAsync(InsertDto dto);

    }
}
