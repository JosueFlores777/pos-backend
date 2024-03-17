using Aplicacion.Commands;
using Aplicacion.Dtos;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CommandHandlers
{
    public  class DescargarReciboPDFHandler :  AbstractHandler<DescargarReciboPDF>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IPdfHelper pdfhelper;

        public DescargarReciboPDFHandler(IReciboRepository reciboRepository, IPdfHelper pdfhelper) {
            this.reciboRepository = reciboRepository;
            this.pdfhelper = pdfhelper;
        }
        public override IResponse Handle(DescargarReciboPDF message)
        {
            var recibo = reciboRepository.ReciboConDetalle(message.nroRecibo);
            var stream = pdfhelper.Traerpermiso(recibo);
            return new DescargaArchivoDto { File=stream, FileName="Permiso.pdf"};
        }


    }
}
