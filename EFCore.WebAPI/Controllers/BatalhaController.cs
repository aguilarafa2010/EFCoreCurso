﻿using System;
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
        private readonly HeroiContext _context; 

        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }
        // GET: api/Batalha
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Batalha());
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Batalha
        [HttpPost]
        public ActionResult Post(Batalha batalha)
        {
            try
            {
                _context.Batalhas.Add(batalha);
                _context.SaveChanges();
                return Ok("BAZINGA");
            }
            catch (Exception ex)
            {

                return BadRequest("Impossível de cadastrar a batalha");
            }

        }

        // PUT: api/Batalha/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha batalha)
        {

            try
            {
                
                if (_context.Batalhas.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    //Aqui salvou essa batalha dentro das tabelas qu estão na parte EFCore.Dominio atravez do -context, que é responsavel por fazer essa conexão. E salvou essas alterações dentro do SQL utilizando o SaveChanges().
                    _context.Batalhas.Update(batalha);
                    _context.SaveChanges();
                    return Ok("BAZINGA");
                }

                return Ok("Batalha não Encontrado!");


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
