namespace CookingAPI.Constantes
{
    public class MensajesEn
    {
        public class Error
        {
            public const string ERROR_INSERTAR = "Error inserting the recipe.";
            public const string ERROR_NO_AUTENTICADO = "User not authenticated.";
            public const string ERROR_NO_PERMISO_ACTUALIZAR = "You do not have permission to update this recipe.";
            public const string ERROR_NO_EXISTE_RECETA = "The recipe does not exist.";
            public const string ERROR_NO_PERMISO_ELIMINAR = "You do not have permission to delete this recipe.";
            public const string ERROR_USERID_NO_ENCONTRADO = "UserId not found in the cache.";
            public const string ERROR_BUSCAR_USUARIO = "Error searching for the user by username.";
            public const string USUARIO_NO_ENCONTRADO = "User not found.";
            public const string CONTRASENA_INCORRECTA = "Incorrect password.";
            public const string ERROR_CREAR_USUARIO = "Error creating the user.";
            public const string USUARIO_CREADO_CORRECTAMENTE = "User created successfully.";
            public const string ERROR_VALIDAR_CREDENCIALES = "Error validating credentials for the user.";
            public const string ERROR_OBTENER_TODAS = "Error getting all entities of type {0}.";
            public const string ERROR_OBTENER_ENTIDAD = "Error getting the entity of type {0} with ID: {1}";
            public const string ERROR_AGREGAR_ENTIDAD = "Error adding a new entity of type {0}";
            public const string ERROR_ACTUALIZAR_ENTIDAD = "Error updating entity of type {0}";
            public const string ERROR_ELIMINAR_ENTIDAD = "Error deleting the entity of type {0} with ID: {1}";
            public const string ERROR_OBTENER_RECETAS_COMPLETAS = "Error getting all complete recipes.";
            public const string ERROR_BUSQUEDA_RECETAS = "Error searching for recipes.";
            public const string ERROR_ASOCIAR_RECETA_USUARIO = "Error associating recipe {0} to user {1}.";
            public const string ERROR_OBTENER_INGREDIENTES = "Error getting all ingredients.";
            public const string ERROR_OBTENER_INGREDIENTE_ID = "Error getting the ingredient with ID: {0}.";
            public const string ERROR_INSERTAR_INGREDIENTE = "Error inserting the ingredient: {0}.";
            public const string ERROR_ACTUALIZAR_INGREDIENTE = "Error updating the ingredient with ID: {0}.";
            public const string ERROR_ELIMINAR_INGREDIENTE = "Error deleting the ingredient with ID: {0}.";
            public const string ERROR_OBTENER_RECETAS = "Error getting all recipes.";
            public const string ERROR_OBTENER_RECETA_ID = "Error getting the recipe with ID: {0}.";
            public const string ERROR_AÑADIR_INGREDIENTE = "Error adding the ingredient {0}.";
            public const string ERROR_OBTENER_RECETA_COMPLETA_ID = "Error getting the complete recipe with ID {0}.";
            public const string ERROR_AÑADIR_RECETA = "Error adding the recipe {0}.";
            public const string ERROR_RECUPERAR_USERID = "Error recovering the UserId from the cache.";
            public const string ERROR_ESTABLECER_USERID = "Error setting the UserId in the cache for user {0}.";
            public const string ERROR_ELIMINAR_RECETA = "Error deleting the recipe with ID {0}.";
            public const string ERROR_ACTUALIZAR_RECETA = "Error updating the recipe with ID {0}.";
            public const string ERROR_REMOVER_USERID = "Error removing the UserId from the cache for user {0}.";
            public const string ERROR_OBTENER_USERID = "Error getting the user ID for {0}.";
            public const string ERROR_OBTENER_USUARIO = "Error getting user {0}.";
            public const string ERROR_ASOCIAR_RECETA = "Error associating recipe {0} to user {1}.";
            public const string ERROR_CREAR_RECETA = "Error creating the recipe {0}.";
            public const string ERROR_OBTENER_RECETAS_USUARIO = "Error getting the user's recipes.";
            public const string ERROR_CREAR_INGREDIENTE = "Error creating the ingredient.";
            public const string ERROR_OBTENER_INGREDIENTE = "Error getting the ingredient.";
            public const string ERROR_OBTENER_RECETA = "Error getting the recipe.";
            public const string ERROR_SERVIDOR = "Server error.";
            public const string ERROR_CERRAR_SESION = "Error logging out.";
            public const string ERROR_CREAR_RECETA_USUARIO = "Error creating the recipe for the user.";
        }

        public class Logs
        {
            public const string OBTENER_INGREDIENTES = "Getting all ingredients.";
            public const string OBTENER_INGREDIENTE_ID = "Getting ingredient with ID: {0}.";
            public const string INSERTAR_INGREDIENTE = "Inserting new ingredient: {0}.";
            public const string ACTUALIZAR_INGREDIENTE = "Updating ingredient with ID: {0}.";
            public const string ELIMINAR_INGREDIENTE = "Deleting ingredient with ID: {0}.";
            public const string OBTENER_RECETAS = "Getting all recipes.";
            public const string OBTENER_RECETA_ID = "Getting recipe with ID: {0}.";
            public const string OBTENER_RECETAS_COMPLETAS = "Getting all complete recipes.";
            public const string INICIANDO_BUSQUEDA_RECETAS = "Starting search for recipes.";
            public const string FILTRANDO_POR_NOMBRE = "Filtering recipes by name: {0}.";
            public const string FILTRANDO_POR_TIPO_DIETA = "Filtering recipes by diet type ID: {0}.";
            public const string FILTRANDO_POR_TIPO_ALERGENO = "Filtering recipes by allergen type ID: {0}.";
            public const string OBTENIENDO_ENTIDAD = "Getting entity of type {0} with ID: {1}.";
            public const string OBTENIENDO_TODAS = "Getting all entities of type {0}.";
            public const string AGREGANDO_ENTIDAD = "Adding a new entity of type {0}.";
            public const string ACTUALIZANDO_ENTIDAD = "Updating entity of type {0} with ID: {1}.";
            public const string ELIMINANDO_ENTIDAD = "Deleting entity of type {0} with ID: {1}.";
            public const string ENTIDAD_NO_ENCONTRADA = "Entity of type {0} with ID: {1} not found for deletion.";
            public const string BUSCANDO_USUARIO = "Searching for user with username: {0}.";
            public const string CREANDO_USUARIO = "Creating user {0}.";
            public const string ASOCIAR_RECETA_USUARIO = "Associating recipe {0} to user {1}.";
            public const string AÑADIR_INGREDIENTE = "Adding ingredient {0}.";
            public const string OBTENER_RECETA_COMPLETA_ID = "Getting complete recipe with ID {0}.";
            public const string AÑADIR_RECETA = "Adding recipe {0}.";
            public const string ACTUALIZAR_RECETA = "Updating recipe with ID {0}.";
            public const string ELIMINAR_RECETA = "Deleting recipe with ID {0}.";
            public const string BUSQUEDA_RECETA = "Searching for recipes with filters - Name: {0}, DietTypeId: {1}, AllergenTypeId: {2}.";
            public const string USERID_RECUPERADO = "UserId retrieved from the cache: {0}.";
            public const string USERID_NO_ENCONTRADO = "UserId not found in the cache.";
            public const string ESTABLECER_USERID = "UserId {0} set in the cache for user {1}.";
            public const string REMOVER_USERID = "UserId removed from the cache for user {0}.";
            public const string VALIDAR_CREDENCIALES = "Validating credentials for user {0}.";
            public const string OBTENER_USUARIO = "Getting user with name {0}.";
            public const string OBTENER_USERID = "Getting user ID for {0}.";
            public const string CREAR_USUARIO = "Creating new user {0}.";
            public const string ASOCIAR_RECETA = "Associating recipe {0} to user {1}.";
            public const string CREAR_RECETA = "Creating recipe {0}.";
            public const string OBTENER_RECETAS_USUARIO = "Getting all user recipes.";
            public const string CREAR_INGREDIENTE = "Ingredient created: {0}.";
            public const string INGREDIENTE_NO_ENCONTRADO = "Ingredient not found.";
            public const string NO_HAY_RECETAS = "No recipes available.";
            public const string RECETA_NO_ENCONTRADA = "Recipe not found.";
            public const string NO_RECETAS_CRITERIOS = "No recipes found with the specified criteria.";
            public const string USUARIO_CONTRASENA_INCORRECTA = "Username or password incorrect.";
            public const string USUARIO_EN_USO = "The username is already in use.";
            public const string CREAR_RECETA_USUARIO = "Recipe created by user with ID {0}: {1}.";
        }

        public class Informacion
        {
            public const string BUSCANDO_USUARIO = "Searching for user with username: {0}.";
            public const string USUARIO_AUTENTICADO = "User {0} authenticated successfully.";
        }
    }
}
