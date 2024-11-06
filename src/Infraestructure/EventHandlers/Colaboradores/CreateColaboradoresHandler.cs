using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Handlers
{
    public class ColaboradoresCreateCommandHandler : IRequestHandler<ColaboradoresCreateCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;

        public ColaboradoresCreateCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(ColaboradoresCreateCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // Crear entidad Colaborador
                var colaborador = new Colaboradores
                {
                    Nombre = request.Nombre,
                    Edad = request.Edad,
                    Birthdate = request.Birthdate,
                    IsProfessor = request.IsProfessor, // Asignación directa de int
                    FechaCreacion = request.FechaCreacion
                };

                _context.Colaboradores.Add(colaborador);
                await _context.SaveChangesAsync(cancellationToken);

                Console.WriteLine($"Colaborador creado con ID: {colaborador.Id}");

                // Crear registro en Profesor o Administrativo según el valor de IsProfessor
                if (request.IsProfessor == 1)
                {
                    Console.WriteLine("Insertando en Profesor");
                    var profesor = new Profesor
                    {
                        FKColaborador = colaborador.Id,
                        Correo = request.Correo,
                        Departamento = request.Departamento
                    };

                    _context.Profesor.Add(profesor);
                }
                else
                {
                    Console.WriteLine("Insertando en Administrativo");
                    var administrativo = new Administrativo
                    {
                        FKColaborador = colaborador.Id,
                        Correo = request.Correo,
                        Puesto = request.Puesto,
                        Nomina = request.Nomina
                    };

                    _context.Administrativo.Add(administrativo);
                }

                // Guardar cambios en la base de datos para Profesor o Administrativo
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Response<int>(colaborador.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                // Log detallado de la excepción
                var errorMessage = new StringBuilder();
                errorMessage.AppendLine($"Error: {ex.Message}");

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    errorMessage.AppendLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                Console.WriteLine(errorMessage.ToString());

                return new Response<int>($"An error occurred: {errorMessage.ToString()}");
            }
        }
    }
}
