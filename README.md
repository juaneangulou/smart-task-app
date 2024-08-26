# SmartTaskApp 🚀

SmartTaskApp es una aplicación modular compuesta por varios microservicios, diseñada para la gestión de tareas y proyectos. La aplicación permite a los usuarios administrar sus proyectos, tareas y notificaciones de manera eficiente, con soporte para autenticación y autorización.

## Arquitectura 🏗

SmartTaskApp sigue los principios de **Domain-Driven Design (DDD)**, organizando el código alrededor de los conceptos y reglas de negocio. Dentro de esta arquitectura, se implementa **CQRS (Command Query Responsibility Segregation)** utilizando **MediatR** en la capa de aplicación para separar las responsabilidades de comandos y consultas.

### Capas de la Arquitectura:

- **Dominio** 🧩:
  - Modela las entidades, valores y reglas de negocio.
  - Contiene lógica pura de negocio, aislada de las infraestructuras técnicas.

- **Aplicación** ⚙️:
  - Coordina las operaciones del sistema, manejando los casos de uso de la aplicación.
  - Implementa CQRS utilizando MediatR, donde los comandos manejan las operaciones de escritura y las consultas manejan las operaciones de lectura.

- **Infraestructura** 🛠:
  - Contiene la implementación de acceso a datos, servicios externos, y la persistencia de los modelos del dominio.
  - Implementa los repositorios y otros servicios necesarios para soportar la lógica de negocio.

- **Presentación** 🎨:
  - Proporciona la interfaz de usuario o API, conectando los clientes con la lógica de aplicación.

## Características ✨

- **Gestión de Tareas** 📋: Crear, actualizar, eliminar y consultar tareas.
- **Gestión de Proyectos** 🗂: Administrar proyectos y sus relaciones con las tareas.
- **Notificaciones** 📬: Enviar y recibir notificaciones relacionadas con las tareas y proyectos.
- **Autenticación y Autorización** 🔐: Manejo de usuarios, roles y tokens JWT.
- **Arquitectura basada en Microservicios** 🛠: Cada funcionalidad está distribuida en microservicios independientes.

## Tecnologías Utilizadas 🛠

- **.NET 8**: Framework principal para el desarrollo de la aplicación.
- **Entity Framework Core**: ORM para la gestión de la base de datos.
- **PostgreSQL**: Base de datos relacional utilizada por la aplicación.
- **MediatR**: Implementación de CQRS para el manejo de comandos y consultas.
- **FluentValidation**: Validación de modelos de datos.
- **Serilog**: Registro de logs.
- **Swagger**: Documentación de la API.
- **Docker**: Contenerización de los microservicios.
- **Kubernetes**: Orquestación de contenedores.
- **JWT**: Autenticación basada en JSON Web Tokens.

## Estructura del Proyecto 📁

La solución está dividida en varios proyectos para facilitar la separación de preocupaciones:

- **SmartTaskApp.Auth.WebApi**: Microservicio de autenticación y autorización.
- **SmartTaskApp.TaskManagement.WebApi**: Microservicio de gestión de tareas.
- **SmartTaskApp.ProjectManagement.WebApi**: Microservicio de gestión de proyectos.
- **SmartTaskApp.Notifications.WebApi**: Microservicio de notificaciones.
- **SmartTaskApp.CommonLib**: Biblioteca común que contiene middlewares, configuraciones y otros elementos reutilizables.
- **SmartTaskApp.CommonDb**: Biblioteca que contiene los modelos de base de datos y `DbContext`.

## Configuración ⚙️

### Requisitos 📝

- **.NET 8 SDK**
- **Docker**
- **PostgreSQL**
- **Kubernetes (Opcional, para despliegue en producción)**

### Configuración de la Base de Datos 🗄️

Primero, asegúrate de que PostgreSQL esté configurado correctamente. Puedes usar el siguiente comando para iniciar un contenedor Docker con PostgreSQL:

```bash
docker run --name smarttask-postgres -e POSTGRES_USER=smarttask_db -e POSTGRES_PASSWORD=Sm@rtt@ks2024 -e POSTGRES_DB=smarttaskdb -p 5432:5432 -d postgres
```

### Migraciones de la Base de Datos 🔄

Para aplicar las migraciones de la base de datos, navega al directorio del proyecto `SmartTaskApp.CommonDb` y ejecuta los siguientes comandos:

```bash
dotnet ef migrations add InitialCreate -p Common\SmartTaskApp.CommonDb -s SmartTaskApp.Auth\SmartTaskApp.Auth.WebApi
dotnet ef database update -p Common\SmartTaskApp.CommonDb -s SmartTaskApp.Auth\SmartTaskApp.Auth.WebApi
```

### Configuración de la Aplicación ⚙️

Asegúrate de que los archivos `appsettings.json` contengan las cadenas de conexión correctas y la configuración JWT.

```json
{
  "Jwt": {
    "Key": "your_secret_key_here",
    "Issuer": "your_issuer_here",
    "Audience": "your_audience_here"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=smarttaskdb;Username=smarttask_db;Password=Sm@rtt@ks2024"
  }
}
```

### Ejecución 🚀

Para ejecutar los microservicios, puedes utilizar los siguientes comandos:

```bash
dotnet run --project SmartTaskApp.Auth\SmartTaskApp.Auth.WebApi
dotnet run --project SmartTaskApp.TaskManagement\SmartTaskApp.TaskManagement.WebApi
dotnet run --project SmartTaskApp.ProjectManagement\SmartTaskApp.ProjectManagement.WebApi
dotnet run --project SmartTaskApp.Notifications\SmartTaskApp.Notifications.WebApi
```

### Despliegue 🌐

PENDING

## Uso 📚

Una vez que la aplicación esté en funcionamiento, puedes acceder a la documentación de la API a través de Swagger en la URL:

```
http://localhost:[puerto]/swagger
```

Cada microservicio tendrá su propia URL de Swagger.



## Licencia 📄

Este proyecto está licenciado bajo los términos de la licencia MIT. Consulta el archivo `LICENSE` para obtener más información.
