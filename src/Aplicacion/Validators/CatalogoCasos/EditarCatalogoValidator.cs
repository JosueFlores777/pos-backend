
using Aplicacion.Commands.CatalogoCasos;
using Aplicacion.Services.Validaciones;
using Dominio.Especificaciones;
using Dominio.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.Validators.CatalogoCasos
{
    class EditarCatalogoValidator : Validador<EditarCatalogo>
    {
        private readonly ICatalogoRepository catalogoRepository;

        public EditarCatalogoValidator(IAutenticationHelper autenticationHelper, ICatalogoRepository catalogoRepository) : base(autenticationHelper)
        {
            RuleFor(x => x.Catalogo).NotNull().Must(c=> PuedeusarNombre(c.Nombre, c.Tipo, c.Id)).WithMessage("No puedes utilizar el nombre por que ya existe un catalogo");
            RuleFor(x => x.Catalogo.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.Catalogo.Id).NotNull().NotEmpty().GreaterThan(0).WithMessage("Id es requerido");
            RuleFor(x => x.Catalogo.Tipo).NotNull().NotEmpty().Must(c=>ExisteTipo(c)).WithMessage("El tipo de catalogo no existe");
            RuleFor(x => x.Catalogo.IdPadre).Must(c=>ExistePadre(c)).WithMessage("El padre no existe");
            this.catalogoRepository = catalogoRepository;
        }

        private bool ExisteTipo(string tipo) {
            var tipos = catalogoRepository.Specify(new BuscarCatalogoPorTipo(tipo)).WithSpecs().ToList();
            return tipos.Count() > 0;
        }
        private bool ExistePadre(int? idpadre)
        {
            if (idpadre == null) return true;
            var padre = catalogoRepository.GetById(idpadre.GetValueOrDefault());
            return padre!=null;
        }
        private bool PuedeusarNombre(string nombre, string tipo, int Id) {
            var catalogo = catalogoRepository.Filter(new Func<Dominio.Models.Catalogo, bool>(c=>c.Nombre.ToUpper().Trim().Equals(nombre.ToUpper().Trim()) && c.Tipo.Equals(tipo))).FirstOrDefault();
            if (catalogo == null) return true;
            return catalogo.Id == Id;
                 
        }

        public override IList<string> Permisos => new List<string> { "catalogo-crear" };
    }
}
