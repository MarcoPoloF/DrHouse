using DocNoc.API.Methods;

namespace DocNoc.Xam.Interfaces
{
    public interface IApiService
    {
        Aseguradoras Aseguradoras { get; set; }
        Citas Citas { get; set; }
        Consultorios Consultorios { get; set; }
        Especialidades Especialidades { get; set; }
        Mensajes Mensajes { get; set; }
        OpenPay OpenPay { get; set; }
        Pacientes Pacientes { get; set; }
        Suscripciones Suscripciones { get; set; }
        Usuarios Usuarios { get; set; }
        TipoEvento TipoEvento { get; set; }
    }
}
