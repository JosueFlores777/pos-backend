using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Dtos.Servicio;
namespace Aplicacion.Dtos
{
    public class DtoRecibo : IResponse
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string NombreRazon { get; set; }
        public double MontoTotal { get; set; }
        public List<DtoDetalleRecibo> DetalleRecibos { get; set; }
        public TipoIdentificadorDto TipoIdentificador { get; set; }
        public int TipoIdentificadorId { get; set; }

        //
        public int MonedaId { get; set; }
        public MonedaDto Moneda { get; set; }

        public EstadoSenadaDto EstadoSenasa { get; set; }
        public int EstadoSenasaId { get; set; }
        public EstadoSefinDto EstadoSefin { get; set; }
        public int EstadoSefinId { get; set; }
        public string Comentario { get; set; }
        public RegionalDto Regional { get; set; }
        public int? RegionalId { get; set; }

        public DescuentoDto Descuento { get; set; }
        public int? DescuentoId { get; set; }
        public int AreaId { get; set; }
        public ImportadorDto Importador { get; set; }
        public int ImportadorId { get; set; }

        public bool RegionalBool { get; set; }

        public int? UsuarioAsignadoId { get; set; }
        public DtoUsuario UsuarioAsignado { get; set; }
        //
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaUtilizado { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FinVigencia { get; set; }

    }
}
