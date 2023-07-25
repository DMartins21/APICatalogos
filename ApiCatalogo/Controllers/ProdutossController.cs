using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if(produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            return produtos;
        }
        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == id);
            if(produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            return produto;
        }
        [HttpPost("{id:int}", Name="ObterProduto")]
        public ActionResult Post(Produto produto)
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
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto is null)
            {
                return NotFound("Produto não localizado");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }
    }
}
