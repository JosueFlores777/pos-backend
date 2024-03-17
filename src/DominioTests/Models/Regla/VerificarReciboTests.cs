using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Models.Regla;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Dominio.Repositories;
using Dominio.Especificaciones;
using Dominio.Service;
using Dominio.Service.Solicitudes;
using Infraestructura.Data;
using Dominio.Models.Catalogos;

namespace Dominio.Models.Regla.Tests
{
    [TestClass()]
    public class VerificarReciboTests
    {
        [TestMethod()]
        public void Recibo_yafueUtilizadoEnOtraSolicitud_retornaFalse()
        {
            var configuracion = new Config();
            var mockRepo = new Mock<ISolicitudRepository>();
            var mockTarifa = new Mock<ITarifaContext>();
            var mkRecibos =new Mock<IPermisoRevalidacionRepository>();
            var context = new Mock<SolicitudesContext>();

         
            mockRepo.Setup(c => c.Filter(It.IsAny<ISpecification<Solicitud>>() )).Returns(new List<Solicitud> { new Solicitud { MedioId=1, Productos=new List<SolicitudProducto>()} });
            var repo = new ReciboRepository(context.Object, configuracion);
            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(c => c.GetIdentificacionUsuario()).Returns("4545454");
            var instancia = new VerificarRecibo(repo, mockRepo.Object, mockTarifa.Object, tokenService.Object, mkRecibos.Object);
            var resultado = instancia.Verificar(7387772, 4554, 1, "4545454", Dependencia.idCuarentenaVegetal);
            Assert.IsFalse(resultado);
        }
        [TestMethod()]
        public void Recibo_DiferenteImpotador_retornaFalse()
        {
            var configuracion = new Config();
            var mockRepo = new Mock<ISolicitudRepository>();
            var mockTarifa = new Mock<ITarifaContext>();
            var context = new Mock<SolicitudesContext>();

            mockRepo.Setup(c => c.Filter(It.IsAny<ISpecification<Solicitud>>())).Returns(new List<Solicitud>());
            var repo = new ReciboRepository(context.Object,configuracion);
            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(c => c.GetIdentificacionUsuario()).Returns("4545454");
            var mkRecibos = new Mock<IPermisoRevalidacionRepository>();
            var instancia = new VerificarRecibo(repo, mockRepo.Object, mockTarifa.Object, tokenService.Object, mkRecibos.Object);
            var resultado = instancia.Verificar(7387772, 500, 1, "4545454", Dependencia.idCuarentenaVegetal);
            Assert.IsFalse(resultado);
        }
        [TestMethod()]
        public void Recibo_Mismoimportadore_retornaTrue()
        {
            var configuracion = new Config();
            var mockRepo = new Mock<ISolicitudRepository>();
            var mockTarifa = new Mock<ITarifaContext>();
            var context = new Mock<SolicitudesContext>();
          
            mockRepo.Setup(c => c.Filter(It.IsAny<ISpecification<Solicitud>>())).Returns(new List<Solicitud>());
            var repo = new ReciboRepository(context.Object,configuracion); var tokenService = new Mock<ITokenService>();
            tokenService.Setup(c => c.GetIdentificacionUsuario()).Returns("4545454");
            var mkRecibos = new Mock<IPermisoRevalidacionRepository>();
            var instancia = new VerificarRecibo(repo, mockRepo.Object, mockTarifa.Object, tokenService.Object, mkRecibos.Object);
            var resultado = instancia.Verificar(7387772,1,454, "0404-1958-19580-0    ", Dependencia.idCuarentenaVegetal);
            Assert.IsTrue(resultado);
           
        }
    }
    public class Config : IConfiguration
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return new Seccion(key);
        }
    }

    public class Seccion : IConfigurationSection
    {
        private readonly string llave;

        public Seccion(string llave) {
            this.llave = llave;
        }
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key => throw new NotImplementedException();

        public string Path => "";

        public string Value { get => this.getvalor(); set => throw new NotImplementedException(); }

        private string getvalor() {
            if (this.llave == "AppSettings:ReciboConfiguracion:Token:Usuario")
            {
                return "fabryalon";
            }
            else if (llave == "AppSettings:ReciboConfiguracion:Token:Password")
            {
                return "fabry2823";
            }
            else if (llave == "AppSettings:ReciboConfiguracion:Token:Grant_type")
            {
                return "password";
            }
            else if (llave == "AppSettings:ReciboConfiguracion:Token:Url")
            {
                return "http://216.10.247.163/SPO.Api/token";
            }
            else {
                return "http://216.10.247.163/SPO.Api/recibo/consultar/";
            }
        
        }
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}