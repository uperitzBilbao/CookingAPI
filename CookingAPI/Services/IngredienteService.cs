using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using CookingAPI.Requests;
using CookingAPI.Responses;

namespace CookingAPI.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepositorio _ingredienteRepositorio;
        private readonly ITipoAlergenoRepositorio _tipoAlergenoRepositorio;
        private readonly IIngredienteAlergenoRepositorio _ingredienteAlergenoRepositorio;
        private readonly ILogger<IngredienteService> _logger;

        public IngredienteService(ITipoAlergenoRepositorio tipoAlergenoRepositorio, IIngredienteAlergenoRepositorio ingredienteAlergenoRepositorio, IIngredienteRepositorio ingredienteRepositorio, ILogger<IngredienteService> logger)
        {
            _ingredienteRepositorio = ingredienteRepositorio;
            _ingredienteAlergenoRepositorio = ingredienteAlergenoRepositorio;
            _tipoAlergenoRepositorio = tipoAlergenoRepositorio;
            _logger = logger;
        }

        public IEnumerable<IngredienteResponse> GetAll()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTES);
                var lista = _ingredienteRepositorio.GetAll();
                List<IngredienteResponse> listaRespuesta = new List<IngredienteResponse>();
                foreach (Ingrediente i in lista)
                {
                    var response = new IngredienteResponse();
                    response.Nombre = i.Nombre + " (Tipo: " + i.TipoIngrediente + ")";

                    if (i.IngredienteAlergenos.Count != 0)
                    {

                        response.Nombre.Concat("[Alérgenos:");
                        foreach (IngredienteAlergeno ia in i.IngredienteAlergenos)
                        {
                            var alergeno = _tipoAlergenoRepositorio.GetById(ia.IdTipoAlergeno);
                            response.Nombre.Concat(" " + alergeno.Nombre);
                        }
                        response.Nombre.Concat("]");
                    }
                    listaRespuesta.Add(response);
                }
                return listaRespuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTES);
                throw;
            }
        }

        public IngredienteResponse? Get(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTE_ID, id);
                var ingrediente = _ingredienteRepositorio.Get(id);
                var response = new IngredienteResponse();

                if (ingrediente != null)
                {


                    response.Nombre = ingrediente.Nombre + " (Tipo: " + ingrediente.TipoIngrediente + ")";

                    if (ingrediente.IngredienteAlergenos.Count != 0)
                    {

                        response.Nombre.Concat("[Alérgenos:");
                        foreach (IngredienteAlergeno ia in ingrediente.IngredienteAlergenos)
                        {
                            var alergeno = _tipoAlergenoRepositorio.GetById(ia.IdTipoAlergeno);
                            response.Nombre.Concat(" " + alergeno.Nombre);
                        }
                        response.Nombre.Concat("]");
                    }

                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTE_ID, id);
                throw;
            }
        }

        public void Add(IngredienteRequest request)
        {
            try
            {
                var nuevo = new Ingrediente
                {
                    Nombre = request.Nombre,
                    IdIngrediente = request.IdTipoIngrediente,
                };

                _logger.LogInformation(Mensajes.Logs.AÑADIR_INGREDIENTE, nuevo.Nombre);
                _ingredienteRepositorio.Add(nuevo);

                foreach (string alergeno in request.Alergenos)
                {
                    var idAlergeno = _tipoAlergenoRepositorio.GetByNombre(alergeno);
                    var tipo = _tipoAlergenoRepositorio.GetByNombre(alergeno);
                    if (tipo != null)
                    {
                        var nuevoIngredienteAlergeno = new IngredienteAlergeno
                        {
                            IdTipoAlergeno = tipo.IdTipoAlergeno,
                            IdIngrediente = nuevo.IdIngrediente
                        };

                        _ingredienteAlergenoRepositorio.Add(nuevoIngredienteAlergeno);
                    }
                    else
                    {
                        _logger.LogError(Mensajes.Error.ERROR_ALERGENO_NO_RECONOCIDO + alergeno);
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_AÑADIR_INGREDIENTE, request.Nombre);
                throw;
            }
        }

        public void Update(int id, IngredienteRequest request)
        {
            try
            {

                var nuevo = new Ingrediente
                {
                    Nombre = request.Nombre,
                    IdIngrediente = request.IdTipoIngrediente,
                };

                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_INGREDIENTE, id);
                _ingredienteRepositorio.Update(id, nuevo);

                //Borrar los registros de la tabla intermedia para volver a introducir los nuevos.
                _ingredienteAlergenoRepositorio.DeleteByIngrediente(request.IdTipoIngrediente);

                foreach (string alergeno in request.Alergenos)
                {
                    var idAlergeno = _tipoAlergenoRepositorio.GetByNombre(alergeno);
                    var tipo = _tipoAlergenoRepositorio.GetByNombre(alergeno);
                    if (tipo != null)
                    {
                        var nuevoIngredienteAlergeno = new IngredienteAlergeno
                        {
                            IdTipoAlergeno = tipo.IdTipoAlergeno,
                            IdIngrediente = nuevo.IdIngrediente
                        };

                        _ingredienteAlergenoRepositorio.Add(nuevoIngredienteAlergeno);
                    }
                    else
                    {
                        _logger.LogError(Mensajes.Error.ERROR_ALERGENO_NO_RECONOCIDO + alergeno);
                    }

                }


                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_INGREDIENTE, id);
                _ingredienteRepositorio.Update(id, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ACTUALIZAR_INGREDIENTE, id);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_INGREDIENTE, id);
                _ingredienteRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ELIMINAR_INGREDIENTE, id);
                throw;
            }
        }
    }
}
