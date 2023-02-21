using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Models;

public class Categoria
{
    public Categoria()
    {
        //boa prática inicializar a coleção na classe que define a coleção.
        this.Produtos= new Collection<Produto>();
    }
    public int CategoriaId { get; set; }
    [Required]
    [MaxLength(80)]
    public string? Nome { get; set; }
    [Required]
    [MaxLength(300)]
    public string? ImagemURL { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
