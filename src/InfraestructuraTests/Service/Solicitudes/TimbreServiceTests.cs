using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestructura.Service.Solicitudes;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Dominio.Service;
using Moq;

namespace Infraestructura.Service.Solicitudes.Tests
{
    [TestClass()]
    public class TimbreServiceTests
    {
     

        [TestMethod()]
        public void TraerColProcahCuaretenaVegetalTimbreTest()
        {
            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(c => c.GetIdentificacionUsuario()).Returns("08011974089013");
            var configuracion = new Config();
            var timbreService = new TimbreColProcah(configuracion, tokenService.Object);
            var resultado = timbreService.TraerTimbre("numero de solictud", "VNM",1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(resultado));
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

        public Seccion(string llave)
        {
            this.llave = llave;
        }
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key => throw new NotImplementedException();

        public string Path => "";

        public string Value { get => this.getvalor(); set => throw new NotImplementedException(); }

        private string getvalor()
        {
            if (this.llave == "AppSettings:TimbreConfiguracion:Url")
            {
                return "https://timbres.colprocah.com/api/v1/timbres/";
            }
            else if (llave == "AppSettings:TimbreConfiguracion:Token")
            {
                return "cc_test_GonP6C1avDKkyug4gmideYNk";
            }
            else if (llave == "AppSettings:TimbreConfiguracion:Tipo_Solicitud")
            {
                return "IMPORTACION";
            }
            else
            {
                return "HND";
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