using DocNoc.API.Methods;
using DocNoc.Xam.Interfaces;

namespace DocNoc.Xam.Services
{
    public class ApiDataService : IApiService
    {
        public ApiDataService()
        {
            Aseguradoras = new Aseguradoras();
            Citas = new Citas();
            Consultorios = new Consultorios();
            Especialidades = new Especialidades();
            Mensajes = new Mensajes();
            OpenPay = new OpenPay();
            Pacientes = new Pacientes();
            Suscripciones = new Suscripciones();
            Usuarios = new Usuarios();
            TipoEvento = new TipoEvento();
        }

        public Aseguradoras Aseguradoras { get; set; }
        public Citas Citas { get; set; }
        public Consultorios Consultorios { get; set; }
        public Especialidades Especialidades { get; set; }
        public Mensajes Mensajes { get; set; }
        public OpenPay OpenPay { get; set; }
        public Pacientes Pacientes { get; set; }
        public Suscripciones Suscripciones { get; set; }
        public Usuarios Usuarios { get; set; }
        public TipoEvento TipoEvento { get; set; }
    }
}
