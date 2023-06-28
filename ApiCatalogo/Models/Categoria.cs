using System.Collections.ObjectModel;

namespace ApiCatalogo.Models;
    public class Categoria
    {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
}
