namespace CookingAPI.Constantes
{
    public class Mensajes
    {
        public class Error
        {
            public const string ERROR_INSERTAR = "Error al insertar la receta.";
            public const string ERROR_NO_AUTENTICADO = "Usuario no autenticado.";
            public const string ERROR_NO_PERMISO_ACTUALIZAR = "No tienes permiso para actualizar esta receta.";
            public const string ERROR_NO_EXISTE_RECETA = "La receta no existe.";
            public const string ERROR_NO_PERMISO_ELIMINAR = "No tienes permiso para eliminar esta receta.";
            public const string ERROR_USERID_NO_ENCONTRADO = "No se encontró el userId en la caché.";
            public const string ERROR_BUSCAR_USUARIO = "Error al buscar el usuario por nombre de usuario.";
            public const string USUARIO_NO_ENCONTRADO = "Usuario no encontrado.";
            public const string CONTRASENA_INCORRECTA = "Contraseña incorrecta.";
            public const string ERROR_CREAR_USUARIO = "Error al crear el usuario.";
            public const string USUARIO_CREADO_CORRECTAMENTE = "Usuario creado correctamente.";
            public const string ERROR_VALIDAR_CREDENCIALES = "Error al validar las credenciales para el usuario.";
            public const string ERROR_OBTENER_TODAS = "Error al obtener todas las entidades de tipo {0}.";
            public const string ERROR_OBTENER_ENTIDAD = "Error al obtener la entidad de tipo {0} con ID: {1}";
            public const string ERROR_AGREGAR_ENTIDAD = "Error al agregar una nueva entidad de tipo {0}";
            public const string ERROR_ACTUALIZAR_ENTIDAD = "Error al actualizar entidad de tipo {0}";
            public const string ERROR_ELIMINAR_ENTIDAD = "Error al eliminar la entidad de tipo {0} con ID: {1}";
            public const string ERROR_OBTENER_RECETAS_COMPLETAS = "Error al obtener todas las recetas completas.";
            public const string ERROR_BUSQUEDA_RECETAS = "Error al buscar recetas.";
            public const string ERROR_ASOCIAR_RECETA_USUARIO = "Error al asociar la receta {0} al usuario {1}.";
        }

        public class Logs
        {
            public const string OBTENER_INGREDIENTES = "Obteniendo todos los ingredientes.";
            public const string ERROR_OBTENER_INGREDIENTES = "Error al obtener todos los ingredientes.";
            public const string OBTENER_INGREDIENTE_ID = "Obteniendo ingrediente con ID: {0}.";
            public const string ERROR_OBTENER_INGREDIENTE_ID = "Error al obtener el ingrediente con ID: {0}.";
            public const string INSERTAR_INGREDIENTE = "Insertando nuevo ingrediente: {0}.";
            public const string ERROR_INSERTAR_INGREDIENTE = "Error al insertar el ingrediente: {0}.";
            public const string ACTUALIZAR_INGREDIENTE = "Actualizando ingrediente con ID: {0}.";
            public const string ERROR_ACTUALIZAR_INGREDIENTE = "Error al actualizar el ingrediente con ID: {0}.";
            public const string ELIMINAR_INGREDIENTE = "Eliminando ingrediente con ID: {0}.";
            public const string ERROR_ELIMINAR_INGREDIENTE = "Error al eliminar el ingrediente con ID: {0}.";

            public const string OBTENER_RECETAS = "Obteniendo todas las recetas.";
            public const string ERROR_OBTENER_RECETAS = "Error al obtener todas las recetas.";
            public const string OBTENER_RECETA_ID = "Obteniendo receta con ID: {0}.";
            public const string ERROR_OBTENER_RECETA_ID = "Error al obtener la receta con ID: {0}.";
            public const string OBTENER_RECETAS_COMPLETAS = "Obteniendo todas las recetas completas.";
            public const string ERROR_OBTENER_RECETAS_COMPLETAS = "Error al obtener todas las recetas completas.";
            public const string INICIANDO_BUSQUEDA_RECETAS = "Iniciando búsqueda de recetas.";
            public const string FILTRANDO_POR_NOMBRE = "Filtrando recetas por nombre: {0}.";
            public const string FILTRANDO_POR_TIPO_DIETA = "Filtrando recetas por tipo de dieta ID: {0}.";
            public const string FILTRANDO_POR_TIPO_ALERGENO = "Filtrando recetas por tipo de alérgeno ID: {0}.";
            public const string ERROR_BUSQUEDA_RECETAS = "Error al buscar recetas.";

            public const string OBTENIENDO_ENTIDAD = "Obteniendo entidad de tipo {0} con ID: {1}.";
            public const string ERROR_OBTENER_ENTIDAD = "Error al obtener la entidad de tipo {0} con ID: {1}.";
            public const string OBTENIENDO_TODAS = "Obteniendo todas las entidades de tipo {0}.";
            public const string ERROR_OBTENER_TODAS = "Error al obtener todas las entidades de tipo {0}.";
            public const string AGREGANDO_ENTIDAD = "Agregando una nueva entidad de tipo {0}.";
            public const string ERROR_AGREGAR_ENTIDAD = "Error al agregar una nueva entidad de tipo {0}.";
            public const string ACTUALIZANDO_ENTIDAD = "Actualizando entidad de tipo {0} con ID: {1}.";
            public const string ERROR_ACTUALIZAR_ENTIDAD = "Error al actualizar la entidad de tipo {0} con ID: {1}.";
            public const string ELIMINANDO_ENTIDAD = "Eliminando entidad de tipo {0} con ID: {1}.";
            public const string ERROR_ELIMINAR_ENTIDAD = "Error al eliminar la entidad de tipo {0} con ID: {1}.";
            public const string ENTIDAD_NO_ENCONTRADA = "Entidad de tipo {0} con ID: {1} no encontrada para eliminación.";

            public const string BUSCANDO_USUARIO = "Buscando usuario con nombre de usuario: {0}.";
            public const string CREANDO_USUARIO = "Creando usuario {0}.";
            public const string ASOCIAR_RECETA_USUARIO = "Asociando receta {0} al usuario {1}.";

            public const string AÑADIR_INGREDIENTE = "Añadiendo ingrediente {0}.";
            public const string ERROR_AÑADIR_INGREDIENTE = "Error al añadir el ingrediente {0}.";

            public const string OBTENER_RECETA_COMPLETA_ID = "Obteniendo receta completa con ID {0}.";
            public const string ERROR_OBTENER_RECETA_COMPLETA_ID = "Error al obtener la receta completa con ID {0}.";
            public const string AÑADIR_RECETA = "Añadiendo receta {0}.";
            public const string ERROR_AÑADIR_RECETA = "Error al añadir la receta {0}.";
            public const string ACTUALIZAR_RECETA = "Actualizando receta con ID {0}.";
            public const string ERROR_ACTUALIZAR_RECETA = "Error al actualizar la receta con ID {0}.";
            public const string ELIMINAR_RECETA = "Eliminando receta con ID {0}.";
            public const string ERROR_ELIMINAR_RECETA = "Error al eliminar la receta con ID {0}.";
            public const string BUSQUEDA_RECETA = "Buscando recetas con filtros - Nombre: {0}, TipoDietaId: {1}, TipoAlergenoId: {2}.";

            public const string USERID_RECUPERADO = "UserId recuperado de la caché: {0}.";
            public const string USERID_NO_ENCONTRADO = "UserId no encontrado en la caché.";
            public const string ERROR_RECUPERAR_USERID = "Error al recuperar el UserId de la caché.";
            public const string ESTABLECER_USERID = "UserId {0} establecido en la caché para el usuario {1}.";
            public const string ERROR_ESTABLECER_USERID = "Error al establecer el UserId en la caché para el usuario {0}.";
            public const string REMOVER_USERID = "UserId removido de la caché para el usuario {0}.";
            public const string ERROR_REMOVER_USERID = "Error al remover el UserId de la caché para el usuario {0}.";

            public const string VALIDAR_CREDENCIALES = "Validando credenciales para el usuario {0}.";
            public const string OBTENIENDO_USUARIO = "Obteniendo usuario con nombre {0}.";
            public const string ERROR_OBTENER_USUARIO = "Error al obtener el usuario {0}.";
            public const string OBTENIENDO_USERID = "Obteniendo ID de usuario para {0}.";
            public const string ERROR_OBTENER_USERID = "Error al obtener el ID de usuario para {0}.";
            public const string CREAR_USUARIO = "Creando nuevo usuario {0}.";
            public const string ERROR_CREAR_USUARIO = "Error al crear el usuario {0}.";
            public const string ASOCIAR_RECETA = "Asociando receta {0} al usuario {1}.";
            public const string ERROR_ASOCIAR_RECETA = "Error al asociar la receta {0} al usuario {1}.";

            public const string CREAR_RECETA = "Creando receta {0}.";
            public const string ERROR_CREAR_RECETA = "Error al crear la receta {0}.";
            public const string OBTENER_RECETAS_USUARIO = "Obteniendo todas las recetas del usuario.";
            public const string ERROR_OBTENER_RECETAS_USUARIO = "Error al obtener las recetas del usuario.";

            public const string CREAR_INGREDIENTE = "Ingrediente creado: {0}.";
            public const string ERROR_CREAR_INGREDIENTE = "Error al crear el ingrediente.";
            public const string INGREDIENTE_NO_ENCONTRADO = "Ingrediente no encontrado.";
            public const string ERROR_OBTENER_INGREDIENTE = "Error al obtener el ingrediente.";

            public const string ERROR_OBTENER_RECETA = "Error al obtener la receta.";

            public const string NO_HAY_RECETAS = "No hay recetas disponibles.";
            public const string RECETA_NO_ENCONTRADA = "Receta no encontrada.";
            public const string NO_RECETAS_CRITERIOS = "No se encontraron recetas con los criterios especificados.";

            public const string USUARIO_CONTRASENA_INCORRECTA = "Nombre de usuario o contraseña incorrectos.";
            public const string USUARIO_EN_USO = "El nombre de usuario ya está en uso.";
            public const string ERROR_SERVIDOR = "Error en el servidor.";
            public const string ERROR_CERRAR_SESION = "Error al cerrar sesión.";

            public const string CREAR_RECETA_USUARIO = "Receta creada por el usuario con ID {0}: {1}.";
            public const string ERROR_CREAR_RECETA_USUARIO = "Error al crear la receta para el usuario.";

            public const string ERROR_NO_AUTENTICADO = "Usuario no autenticado.";
            public const string ERROR_NO_PERMISO_ACTUALIZAR = "No tienes permiso para actualizar esta receta.";
            public const string ERROR_NO_PERMISO_ELIMINAR = "No tienes permiso para eliminar esta receta.";




        }

        public class Informacion
        {
            public const string BUSCANDO_USUARIO = "Buscando usuario con nombre de usuario: {0}.";
            public const string USUARIO_AUTENTICADO = "Usuario {0} autenticado correctamente.";
        }
    }
}
