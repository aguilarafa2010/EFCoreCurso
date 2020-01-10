using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;
        #region
        //private readonly HeroiContext _context; 

        //public BatalhaController(HeroiContext context)
        //{
        //    _context = context;
        //}
        // GET: api/Batalha
        #endregion

        public BatalhaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult > Get()
        {
            try
            {
                var herois = await _repo.GetAllBatalhas(true);
                return Ok(herois);
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var herois = await _repo.GetBatalhaById(id, true);
                return Ok(herois);
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // POST: api/Batalha
        [HttpPost]
        public async Task<IActionResult> Post(Batalha batalha)
        {
            try
            {
                _repo.Add(batalha);
                if(await _repo.SaveChangeAsync())
                    return Ok("BAZINGA");
                

            }
            catch (Exception ex)
            {

                return BadRequest("Impossível de cadastrar a batalha");
            }


            return BadRequest("Não Salvou");
        }

        // PUT: api/Batalha/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha batalha)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);

                if (heroi != null)
                {
                    _repo.Update(heroi);

                    if (await _repo.SaveChangeAsync())
                        return Ok("BAZINGA");
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não Deletado");
        }
    

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var heroi = await _repo.GetBatalhaById(id);

                if (heroi != null)
                {
                    _repo.Delete(heroi);

                    if(await _repo.SaveChangeAsync() )
                        return Ok("BAZINGA");
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não Deletado");
        }
    }
}
