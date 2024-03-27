using Aplicacion.Commands.Servicio;
using Aplicacion.Dtos.Servicio;
using Aplicacion.Dtos;
using Ardalis.Specification;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Repositories;
using Dominio.Repositories.Extensiones;
namespace Aplicacion.CommandHandlers.Producto
{
    public class ConsultarServicioHandler : AbstractHandler<ConsultarServicio>
    {
        private readonly IServicioRepository servcioRepository;
        private readonly IMapper mapper;
 
        public ConsultarServicioHandler(IServicioRepository servcioRepository, IMapper mapper) {
            this.servcioRepository = servcioRepository;
            this.mapper = mapper;
   
        }
        public override IResponse Handle(ConsultarServicio message)
        {
            var respuesta = servcioRepository
                .Specify(new BuscarServicioPorTag(message.Tag))
                .Specify(new BuscarServicioPorCategoriaId(message.CategoriaId))
                .Specify(new BuscarServicioPorArea(message.AreaId))
                .Specify(new BuscarServicioPorCodigo(message.Codigo))
                .Specify(new BuscarServicioPorDepartamento(message.DepartamentoId))
                .Specify(new BuscarServicioPorVerificado(message.Verificado))
                .Paginar(message);

            return mapper.Map<DtoServicioPaginado>(respuesta);
        }

    }
}
