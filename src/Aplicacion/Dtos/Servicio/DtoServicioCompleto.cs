using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos.Servicio
{
    public class DtoServicioCompleto : IResponse
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool Verificado { get; set; }
        public string NombreServicio { get; set; }
        public string NombreSubServicio { get; set; }
        public string AliasServicio { get; set; }
        public DtoCategoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public DtoTipoServicio TipoServicio { get; set; }
        public int TipoServicioId { get; set; }
        public int AreaId { get; set; }
        public DtoArea Area { get; set; }
        public int TipoCobroId { get; set; }
        public DtoTipoCobro TipoCobro { get; set; }
        public int MonedaId { get; set; }
        public bool AdicionarMismoServicio { get; set; }
        public DtoTipoCobro Moneda { get; set; }
        public double? Monto { get; set; }
        public int Rubro { get; set; }
        public string Descripcion { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Tag { get; set; }
        public bool Activo { get; set; }

        public int TipoCobroUnidadesId { get; set; }
        public int DepartamentoId { get; set; }
        public DtoDepartemento Departamento { get; set; }
        public bool MetodoVerificacion { get; set; }
        public bool Descuento { get; set; }
    }
    public class DtoSubCategoria : CodigoValorDto
    {
    }
    public class DtoDepartemento : CodigoValorDto
    {
    }
    public class DtoTipoCobro : CodigoValorDto
    {
    }
    public class DtoArea : CodigoValorDto
    {
    }
    public class DtoTipoServicio : CodigoValorDto
    {
    }
    public class DtoCategoria : CodigoValorDto
    {
    }
    public class DtoMoneda : CodigoValorDto
    {
    }
}
