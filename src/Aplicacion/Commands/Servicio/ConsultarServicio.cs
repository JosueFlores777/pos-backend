using Dominio.Repositories.Extenciones;
using Dominio.Service;

namespace Aplicacion.Commands.Servicio
{
    public class ConsultarServicio : QueryStringParameters, IMessage
    {
        public string NombreServicio { get; set; }
        public string NombreSubServicio { get; set; }
        public string Tag { get; set; }
        public int CategoriaId { get; set; }
        public int AreaId { get; set; }
        public int DepartamentoId { get; set; }
        public string Codigo { get; set; }
        public int Verificado { get; set; }

    }
}
