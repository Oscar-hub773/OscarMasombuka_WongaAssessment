FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WongaAssessment.API/WongaAssessment.API/WongaAssessment.API.csproj", "WongaAssessment.API/"]
RUN dotnet restore "WongaAssessment.API/WongaAssessment.API.csproj"

COPY . .

WORKDIR "/src/WongaAssessment.API/WongaAssessment.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WongaAssessment.API.dll"]