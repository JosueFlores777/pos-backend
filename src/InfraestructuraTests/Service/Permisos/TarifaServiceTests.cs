using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestructura.Service.Permisos;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Repositories;
using Moq;
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Models.Catalogos;

namespace Infraestructura.Service.Permisos.Tests
{
    [TestClass()]
    public class TarifaServiceTests
    {
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_4000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(4000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 300);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_12000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(12000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 350);
        }

        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_23000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(23000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 420);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_55000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(55000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 1140);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_75000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(75000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 1260);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_78000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(78000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 1610);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_82000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasContenerizadas());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(82000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 1190);
        }
        /*************************************Granel**********************************/
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_25000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(25000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 525);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_500000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(500000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 780);
        }

        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_2000000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(2000000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 1750);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_5000000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(5000000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 3450);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_10000000()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(10000000, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 5700);
        }
        [TestMethod()]
        public void CarculaTarifa_CargaContenorizada_peso_21100007()
        {
            var mockRepo = new Mock<ITarifaRepository>();
            mockRepo.Setup(p => p.Filter(It.IsAny<ISpecification<Tarifa>>())).Returns(tarifasGranel());
            var tarifas = new TarifaCuarentenaVegetalService(mockRepo.Object);
            var monto = tarifas.CalcularTarifaSanidadVegetal(21100007, MedioTarifa.IdCargaConterenizada);
            Assert.AreEqual(monto, 39000.02);
        }
        private List<Tarifa> tarifasContenerizadas()
        {
            return new List<Tarifa> { new Tarifa { Monto = 300, PesoDesde = 1, PesoHasta = 5000 }, new Tarifa { Monto = 350, PesoDesde = 5001, PesoHasta = 15000 }, new Tarifa { Monto = 420, PesoDesde = 15001, PesoHasta = 25000 } };
        }
        private List<Tarifa> tarifasGranel()
        {
            return new List<Tarifa> { new Tarifa { Monto = 525, PesoDesde = 1, PesoHasta = 25000 }, new Tarifa { Monto = 780, PesoDesde = 25001, PesoHasta = 500000 }
            , new Tarifa { Monto = 1750, PesoDesde = 500001, PesoHasta = 2000000 }, new Tarifa { Monto = 3450
, PesoDesde = 2000001, PesoHasta = 5000000 },  new Tarifa { Monto = 5700
, PesoDesde = 5000000, PesoHasta = 10000000 } };
        }
    }

  
}