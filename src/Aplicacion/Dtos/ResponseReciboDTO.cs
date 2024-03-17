using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Models;

namespace Aplicacion.Dtos
{
    public class ResponseReciboDTO : IResponse
    {
        public ResponseReciboDTO() {
            Success = false;
            Message ="";
        }
        public List<ReciboResponse> Entity { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ResponseReciboPostDTO : IResponse
    {
        public ResponseReciboPostDTO()
        {
            Entity = false;
            Success = false;
            Message = "";
        }
        public bool Entity { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ResponseReciboGenerarDTO : IResponse
    {
        public ResponseReciboGenerarDTO()
        {
            Success = false;
            Message = "";
        }
        public List<SefinRecibo> Entity { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
