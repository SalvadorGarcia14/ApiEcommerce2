Minuta de Relevamiento del Proyecto de API REST de Ecommerce de Venta de Productos de PCs
1. Información General
    Nombre del Proyecto: API REST de Ecommerce Venta de Productos PCs
    Tecnologías Utilizadas:
        C#
        .NET 6
        Visual Studio 2022
        Arquitectura: Clean Architecture
2. Objetivo del Proyecto
    Desarrollar una API REST para un sistema de Ecommerce que permita gestionar usuarios, productos, órdenes y detalles de órdenes, facilitando así la operación y administración de un comercio electrónico.

3. Componentes Principales
    3.1 Domain Interfaces
        IJwtTokenService:

            Función: Generar tokens JWT para la autenticación de usuarios.
            IOrdenRepository:
        
                Métodos:
                    GetAllAsync(): Obtiene todas las órdenes.
                    GetByUsuarioEmailAsync(string email): Obtiene órdenes filtradas por email del usuario.
                    GetByIdAsync(int id): Obtiene una orden por ID.
                    AddAsync(Orden orden): Agrega una nueva orden.
                    UpdateAsync(Orden orden): Actualiza una orden existente.
                    DeleteAsync(int id): Elimina una orden por ID.
                    IOrdenService:
                
                Métodos:
                    ObtenerOrdenesPorUsuario(string email, string role): Obtiene órdenes por email y rol del usuario.
                    ObtenerOrdenPorId(int id): Obtiene una orden por ID.
                    CrearOrden(Orden orden): Crea una nueva orden.
                    ModificarOrden(Orden orden): Modifica una orden existente.
                    EliminarOrden(int id): Elimina una orden.
                    IProductoRepository:
                
                Métodos:
                    GetAllAsync(): Obtiene todos los productos.
                    GetByNombreAsync(string nombre): Obtiene un producto por nombre.
                    GetByIdAsync(int id): Obtiene un producto por ID.
                    AddAsync(Producto producto): Agrega un nuevo producto.
                    UpdateAsync(Producto producto): Actualiza un producto existente.
                    DeleteAsync(int id): Elimina un producto por ID.
                    IProductoService:
                
                Métodos:
                    ObtenerProductos(): Obtiene la lista de productos.
                    ObtenerProductoPorNombre(string nombre): Obtiene un producto por nombre.
                    ObtenerProductoPorId(int id): Obtiene un producto por ID.
                    AgregarProducto(Producto producto): Agrega un nuevo producto.
                    ModificarProducto(Producto producto): Modifica un producto existente.
                    EliminarProducto(int id): Elimina un producto.
                    IUsuarioRepository:
                
                Métodos:
                    GetAllAsync(): Obtiene todos los usuarios.
                    GetByNombreAsync(string nombre): Obtiene un usuario por nombre.
                    GetByEmailAsync(string email): Obtiene un usuario por email.
                    AddAsync(Usuario usuario): Agrega un nuevo usuario.
                    UpdateAsync(Usuario usuario): Actualiza un usuario existente.
                    DeleteAsync(int id): Elimina un usuario por ID.
                    IUsuarioService:
                
                Métodos:
                    RegisterAsync(...): Registra un nuevo usuario.
                    ObtenerPorEmailAsync(string email): Obtiene un usuario por email.
                    LoginAsync(string email, string password): Realiza el login de un usuario.
                    ObtenerUsuarios(): Obtiene todos los usuarios.
                    ModificarUsuario(Usuario usuario): Modifica un usuario existente.
                    EliminarUsuarioPorEmail(string email): Elimina un usuario por email.
    3.2 Infrastructure
            ApplicationDbContext:
                Función: Contexto de base de datos que incluye las entidades de Producto, Usuario, Orden y DetalleOrden.
            Repositories:
            OrdenRepository: Implementación de IOrdenRepository para la gestión de órdenes.
            ProductoRepository: Implementación de IProductoRepository para la gestión de productos.
            UsuarioRepository: Implementación de IUsuarioRepository para la gestión de usuarios.
4. Requerimientos Funcionales
    Autenticación y Autorización:
        Generación de tokens JWT para usuarios.
        Gestión de Usuarios:
            Registro, modificación, eliminación y obtención de usuarios.
        Gestión de Productos:
            Agregar, modificar, eliminar y obtener productos.
        Gestión de Órdenes:
            Creación, modificación, eliminación y obtención de órdenes y detalles de órdenes.
5. Conclusiones y Próximos Pasos
    La API REST está diseñada con una arquitectura limpia y cumple con los requerimientos establecidos para la gestión de un Ecommerce. Los próximos pasos incluyen:
    
    Implementación de pruebas unitarias.
    Despliegue de la API en un entorno de producción.
    Documentación del API para facilitar su uso.