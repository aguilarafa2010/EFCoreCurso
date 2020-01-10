using EFCore.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public interface IEFCoreRepository
    {
        void Add<T>(T entitt) where T : class;
        void Update<T>(T entitt) where T : class;

        
        void Delete<T>(T entitt) where T : class;


        Task<bool> SaveChangeAsync();

        //Assinatura do método
        //Somente o  métod GetAllHerois foi criado com o IEnumerable pelo fato de ser oferatdo uma lista de herois
        Task<Heroi[]> GetAllHerois(bool inlcuirBatalha = false);
        Task<Heroi> GetHeroiById(int id, bool inlcuirBatalha = false);

        Task<Heroi[]> GetHeroisByNome(string nome, bool inlcuirBatalha = false);
        //------------------------------------------------------------
        Task<Batalha[]> GetAllBatalhas(bool inlcuirBatalha = false);
        Task<Batalha> GetBatalhaById(int id, bool inlcuirBatalha = false);

    }
}
