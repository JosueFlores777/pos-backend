﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Cliente : IEntity
    {
        public int Id { get; set; }
        public int TipoIdentificadorId { get; set; }
        public Catalogo TipoIdentificador { get; set; }
        public string Identificador { get; set; }
        public int TipoPersonaId { get; set; }
        public Catalogo TipoPersona { get; set; }
        public string Nombre { get; set; }
        public int NacionalidadId { get; set; }
        public Catalogo Nacionalidad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public int DepartamentoId { get; set; }
        public Catalogo Departamento { get; set; }
        public int MunicipioId { get; set; }
        public Catalogo Municipio { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int MarcaId { get; set; }
        public Catalogo Marca { get; set; }
        public int ModeloId { get; set; }
        public Catalogo Modelo { get; set; }
        public string TipoIngreso { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool CorreoEnviado { get; set; }
        public DateTime? FechaEnvioCorreo { get; set; }
        public bool CorreoVerificado { get; set; }
        public DateTime? FechaVerificacionCorreo { get; set; }
        public bool AccesoAprobado { get; set; }
        public DateTime? FechaAprobacionAcceso { get; set; }
        public int? UsuarioGentionId { get; set; }
        public Usuario UsuarioGention { get; set; }
        public string TokenVerificacion { get; set; }
        public string MotivoRechazo { get; set; }

    }
}
