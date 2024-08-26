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
    "Domain/Entities/Project.cs",
    "Domain/Repositories/IProjectRepository.cs",
    "Domain/Services/IProjectService.cs",
    "Application/Commands/CreateProjectCommand.cs",
    "Application/Commands/UpdateProjectCommand.cs",
    "Application/Commands/DeleteProjectCommand.cs",
    "Application/Queries/GetProjectByIdQuery.cs",
    "Application/Queries/GetAllProjectsQuery.cs",
    "Application/Handlers/ProjectCommandHandler.cs",
    "Application/Handlers/ProjectQueryHandler.cs",
    "Application/DTOs/ProjectDto.cs",
    "Infrastructure/Data/ProjectDbContext.cs",
    "Infrastructure/Repositories/ProjectRepository.cs",
    "Infrastructure/Services/ProjectService.cs",
    "Infrastructure/Extensions/ServiceCollectionExtensions.cs",
    "Presentation/Controllers/ProjectController.cs",
    "Shared/Constants/AppConstants.cs"
]

# Crear carpetas
for folder in folders:
    if not os.path.exists(folder):
        os.makedirs(folder)

# Crear archivos
for file in files:
    if not os.path.exists(file):
        with open(file, 'w') as f:
            pass

print("Estructura de archivos creada con éxito.")
