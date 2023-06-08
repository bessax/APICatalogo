using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository.UofW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger, IMapper mapper)
    {
        _uof = uof;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("categorias")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        var categoria = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(categoria);
        if (categoriaDTO is null)
        {
            return NotFound("Categoria não encontrados.");
        }
        return Ok(categoriaDTO);
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriasParameters parameters)
    {
        try
        {
            var categoria = _uof.CategoriaRepository.GetCategorias(parameters);

            var metadata = new
            {

                categoria.TotalCount,
                categoria.PageSize,
                categoria.CurrentPage,
                categoria.TotalPages,
                categoria.HasNext,
                categoria.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(categoria);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrados.");
            }
            return categoriaDTO;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreum um problema ao tratar sua solicitação.");
        }    
               
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        try
        {
            var categoria = _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            if (categoriaDTO is null)
            {
                return NotFound("Categoria não encontrado.");
            }
            return categoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreum um problema ao tratar sua solicitação.");
        }        
    }

    [HttpPost]
    public ActionResult Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            return BadRequest();
        }
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uof.CategoriaRepository.Add(categoria);
        _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaDTO.CategoriaId }, categoriaDTO);
    }

    //Tenho que atualizar todos os dados do produtos.
    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {

        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest("Id's enviados diferentes.");
        }
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();
        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrado.");
        }
        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();
        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDTO);
    }

}
