using APICatalogo.Context;
using APICatalogo.Controllers;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Pagination;
using APICatalogo.Repository.UofW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.ComponentModel;

namespace APICatalogoxUnitTests
{
    public class CategoriasUnitTestController
    {
        private readonly IUnitOfWork _uof;
  
        private readonly IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        private static string connectionString = "Server=localhost;DataBase=CatalogoDB;Uid=root;Pwd=root";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            
        }
        public CategoriasUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
  
            });
            _mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);

            _uof = new UnitOfWork(context);
        }

        [Fact]
        public void GetCategorias_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(_uof,_mapper);
            var parameters = new CategoriasParameters { PageNumber = 1, PageSize = 10 };

            //Act
            var data = controller.Get(parameters);

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Task<ActionResult<IEnumerable<CategoriaDTO>>>>(data);

        }

        // O segundo teste apresentado no curso foi um badrequest, mas acheio meio gambiarra( tbm não temos mock), por isso pulei ele.

        [Fact]
        public void GetCategorias_MatchResult()
        {
            //Arrange
            var controller = new CategoriasController(_uof, _mapper);
            var parameters = new CategoriasParameters { PageNumber = 1, PageSize = 10 };

            //Act
            var data = controller.Get(parameters);

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Task<ActionResult<IEnumerable<CategoriaDTO>>>>(data);

            var cat = data.Should().BeAssignableTo<Task<ActionResult<IEnumerable<CategoriaDTO>>>>().Subject;
            var lista = cat.Result.Value.ToList();

            Assert.Equal("Bebida", lista[0].Nome );
            Assert.Equal("bebidas.jpg", lista[0].ImagemURL);
        }

        [Fact]
        public void GetCategoriaById_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(_uof, _mapper);
            var parameters = new CategoriasParameters { PageNumber = 1, PageSize = 10 };
            var catId = 2;

            //Act
            var data = controller.Get(catId);

            //Assert
            Assert.NotNull(data);
            Assert.IsType<ActionResult<CategoriaDTO>>(data.Result);

        }
    }
}