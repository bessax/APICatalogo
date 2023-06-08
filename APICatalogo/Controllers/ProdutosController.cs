using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository.UofW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            var produtos =  _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
            return _mapper.Map<List<ProdutoDTO>>(produtos);
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters parameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(parameters);

            var metadata = new
            {

                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metadata));

            var produtosDTO= _mapper.Map<List<ProdutoDTO>>(produtos);
            if (produtosDTO is null)
            {
                return NotFound("Produtos não encontrados.");
            }
            return Ok(produtosDTO);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(x=>x.ProdutoId==id);
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            if (produtoDTO is null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produtoDTO);
        }

        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null )
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produtoDTO.ProdutoId }, produtoDTO);
        }

        //Tenho que atualizar todos os dados do produtos.
        [HttpPut("{id:int}")]
        public ActionResult Put(int id,ProdutoDTO produtoDto)
        {

            if (id != produtoDto.ProdutoId)
            {
                return BadRequest("Id's enviados diferentes.");
            }

            var produto = _mapper.Map<Produto>(produtoDto);
            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(x => x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }

    }
}
