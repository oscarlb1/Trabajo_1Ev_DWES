# Dockerfile

# Stage 1: Build - Usamos el SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo del proyecto (Academia.Api.csproj)
# Asumimos que está en la raíz del contexto de compilación
COPY Academia.Api.csproj .

# Restaura las dependencias
RUN dotnet restore

# Copia todos los archivos restantes (código fuente)
COPY . .

# Publica el proyecto. La salida va a /app/publish
# Aquí es donde se crea Academia.Api.dll
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Final - Usamos solo el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia los archivos publicados (la DLL y sus dependencias)
COPY --from=build /app/publish .

# Define el punto de entrada
# Si el proyecto se llama Academia.Api, este es el nombre de la DLL
ENTRYPOINT ["dotnet", "Academia.Api.dll"]