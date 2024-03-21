using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("Produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        try
        {
            return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        return _context.Categorias.AsNoTracking().ToList();
    }

    [HttpGet("{id:int}", Name = "Obter Categoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault();
            if (categoria == null)
            {
                return NotFound($"Não existe a categoria {categoria}");
            }
            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
        }

    }
    [HttpPost]
    public ActionResult<Categoria> Post(Categoria categoria)
    {
        if (categoria == null)
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
            return NotFound($"Categoria:{categoria} não localizado");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return Ok(categoria);
    }
}
