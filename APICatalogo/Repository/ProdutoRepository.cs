using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context):base(context) { }

        public PagedList<Produto> GetProdutos(ProdutosParameters parameters)
        {
            //return Get()
            //    .OrderBy(on=>on.Nome)
            //    .Skip((parameters.PageNumber -1) * parameters.PageSize)
            //    .Take(parameters.PageSize)
            //    .ToList();

            return PagedList<Produto>.ToPagedList(Get().OrderBy(x=>x.Nome), parameters.PageNumber, parameters.PageSize);
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c=>c.Preco).ToList();
        }
    }
}
