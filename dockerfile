FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
 
WORKDIR /src 

COPY ["/FirstCodeDb/FirstCodeDb.sln", "/src/"]
COPY ["/FirstCodeDb/FirstCodeDb.csproj", "/src/"] 
 
RUN dotnet restore "/src/" 
COPY . .
WORKDIR "/src/"

RUN dotnet build "/src/FirstCodeDb/FirstCodeDb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/src/FirstCodeDb/FirstCodeDb.csproj" -c Release -o /app

FROM base AS final
# echo "Asia/Bangkok" > /etc/timezone
ENV TZ Asia/Bangkok
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=publish /app . 

RUN apt-get update
RUN apt-get install -y locales
RUN sed -i -e 's/# th_TH.UTF-8 UTF-8/th_TH.UTF-8 UTF-8/' /etc/locale.gen && \
    locale-gen 
ENV LC_ALL th_TH.UTF-8  
# ห้ามใช้ ใช้แล้ว entity framework ใช้งานไม่ได้
ENV LANG th_TH.UTF-8  
# ห้ามใช้ ใช้แล้ว entity framework ใช้งานไม่ได้
ENV LANGUAGE th_TH:th 
RUN apt-get clean && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*
ENTRYPOINT ["dotnet" , "FirstCodeDb.dll" ,"--environment=Development"  ]   