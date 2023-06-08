using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context):base(context) { }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
           return Get().Include(x=>x.Produtos).ToList();
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters parameters)
        {  
           return PagedList<Categoria>.ToPagedList(Get().Include(x => x.Produtos).OrderBy(x => x.Nome), parameters.PageNumber, parameters.PageSize);
        }
    }
}
