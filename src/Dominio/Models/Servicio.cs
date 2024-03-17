
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Servicio : IEntity
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreServicio { get; set; }
        public string NombreSubServicio { get; set; }
        public Catalogo Categoria { get; set; }
        public int CategoriaId { get; set; }
        public Catalogo TipoServicio { get; set; }
        public int TipoServicioId { get; set; }
        public int AreaId { get; set; }
        public Catalogo Area { get; set; }
        public int TipoCobroId { get; set; }
        public Catalogo TipoCobro { get; set; }
        public int DepartamentoId { get; set; }
        public Catalogo Departamento { get; set; }
        public int MonedaId { get; set; }
        public Catalogo Moneda { get; set; }
        public double? Monto { get; set; }
        public bool Activo { get; set; }
        public bool Verificado { get; set; }
        public bool AdicionarMismoServicio { get; set; }
        public int Rubro { get; set; }
        public string Descripcion { get; set; }
        public string Tag { get; set; }
        public List<RangoCobros> RangoCobros { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Descuento { get; set; }
        public int? TipoCobroUnidadesId { get; set; }
        

        public void Inicializar()
        {
            
            FechaRegistro = DateTime.Now;
            Tag = NombreServicio + " / " + NombreSubServicio + " / " + Descripcion;
        }

    }
    
}
