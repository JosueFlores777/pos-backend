using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models
{
    public class Recibo : IEntity
    {
        public static int EstadoReciboCreado = 6; //CREADO
        public static int EstadoReciboPagado = 7; //PAGADO
        public static int EstadoReciboProcesado = 8; //PROCESADO
        public static int EstadoReciboUtilizado = 9; //UTILIZADO
        public static int EstadoReciboSolicitado = 10; //UTILIZADO
        public static int EstadoReciboEliminado = 11; //ELIMINADO
        public static int IdentificadorRTN = 5; //RTN

        public int Id { get; set; }
        public string Identificacion { get; set; }
        public Importador Importador { get; set; }
        public int? ImportadorId { get; set; }
        public string NombreRazon { get; set; }
        public int TipoIdentificadorId { get; set; }
        public Catalogo TipoIdentificador { get; set; }
        public double MontoTotal { get; set; }
        public int MonedaId { get; set; }
        public Catalogo Moneda { get; set; }
        public Catalogo EstadoSenasa { get; set; }
        public int EstadoSenasaId { get; set; }
        public Catalogo EstadoSefin { get; set; }
        public int EstadoSefinId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public string Comentario { get; set; }
        public Catalogo Regional { get; set; }
        public int? RegionalId { get; set; }
        public Catalogo Descuento { get; set; }
        public int? DescuentoId { get; set; }
        public Catalogo Area { get; set; }
        public int AreaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime LastSync { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaUtilizado{ get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FinVigencia { get; set; }
        public string Banco { get; set; }


        public ICollection<CambioEstado> Cambios { get; set; }
        //DetalleRecibo
        public List<DetalleRecibo> DetalleRecibos { get; set; }
        public void Inicializar()
        {
            LastSync = DateTime.Now;
            this.FechaCreacion = DateTime.Now;
            this.Cambios = new List<CambioEstado> {
                new CambioEstado {
                    EstadoId=EstadoReciboCreado,
                    Fecha=this.FechaCreacion,
                    ReciboId=this.Id,
                    UsuarioId = 5,
                } 
            };
            this.EstadoSenasaId = EstadoReciboCreado;
            this.EstadoSefinId = EstadoReciboCreado;
        }

        private void EstablecerVigencia() {
            FinVigencia = DateTime.Now.AddDays(29);
            InicioVigencia = DateTime.Now;
        }
        public void ProcesarRecibo(string comentario, int idUsuario)
        {
            FechaModificacion = DateTime.Now;
            FechaUtilizado = DateTime.Now;
            Comentario = comentario;
            UsuarioAsignadoId = idUsuario;
            EstadoSefinId = EstadoReciboProcesado;
            EstadoSenasaId = EstadoReciboUtilizado;
        }
        public void ProcesarReciboWS(string comentario, int regional, int idUsuario)
        {
            FechaModificacion = DateTime.Now;
            FechaUtilizado = DateTime.Now;
            Comentario = comentario;
            RegionalId = regional;
            UsuarioAsignadoId = idUsuario;
            EstadoSefinId = EstadoReciboProcesado;
            EstadoSenasaId = EstadoReciboUtilizado;
        }
        public void ProcesarReciboTemporal(string comentario, int? regional, int idUsuario,DateTime date)
        {
            FechaPago = date;
            FechaModificacion = date;
            FechaUtilizado = date;
            Comentario = comentario;
            RegionalId = regional;
            UsuarioAsignadoId = idUsuario;
            EstadoSefinId = EstadoReciboProcesado;
            EstadoSenasaId = EstadoReciboUtilizado;
        }
        public void PagarRecibo(DateTime date,string banco)
        {
            var mes = date.Month;
            var anio = date.Year;
            var fecha1 = new DateTime(anio,mes,1);
            var fecha2 = fecha1.AddMonths(1).AddDays(-1);
            Banco = banco;
            FechaModificacion = date;
            FechaPago = date;
            EstadoSefinId = EstadoReciboPagado;
            EstadoSenasaId = EstadoReciboCreado;
            InicioVigencia = date;
            FinVigencia = fecha2.AddMonths(1);


        }
        public void EliminarRecibo(DateTime date)
        {

            FechaModificacion = date;
            EstadoSenasaId = EstadoReciboEliminado;

        }
    }
}
