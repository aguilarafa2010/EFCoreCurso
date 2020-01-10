﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContext _context;

        public EFCoreRepository(HeroiContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);

        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0; 
        }

        public async Task<Heroi[]> GetAllHerois(bool inlcuirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Id)
                .Include(h => h.Armas);

            if (inlcuirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);
            }
            

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool inlcuirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Id)
                .Include(h => h.Armas);

            if (inlcuirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);
            }


            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> GetHeroisByNome(string nome, bool inlcuirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                            .Include(h => h.Id)
                            .Include(h => h.Armas);

            if (inlcuirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);
            }


            query = query.AsNoTracking()
                .Where(h => h.Nome.Contains(nome))
                .OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }
        //--------------------------------------------
        public async Task<Batalha[]> GetAllBatalhas(bool inlcuirHerois = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;


            if (inlcuirHerois)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Heroi);
            }


            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id, bool inlcuirHerois = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;


            if (inlcuirHerois)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Heroi);
            }


            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
