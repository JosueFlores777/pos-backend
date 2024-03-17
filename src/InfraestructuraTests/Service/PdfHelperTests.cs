using Dominio.Models;
using Dominio.Models.Catalogos;
using Dominio.Repositories;
using Infraestructura.Service.Permisos;
using Infraestructura.Service.Requisitos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Dominio.Models.ProductoModels;
using Dominio.Service;

namespace Infraestructura.Service.Tests
{
    [TestClass()]
    public class PdfHelperTests
    {
        [TestMethod()]
        public void CrearPermisoTest()
        {
            var mockRepo = new Mock<IFirmaHelper>();
            var requisitos = new Mock<IRequisitosImportacion>();
            var mockPaisProducto = new Mock<IProductosPaisRepository>();
            var configuracion = new Config();
            mockPaisProducto.Setup(c => c.TraerRequisitos(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<ProductoPaisRequisito> { new ProductoPaisRequisito { Nombre = "Requisito A" }, new ProductoPaisRequisito { Nombre = "Requisito B" } });
        

            var instancia = new PdfHelper(new List<IPermisoSanitario> { new CuarentenaVegetal(new QrHelper(), configuracion) }, mockRepo.Object, mockPaisProducto.Object, requisitos.Object);
            var resultado = instancia.Traerpermiso(TraerSolicitud());
            Assert.IsNotNull(resultado);
        }

        [TestMethod()]
        public void CrearPermisoCrearArchivoTest()
        {
            var mockRepo = new Mock<IFirmaHelper>();
            var mockPaisProducto = new Mock<IProductosPaisRepository>();
            var configuracion = new Config();
            var requisitos = new Mock<IRequisitosImportacion>();
            mockPaisProducto.Setup(c => c.TraerRequisitos(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<ProductoPaisRequisito> { new ProductoPaisRequisito { Nombre = "Requisito A" }, new ProductoPaisRequisito { Nombre = "Requisito B" } });
         

            var instancia = new PdfHelper(new List<IPermisoSanitario> { new CuarentenaVegetal(new QrHelper(), configuracion) }, mockRepo.Object, mockPaisProducto.Object, requisitos.Object);
            var resultado = instancia.Traerpermiso(TraerSolicitud());
            using (var fileStream = File.Create("prueba.pdf"))
            {
                resultado.Seek(0, SeekOrigin.Begin);
                resultado.CopyTo(fileStream);
            }
            Assert.IsTrue(File.Exists("prueba.pdf"));
        }

        private Solicitud TraerSolicitud()
        {
            var sol = new Solicitud
            {
                DependenciaId = Dependencia.idCuarentenaVegetal,
                Dependencia = new Dependencia { Nombre = "Sub Dirección tecnica de cuarentena Vegetal" },
                ReciboDePago = 1234,
                NumeroPermiso = "PC-001-00001",
                Importador = new Importador { Correo = "prueba2@gmail.com", Nombre = "Dinant SA", Identificador = "08489845498", TipoIdentificador = new TipoIdentificacion { Nombre = "RTN" } },
                Productos = new List<SolicitudProducto> { new SolicitudProducto { Producto =
                new Producto { NombreComun = "Cebolla", NombreCientifico = new NombreCientifico {Nombre="Cebolla" } ,
                    Paises = new List<ProductoPais> { new ProductoPais { Requisitos = new List<ProductoPaisRequisito> { new ProductoPaisRequisito {
                        Nombre = "Requisito A",
                    } } } }

                },
                    Peso = 5454, ValorFob = 6565 } },
                PaisOrigen = new Pais { Nombre = "Estados unidos de america" },
                PaisProcedencia = new Pais { Nombre = "Honduras" },
                Aduana=new Aduana { Nombre="prueba", Id=1},
                FechaCreacion = DateTime.Now,
                InicioVigencia = DateTime.Now,
                FinVigencia = DateTime.Now,
                UsuarioApruebaId=2,


            };
            return sol;
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
                return new Seccion();
            }
        }

        public class Seccion : IConfigurationSection
        {
            public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public string Key => throw new NotImplementedException();

            public string Path => "";

            public string Value { get =>  "http://52.11.191.48/#/cuarentena-vegetal-solicitudes-pendientes/ver/"; set => throw new NotImplementedException(); }

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
}