using Xunit;
using Moq;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Dominio.Entidades;
using apiFestivos.Dominio.DTOs;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class FestivoServicioTest
{
    private readonly Mock<IFestivoRepositorio> _repositorioMock;
    private readonly IFestivoServicio _servicio;

    public FestivoServicioTest()
    {
        _repositorioMock = new Mock<IFestivoRepositorio>();
        _servicio = new FestivoServicio(_repositorioMock.Object);
    }

    // 1. Pruebas para el método EsFestivo()
    [Fact]
    public async Task EsFestivo_FechaFestivaFija_DebeRetornarVerdadero()
    {
        // Organizar
        var fechaPrueba = new DateTime(2024, 12, 25);
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1, // Festivo tipo fijo
                DiasPascua = 0
            }
        };

        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Actuar
        var resultado = await _servicio.EsFestivo(fechaPrueba);

        // Afirmar
        Assert.True(resultado);
    }

    [Fact]
    public async Task EsFestivo_FechaNoFestiva_DebeRetornarFalso()
    {
        // Organizar
        var fechaPrueba = new DateTime(2024, 12, 26);
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1,
                DiasPascua = 0
            }
        };

        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Actuar
        var resultado = await _servicio.EsFestivo(fechaPrueba);

        // Afirmar
        Assert.False(resultado);
    }

    [Fact]
    public async Task EsFestivo_FestivoPorTipoFijo_DebeRetornarVerdadero()
    {
        // Organizar
        var fechaPrueba = new DateTime(2024, 12, 25);
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1, // Festivo tipo fijo
                DiasPascua = 0
            }
        };

        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Actuar
        var resultado = await _servicio.EsFestivo(fechaPrueba);

        // Afirmar
        Assert.True(resultado);
    }

    [Fact]
    public async Task EsFestivo_FestivoPorTipoTrasladable_DebeRetornarVerdadero()
    {
        // Organizar
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Todos los Santos",
                Dia = 1,
                Mes = 11,
                IdTipo = 2, // Festivo trasladable
                DiasPascua = 0
            }
        };

        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // El 1 de noviembre de 2024 cae viernes, se traslada al lunes 4
        var fechaLunes = new DateTime(2024, 11, 4);

        // Actuar
        var resultado = await _servicio.EsFestivo(fechaLunes);

        // Afirmar
        Assert.True(resultado);
    }

    // [Fact]
    // public async Task EsFestivo_FestivoPorTipoSemanaSanta_DebeRetornarVerdadero()
    // {
    //     // Organizar
    //     var festivosDB = new List<Festivo>
    //     {
    //         new Festivo
    //         {
    //             Id = 3,
    //             Nombre = "Ascensión del Señor",
    //             Dia = 0,
    //             Mes = 0,
    //             IdTipo = 4, // Festivo relativo a Semana Santa
    //             DiasPascua = 43 // 43 días después del domingo de Pascua
    //         }
    //     };

    //     _repositorioMock.Setup(r => r.ObtenerTodos())
    //         .ReturnsAsync(festivosDB);

    //     // En 2024, el domingo de Pascua es el 31 de marzo
    //     // 43 días después es el 13 de mayo, que cae en lunes
    //     var fechaAscension = new DateTime(2024, 5, 13);

    //     // Actuar
    //     var resultado = await _servicio.EsFestivo(fechaAscension);

    //     // Afirmar
    //     Assert.True(resultado);
    // }

    [Fact]
    public async Task EsFestivo_FechaOriginalTrasladada_DebeRetornarFalso()
    {
        // Organizar
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Todos los Santos",
                Dia = 1,
                Mes = 11,
                IdTipo = 2, // Festivo trasladable
                DiasPascua = 0
            }
        };

        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Verificamos que la fecha original (1 de noviembre) NO es festiva
        var fechaOriginal = new DateTime(2024, 11, 1);

        // Actuar
        var resultado = await _servicio.EsFestivo(fechaOriginal);

        // Afirmar
        Assert.False(resultado);
    }
}