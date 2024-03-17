using Dominio.Models;
using Dominio.Service;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

using SefinProto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructura.Service.SefinClient
{
    public sealed class RpcClient : ISefinClient
    {
        private readonly IConfiguration configuration;

        private SefinServer.SefinServerClient client;

        public RpcClient(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SefinRecibo GetRecibo(uint tgr)
        {
            var client = GetRpcClient();
            GetReciboResponse response = client.GetRecibo(new GetReciboRequest { Tgr = tgr });
            //GetReciboResponse response = await client.GetReciboAsync(new GetReciboRequest { Tgr = tgr });
            return GetFromProto(response.Recibo);
        }

        public bool ProcessRecibo(uint tgr)
        {
            var client = GetRpcClient();
            ProcessReciboResponse response = client.ProcessRecibo(new ProcessReciboRequest { Tgr = tgr });
            return response.Success;
        }

        public uint InsertRecibo(SefinRecibo recibo)
        {
            ReciboProto payload = GetFromModel(recibo);
            var client = GetRpcClient();
            InsertReciboRequest request = new InsertReciboRequest
            {
                Recibo = payload
            };
            InsertReciboResponse response = client.InsertRecibo(request);
            return response.Tgr;
        }


        private SefinServer.SefinServerClient GetRpcClient()
        {
            if (client == null)
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                string url = configuration.GetValue<string>("SefinRpcClient:URL");
                GrpcChannel channel = GrpcChannel.ForAddress(url);
                client = new SefinServer.SefinServerClient(channel);
            }
            
            return client;
        }

        private SefinRecibo GetFromProto(ReciboProto proto)
        {
            List<SefinRubro> rubros = new List<SefinRubro>();
            foreach (RubroProto rubro in proto.Rubros)
            {
                rubros.Add(GetFromProto(rubro));
            }
            DateTime fechaModificacion = new DateTime();

            if (proto.FechaModificacion != null) {
               
                DateTime fechaMo = new DateTime(proto.FechaModificacion.Year, proto.FechaModificacion.Month, proto.FechaModificacion.Day);
                fechaModificacion = fechaMo;
            }
            

            return new SefinRecibo
            {
                NumeroRecibo = proto.NumeroRecibo,
                TipoIdentificacion = proto.TipoIdentificacion,
                DescripcionIdentificacion = proto.DescripcionIdentificacion,
                NombreRazon = proto.NombreRazon,
                Institucion = proto.Institucion,
                DescripcionInstitucion = proto.DescripcionInstitucion,
                Monto = proto.Monto,
                ApiEstado = proto.ApiEstado,
                UsuarioModificacion=proto.UsuarioModificacion,
                ApiTransacion = proto.ApiTransaccion,
                FechaMod= fechaModificacion,
                Rubros = rubros
            };
        }

        private SefinRubro GetFromProto(RubroProto proto)
        {
            List<SefinArticulo> articulos = new List<SefinArticulo>();
            foreach (ArticuloProto articulo in proto.Articulos)
            {
                articulos.Add(GetFromProto(articulo));
            }
            return new SefinRubro
            {
                Rubro = proto.Rubro,
                Descripcion = proto.Descripcion,
                Articulos = articulos
            };
        }

        private SefinArticulo GetFromProto(ArticuloProto proto)
        {
            return new SefinArticulo
            {
                Articulo = proto.Articulo,
                Descripcion = proto.Descripcion,
                Monto = proto.Monto
            };
        }

        private ReciboProto GetFromModel(SefinRecibo model)
        {
            IList<RubroProto> rubros = model
                .Rubros
                .ConvertAll(GetFromModel);
            ReciboProto recibo = new ReciboProto
            {
                NumeroRecibo = model.NumeroRecibo,
                TipoIdentificacion = model.TipoIdentificacion,
                DescripcionIdentificacion = model.DescripcionIdentificacion,
                NombreRazon = model.NombreRazon,
                Institucion = model.Institucion,
                DescripcionInstitucion = model.DescripcionInstitucion,
                Monto = model.Monto
            };
            recibo.Rubros.AddRange(rubros);

            return recibo;
        }

        private RubroProto GetFromModel(SefinRubro model)
        {
           

            RubroProto rubro = new RubroProto
            {
                Rubro = model.Rubro,
                Descripcion = model.Descripcion,
                Monto = model.Monto
            };
            if (model.Articulos[0] != null) {
                IList<ArticuloProto> articulos = model
                   .Articulos
                   .ConvertAll(GetFromModel);
                rubro.Articulos.AddRange(articulos);
            }
            
            return rubro;
        }

        private ArticuloProto GetFromModel(SefinArticulo model)
        {
            return new ArticuloProto
            {
                Articulo = model.Articulo,
                Descripcion = model.Descripcion,
                Monto = model.Monto
            };
        }
    }
}
