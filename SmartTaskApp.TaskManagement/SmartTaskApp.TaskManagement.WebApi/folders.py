# coding=utf-8
import os

folders = [
    "Domain/Entities",
    "Domain/Repositories",
    "Domain/Services",
    "Domain/ValueObjects",
    "Application/Commands",
    "Application/Queries",
    "Application/Handlers",
    "Application/DTOs",
    "Infrastructure/Data",
    "Infrastructure/Repositories",
    "Infrastructure/Services",
    "Infrastructure/Extensions",
    "Presentation/Controllers",
    "Shared/Constants"
]

files = [
    "Domain/Entities/Task.cs",
    "Domain/Repositories/ITaskRepository.cs",
    "Domain/Services/ITaskService.cs",
    "Application/Commands/CreateTaskCommand.cs",
    "Application/Commands/UpdateTaskCommand.cs",
    "Application/Commands/DeleteTaskCommand.cs",
    "Application/Queries/GetTaskByIdQuery.cs",
    "Application/Queries/GetAllTasksQuery.cs",
    "Application/Handlers/TaskCommandHandler.cs",
    "Application/Handlers/TaskQueryHandler.cs",
    "Application/DTOs/TaskDto.cs",
    "Infrastructure/Data/TaskDbContext.cs",
    "Infrastructure/Repositories/TaskRepository.cs",
    "Infrastructure/Services/TaskService.cs",
    "Infrastructure/Extensions/ServiceCollectionExtensions.cs",
    "Presentation/Controllers/TaskController.cs",
    "Shared/Constants/AppConstants.cs"
]

# Crear carpetas si no existen
for folder in folders:
    if not os.path.exists(folder):
        os.makedirs(folder)

# Crear archivos vacíos si no existen
for file in files:
    if not os.path.exists(file):
        with open(file, 'w') as f:
            pass

print("Estructura de archivos creada con éxito.")
