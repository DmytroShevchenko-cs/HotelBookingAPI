FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["HotelBooking.sln", "./"]
COPY ["HotelBooking.Web/HotelBooking.Web.csproj", "HotelBooking.Web/"]
COPY ["HotelBooking.DAL/HotelBooking.DAL.csproj", "HotelBooking.DAL/"]
COPY ["HotelBooking.BLL/HotelBooking.BLL.csproj", "HotelBooking.BLL/"]
COPY ["HotelBooking.Database/HotelBooking.Database.csproj", "HotelBooking.Database/"]
COPY ["HotelBooking.Shared/HotelBooking.Shared.csproj", "HotelBooking.Shared/"]

RUN dotnet restore "HotelBooking.sln"

COPY . .

WORKDIR "/src/HotelBooking.Web"
RUN dotnet build "HotelBooking.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelBooking.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "HotelBooking.Web.dll"]

