#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["MclApp.Api/MclApp.Api.csproj", "MclApp.Api/"]
COPY ["UserIdentity.Services/UserIdentity.Services.csproj", "UserIdentity.Services/"]
COPY ["MclApp.Core/MclApp.Core.csproj", "MclApp.Core/"]
COPY ["UserIdentity.ViewModels/UserIdentity.ViewModels.csproj", "UserIdentity.ViewModels/"]
COPY ["UserIdentity.Data/UserIdentity.Data.csproj", "UserIdentity.Data/"]
COPY ["MclApp.Data/MclApp.Data.csproj", "MclApp.Data/"]
COPY ["MclApp.Services/MclApp.Services.csproj", "MclApp.Services/"]
COPY ["MclApp.ViewModelServices/MclApp.ViewModelServices.csproj", "MclApp.ViewModelServices/"]
RUN dotnet restore "MclApp.Api/MclApp.Api.csproj"
COPY . .
WORKDIR "/src/MclApp.Api"
RUN dotnet build "MclApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MclApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MclApp.Api.dll"]