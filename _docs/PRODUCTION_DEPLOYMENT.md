# EventEase - Production Deployment Rehberi

## 📦 Genel Bakış

Bu doküman, EventEase uygulamasının production ortamına deployment için gerekli adımları içermektedir.

**Versiyon:** 1.0.0  
**Tarih:** 1 Aralık 2025  
**Platform:** ASP.NET Core 9.0 Blazor Server

---

## ✅ Production Hazırlık Kontrol Listesi

### 1. Kod Kalitesi ✅

- [x] Gereksiz using statements kaldırıldı
- [x] \_Imports.razor optimize edildi
- [x] Null safety ve defensive programming uygulandı
- [x] Thread-safe service implementasyonları
- [x] Comprehensive error handling
- [x] XML documentation comments

### 2. Performans Optimizasyonları ✅

- [x] Response compression etkinleştirildi
- [x] Static asset caching
- [x] Singleton ve Scoped service lifetimes optimize edildi
- [x] Toast message limiti (MaxMessages = 5)
- [x] JSON serialization options configured
- [x] Defensive data copying

### 3. Güvenlik ✅

- [x] HTTPS redirection zorunlu
- [x] HSTS enabled (production)
- [x] Antiforgery protection
- [x] Input validation (client + server)
- [x] XSS protection (Razor automatic encoding)
- [x] CSRF protection

### 4. Logging ✅

- [x] Production logging levels configured (Warning)
- [x] Development logging levels (Debug/Information)
- [x] Console ve Debug logging providers
- [x] Structured logging with categories

---

## 🚀 Deployment Adımları

### Adım 1: Build Hazırlığı

```powershell
# Projeyi temizle
dotnet clean

# Restore dependencies
dotnet restore

# Build testi (Debug mode)
dotnet build --configuration Debug
```

### Adım 2: Production Build

```powershell
# Release build
dotnet build --configuration Release

# Publish (self-contained veya framework-dependent)
dotnet publish --configuration Release --output ./publish

# Framework-dependent publish (önerilen)
dotnet publish -c Release -o ./publish --no-self-contained

# Self-contained publish (runtime dahil)
dotnet publish -c Release -o ./publish --self-contained -r win-x64
```

### Adım 3: Konfigürasyon

#### appsettings.json (Production)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "EventEase": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "MaxRegistrationsPerEvent": 1000,
    "SessionTimeoutMinutes": 60,
    "EnableDetailedErrors": false
  }
}
```

### Adım 4: Environment Variables

Production ortamında şu environment variables ayarlanmalı:

```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:443;http://+:80
ASPNETCORE_HTTPS_PORT=443
```

---

## 🌐 IIS Deployment

### web.config Örneği

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet"
                  arguments=".\EventEase.dll"
                  stdoutLogEnabled="false"
                  stdoutLogFile=".\logs\stdout"
                  hostingModel="inprocess" />
      <security>
        <requestFiltering>
          <requestLimits maxAllowedContentLength="52428800" />
        </requestFiltering>
      </security>
    </system.webServer>
  </location>
</configuration>
```

### IIS Adımları

1. IIS'de yeni bir site oluştur
2. Physical path'i publish klasörüne ayarla
3. Application Pool'u .NET CLR Version: "No Managed Code" olarak ayarla
4. Bindings'e HTTPS ekle (port 443)
5. SSL sertifikası yükle
6. Application'ı start et

---

## 🐳 Docker Deployment

### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["EventEase.csproj", "./"]
RUN dotnet restore "EventEase.csproj"
COPY . .
RUN dotnet build "EventEase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventEase.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventEase.dll"]
```

### Docker Commands

```bash
# Build image
docker build -t eventease:1.0 .

# Run container
docker run -d -p 8080:80 -p 8443:443 --name eventease eventease:1.0

# Stop container
docker stop eventease

# View logs
docker logs eventease
```

---

## ☁️ Azure App Service Deployment

### CLI ile Deployment

```bash
# Azure login
az login

# Resource group oluştur
az group create --name EventEaseRG --location eastus

# App Service plan oluştur
az appservice plan create --name EventEasePlan --resource-group EventEaseRG --sku B1

# Web app oluştur
az webapp create --resource-group EventEaseRG --plan EventEasePlan --name eventease --runtime "DOTNETCORE:9.0"

# Deploy
az webapp deployment source config-zip --resource-group EventEaseRG --name eventease --src ./publish.zip
```

### Visual Studio ile Deployment

1. Solution Explorer'da projeye sağ tıkla
2. "Publish" seç
3. "Azure" target'ı seç
4. "Azure App Service (Windows)" seç
5. Subscription ve Resource Group seç/oluştur
6. "Publish" tıkla

---

## 📊 Performance Monitoring

### Application Insights (Azure)

```csharp
// Program.cs'e ekle
builder.Services.AddApplicationInsightsTelemetry();
```

### Health Checks

```csharp
// Program.cs
builder.Services.AddHealthChecks();

// Pipeline'a ekle
app.MapHealthChecks("/health");
```

---

## 🔒 Güvenlik Best Practices

### SSL/TLS Sertifikası

- **Production:** Let's Encrypt veya ücretli SSL sertifikası kullan
- **Development:** Self-signed sertifika yeterli

### CORS (Gerekirse)

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://yourdomain.com")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

app.UseCors("AllowSpecificOrigin");
```

---

## 📈 Ölçeklendirme

### Horizontal Scaling

Blazor Server için SignalR ölçeklendirme:

```csharp
// Azure SignalR Service
builder.Services.AddSignalR().AddAzureSignalR();

// Redis backplane
builder.Services.AddSignalR().AddStackExchangeRedis("redis-connection-string");
```

### Load Balancing

- Sticky sessions (affinity) etkinleştir
- Health check endpoints kullan
- Auto-scaling rules tanımla

---

## 🐛 Troubleshooting

### Yaygın Sorunlar

**1. 500 Internal Server Error**

- `appsettings.json` eksik veya hatalı
- Database connection string geçersiz
- Service registration hatası

**Çözüm:**

```bash
# Detaylı hata loglarını göster
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

**2. SignalR Connection Failed**

- WebSocket support eksik (IIS)
- Firewall port 443 kapalı

**Çözüm:**

```powershell
# IIS WebSocket feature yükle
Install-WindowsFeature -name Web-WebSockets
```

**3. High Memory Usage**

- Circuit state buildup
- Memory leak

**Çözüm:**

```csharp
// Circuit options ayarla
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = false;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
});
```

---

## 📋 Deployment Checklist

- [ ] Production build başarılı
- [ ] appsettings.json production değerleri
- [ ] SSL sertifikası yüklü
- [ ] Environment variables ayarlandı
- [ ] Database migrations uygulandı
- [ ] Health checks çalışıyor
- [ ] Logging production'da test edildi
- [ ] Performance monitoring aktif
- [ ] Backup stratejisi hazır
- [ ] Rollback planı mevcut

---

## 🔧 Bakım

### Düzenli Kontroller

- **Günlük:** Log files kontrol
- **Haftalık:** Performance metrics review
- **Aylık:** Security updates check
- **3 Aylık:** Dependency updates

### Update Stratejisi

```powershell
# NuGet packages güncelle
dotnet list package --outdated

# Specific package güncelle
dotnet add package Microsoft.AspNetCore.Components.Web --version 9.0.x

# Tüm packages güncelle
dotnet restore
dotnet build
```

---

## 📞 Destek

**Dokümantasyon:**

- [ASP.NET Core Blazor](https://learn.microsoft.com/aspnet/core/blazor)
- [Azure App Service](https://learn.microsoft.com/azure/app-service)

**İletişim:**

- GitHub Issues: [Repository Link]
- Email: support@eventease.com

---

**Son Güncelleme:** 1 Aralık 2025  
**Doküman Versiyonu:** 1.0  
**Uygulama Versiyonu:** 1.0.0
