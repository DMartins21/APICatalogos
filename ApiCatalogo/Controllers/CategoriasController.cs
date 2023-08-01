﻿using Microsoft.AspNetCore.Mvc;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers;
    [Route("[controller]")]
    [ApiController]
public class CategoriasController : ControllerBase
{

        private readonly AppDbContext _context;
        
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
        return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
             return _context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("{id:int}", Name = "Obter Categoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault();
            if(categoria == null)
            {
                return NotFound($"Não existe a categoria {categoria}");
            }
            return Ok(categoria);
        }
        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if(categoria == null)
            {
            return BadRequest();
            }
         _context.Categorias.Add(categoria);
         _context.SaveChanges();
         return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
        }
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return BadRequest();
        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);
        if (categoria is null)
        {
            return NotFound("Produto não localizado");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return Ok(categoria);
    }
}