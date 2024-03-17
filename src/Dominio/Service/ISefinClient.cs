using Dominio.Models;

namespace Dominio.Service
{
    public interface ISefinClient
    {
        SefinRecibo GetRecibo(uint tgr);

        bool ProcessRecibo(uint tgr);

        uint InsertRecibo(SefinRecibo recibo);
    }
}
