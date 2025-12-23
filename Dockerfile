# Etapa 1 — Build (Usando SDK 9.0)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o arquivo da solução
COPY ControleGastoResidencial.sln ./ 

# Copia arquivos de projeto (.csproj)
COPY Api.API/*.csproj Api.API/
COPY Api.Application/*.csproj Api.Application/
COPY Api.Infrastructure/*.csproj Api.Infrastructure/
COPY Api.Domain/*.csproj Api.Domain/
COPY Api.Authentication/*.csproj Api.Authentication/

# Restaura dependências
RUN dotnet restore

# Copia todo o código restante
COPY . .

# Compila o projeto de entrada (API)
WORKDIR /src/Api.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 — Runtime (Usando ASP.NET 9.0)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.API.dll"]