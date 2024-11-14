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

    // 2.1 "Verificar que, al dar un festivo con tipo 1, se retorne la fecha esperada"
[Fact]
public async Task ObtenerAño_FestivoTipoFijo_DebeRetornarFechaCorrecta()
{
    // Organizar
    var año = 2024;
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
    var resultado = await _servicio.ObtenerAño(año);

    // Afirmar
    var festivo = resultado.First();
    Assert.Equal(new DateTime(2024, 12, 25), festivo.Fecha);
    Assert.Equal("Navidad", festivo.Nombre);
}

    // 2.2 "Probar que un festivo movible (tipo 2) caiga en el lunes siguiente a la fecha inicial"
[Fact]
public async Task ObtenerAño_FestivoTipoTrasladable_DebeRetornarLunesSiguiente()
{
    // Organizar
    var año = 2024;
    var festivosDB = new List<Festivo>
    {
        new Festivo
        {
            Id = 2,
            Nombre = "Todos los Santos",
            Dia = 1,
            Mes = 11,
            IdTipo = 2, // Festivo trasladable
            DiasPascua = 0
        }
    };

    _repositorioMock.Setup(r => r.ObtenerTodos())
        .ReturnsAsync(festivosDB);

    // Actuar
    var resultado = await _servicio.ObtenerAño(año);

    // Afirmar
    var festivo = resultado.First();
    Assert.Equal(new DateTime(2024, 11, 4), festivo.Fecha); // El 1 de noviembre cae viernes, se traslada al lunes 4
    Assert.Equal("Todos los Santos", festivo.Nombre);
}

    //  2.3 "Verificar un festivo que se desplaza a lunes basado en una fecha relativa a Semana Santa (tipo 4)"
[Fact]
public async Task ObtenerAño_FestivoTipoSemanaSantaTrasladable_DebeRetornarLunesSiguiente()
{
    // Organizar
    var año = 2024;
    var festivosDB = new List<Festivo>
    {
        new Festivo
        {
            Id = 4,
            Nombre = "Ascensión del Señor",
            Dia = 0,
            Mes = 0,
            IdTipo = 4, // Festivo Semana Santa trasladable
            DiasPascua = 43
        }
    };

    _repositorioMock.Setup(r => r.ObtenerTodos())
        .ReturnsAsync(festivosDB);

    // Actuar
    var resultado = await _servicio.ObtenerAño(año);

    // Afirmar
    var festivo = resultado.First();
    // En 2024:
    // 1. Pascua: 31 de marzo
    // 2. +43 días = 13 de mayo (que cae en lunes)
    // 3. Como es tipo 4, se aplica el traslado al lunes anterior
    Assert.Equal(new DateTime(2024, 5, 6), festivo.Fecha); 
    Assert.Equal("Ascensión del Señor", festivo.Nombre);
}
}