# API Festivos - Pruebas Unitarias con xUnit

## Descripción
Proyecto de API de días festivos desarrollado en C# con .NET 8, siguiendo una arquitectura por capas. Implementado como parte de la evaluación parcial en la asignatura de Administración de Sistemas de Información.

## Integrantes del Equipo
- Luis Guillermo González
- Omar Yesid Fonseca Montes
- Jhoan Sebastián Gómez Valderrama
- Juan David Machado Mosquera

## Detalles del Proyecto
- **Universidad:** Instituto Tecnológico Metropolitano (ITM)
- **Asignatura:** Administración de Sistemas de Información
- **Profesor:** Fray Leon Osorio
- **Tipo de Evaluación:** Parcial

## Estructura del Proyecto
```
ITM_AS_apiFestivos/
├── .vs/
├── apiFestivos.Core/
├── apiFestivos.Dominio/
├── apiFestivos.Infraestructura.Repositorio/
├── apiFestivos.Presentacion/
├── apiFestivos.Test/
├── .gitignore
└── apiFestivos.sln
```

## Capas del Proyecto
- **apiFestivos.Core:** Contiene las entidades base y contratos del sistema
- **apiFestivos.Dominio:** Implementación de la lógica de negocio
- **apiFestivos.Infraestructura.Repositorio:** Capa de acceso a datos
- **apiFestivos.Presentacion:** Capa de presentación y API
- **apiFestivos.Test:** Pruebas unitarias del proyecto

## Puntos Evaluados

### 1. Pruebas Unitarias para el método EsFestivo()
- Test para verificar que una fecha festiva retorna true (Caso Positivo)
- Test para verificar que una fecha no festiva retorna false (Caso Negativo)

### 2. Pruebas Unitarias para el método ObtenerFestivo()
- Test para verificar festivos de tipo 1 (fecha fija)
- Test para verificar festivos de tipo 2 (festivos que se trasladan al siguiente lunes)
- Test para validación de cálculos de fechas festivas

## Tecnologías Utilizadas
- **.NET 8**
- **C#**
- **xUnit** para pruebas unitarias
- **Moq** para mocking en pruebas

## Comandos Principales
```bash
# Restaurar dependencias
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar pruebas
dotnet test apiFestivos.Test/apiFestivos.Test.csproj

# Ejecutar la aplicación
dotnet run --project apiFestivos.Presentacion/apiFestivos.Presentacion.csproj
```

## Pruebas Unitarias
El proyecto utiliza xUnit como framework de pruebas, implementando:
- Pruebas unitarias para validación de fechas festivas
- Mocking de dependencias
- Casos de prueba positivos y negativos
- Pruebas para cálculos de fechas móviles

## Contribuciones
Este proyecto fue desarrollado como parte de una evaluación académica por los estudiantes mencionados anteriormente.
