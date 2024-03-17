using System;

namespace Aplicacion.Dtos
{
    public class CodigoValorDto
    {
        public int? Id { get; set; }
        public String Nombre { get; set; }
    }

    public class DepartamentoDto: CodigoValorDto
    { 
    }

    public class MunicipioDto : CodigoValorDto
    {
    }

    public class PaisDto : CodigoValorDto
    {
    }
    public class TipoIdentificadorDto : CodigoValorDto
    {
    }
    public class EstadoSenadaDto : CodigoValorDto
    {
    }
    public class MonedaDto : CodigoValorDto
    {
    }
    public class EstadoSefinDto : CodigoValorDto
    {
    }
    public class RegionalDto : CodigoValorDto
    {
    }
    public class DescuentoDto : CodigoValorDto
    {
    }
}
