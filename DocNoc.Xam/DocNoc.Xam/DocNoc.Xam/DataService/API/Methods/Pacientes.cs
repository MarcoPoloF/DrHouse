using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la información de pacientes registrados en Doc+Noc.
    /// </summary>
    public class Pacientes
    {
        //Ruta base del módulo.
        private readonly string controlador = "Pacientes";

        public async Task<CodigoRespuesta> ActualizaMedicamentoATomado(TomaMedicamento content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaMedicamentoATomado", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> AgregaArchivoExpediente(AgregaArchivosExpediente content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/AgregaArchivoExpediente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> AgregaHistClinicaelPaciente(ExpedienteHistoriaClinica content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/AgregaHistClinicaelPaciente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> AgregaFavorito(Favorito content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/AgregaFavorito", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ComparteArchivoExpediente(ParaComparteArchivoExpediente content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ComparteArchivoExpediente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> EliminaFavorito(Favorito content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/EliminaFavorito", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<MedicoEsFavorito>> EsFavorito(ParaFiltroUsuarios content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<MedicoEsFavorito>)await api.PeticionHTTP<Contenedor<MedicoEsFavorito>>
                ($"{controlador}/EsFavorito", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<ContenedorLista<ExpedienteHistoriaClinica>> HistClinicoPacienteelPaciente(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ExpedienteHistoriaClinica>)await api.PeticionHTTP<ContenedorLista<ExpedienteHistoriaClinica>>
                ($"{controlador}/HistClinicoPacienteelPaciente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> InsertaMiMedicamento(InsertarMiMedicamento content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaMiMedicamento", MetodoHTTP.Post, RequiereJwt.Si, content);
        }
        public async Task<CodigoRespuesta> InsertaMedicamentoProgramacion(InsertarMedicamentoProgramacion content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaMedicamentoProgramacion", MetodoHTTP.Post, RequiereJwt.Si, content);
        }


        public async Task<ContenedorLista<EstudioAnalisisPaciente>> ListaEstudiosyAnalisisPaciente(ParaFiltroUsuario2 content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<EstudioAnalisisPaciente>)await api.PeticionHTTP<ContenedorLista<EstudioAnalisisPaciente>>
                ($"{controlador}/ListaEstudiosyAnalisisPaciente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<MiMedicamento>> MisMedicamentos(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<MiMedicamento>)await api.PeticionHTTP<ContenedorLista<MiMedicamento>>
                ($"{controlador}/MisMedicamentos", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<MiMedicamentoAPP>> MisMedicamentosAPP(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<MiMedicamentoAPP>)await api.PeticionHTTP<ContenedorLista<MiMedicamentoAPP>>
                ($"{controlador}/MisMedicamentosAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ListaFavoritos>> ListaFavoritos(ParaFiltroUsuario content)
        {
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            return (ContenedorLista<ListaFavoritos>)await api.PeticionHTTP<ContenedorLista<ListaFavoritos>>
                ($"{controlador}/ListaFavorito", MetodoHTTP.Post, RequiereJwt.Si, content);
        }
    }
}
