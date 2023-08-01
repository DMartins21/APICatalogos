using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProdutossController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutossController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Produto>> Get()
        {
            var produto = _context.Produtos.AsNoTracking().ToList();
            if(produto is null)
            {
                return NoContent();
            }
            return Ok(produto);
        }

        [HttpGet("{id:int}", Name = "Obter Produtos")]
        public ActionResult Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(produto is null)
            {
                return NotFound($"{id} não encontrado");
            }
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.Id)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto is null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);

        }
    }
}
