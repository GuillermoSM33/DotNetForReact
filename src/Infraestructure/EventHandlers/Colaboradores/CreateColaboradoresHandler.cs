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
                var colaborador = new Colaboradores
                {
                    Nombre = request.Nombre,
                    Edad = request.Edad,
                    Birthdate = request.Birthdate,
                    IsProfessor = request.IsProfessor, 
                    FechaCreacion = request.FechaCreacion
                };

                _context.Colaboradores.Add(colaborador);
                await _context.SaveChangesAsync(cancellationToken);

                Console.WriteLine($"Colaborador creado con ID: {colaborador.Id}");

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

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Response<int>(colaborador.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

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
