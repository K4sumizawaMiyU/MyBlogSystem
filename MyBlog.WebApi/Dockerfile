FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyBlog.WebApi/MyBlog.WebApi.csproj", "MyBlog.WebApi/"]
RUN dotnet restore "MyBlog.WebApi/MyBlog.WebApi.csproj"
COPY . .
WORKDIR "/src/MyBlog.WebApi"
RUN dotnet build "MyBlog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlog.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlog.WebApi.dll"]
