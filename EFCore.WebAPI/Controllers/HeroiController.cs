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
    public class HeroiController : ControllerBase
    {
        private readonly HeroiContext _context;

        public HeroiController(HeroiContext context)
        {
            _context = context;
        }
        // GET: api/Heroi
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }


        // GET: api/Heroi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Heroi
        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {
                #region
                //Ele criou um heroi novo acrescentando uma lista de armas. Essa seção poderia ser linear mas a organização dessa forma não interfere na programação ex: var heroi = new Heroi { assim, assado };
                //var heroi = new Heroi
                //{
                //    Nome = "Homem de Ferro",
                //    Armas = new List<Arma>
                //    {
                //        new Arma {Nome = "Mac 3"},
                //        new Arma {Nome = "Mac 5"}
                //    }
                //};

                //Aqui salvou esse heroi dentro das tabelas qu estão na parte EFCore.Dominio atravez do -context, que é responsavel por fazer essa conexão. E salvou essas alterações dentro do SQL utilizando o SaveChanges().
                #endregion


                _context.Herois.Add(model);
                _context.SaveChanges();
                return Ok("BAZINGA");
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }
        
        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {

            try
            {
                #region
                //Ele criou um heroi novo acrescentando uma lista de armas. Essa seção poderia ser linear mas a organização dessa forma não interfere na programação ex: var heroi = new Heroi { assim, assado};
                //var heroi = new Heroi
                //{
                //    Id = id,
                //    Nome = "Capitão Caverna",
                //    Armas = new List<Arma>
                //    {
                //        new Arma {Nome = "Mark III"},
                //        new Arma {Nome = "Mark V"}
                //    }
                //};
                #endregion

                //Lembre-se de numca usar o Find para atualizações, visto que ele track *-* os elementos da tabela
                //if(_context.Herois.Find(id)!= null)
                if (_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                        //Aqui salvou esse heroi dentro das tabelas qu estão na parte EFCore.Dominio atravez do -context, que é responsavel por fazer essa conexão. E salvou essas alterações dentro do SQL utilizando o SaveChanges().
                        _context.Herois.Update(model);
                    _context.SaveChanges();
                    return Ok("BAZINGA");
                }

                return Ok("Não Encontrado!");


            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
