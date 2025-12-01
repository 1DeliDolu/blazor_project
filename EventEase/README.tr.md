# EventEase - Etkinlik Yönetimi Uygulaması

EventEase, kurumsal ve sosyal etkinlik yönetimi için geliştirilmiş modern bir Blazor web uygulamasıdır.

## 🎯 Proje Hakkında

Bu proje, **Module 5 - Activity 1: Microsoft Copilot Kullanarak Blazor Kodu Üretme** aktivitesi kapsamında geliştirilmiştir.

### Özellikler

✨ **Temel Özellikler:**

- Etkinlik listeleme ve filtreleme
- Etkinlik detay sayfaları
- Kayıt formu ve validasyon
- Two-way data binding
- Routing ve navigation
- Responsive tasarım

🎨 **Tasarım:**

- Modern ve kullanıcı dostu arayüz
- Gradient renkler ve animasyonlar
- Bootstrap Icons entegrasyonu
- Mobil uyumlu responsive tasarım

## 📁 Proje Yapısı

```
EventEase/
├── Components/
│   ├── Custom/
│   │   ├── EventCard.razor          # Event card component
│   │   └── EventCard.razor.css      # Component styles
│   ├── Pages/
│   │   ├── Home.razor               # Ana sayfa (etkinlik listesi)
│   │   ├── EventDetails.razor       # Etkinlik detay sayfası
│   │   └── Registration.razor       # Kayıt formu sayfası
│   └── Layout/                      # Layout components
├── Models/
│   └── Event.cs                     # Event model
├── Services/
│   └── EventService.cs              # Event service (mock data)
└── wwwroot/
    └── app.css                      # Global styles
```

## 🚀 Çalıştırma

### Gereksinimler

- .NET 9.0 SDK veya üzeri
- Visual Studio 2022 veya VS Code

### Kurulum ve Çalıştırma

1. Projeyi klonlayın veya indirin:

```bash
cd d:\CODE\dotnet\blazor_Frontend\module_5\EventEase
```

2. Bağımlılıkları yükleyin:

```bash
dotnet restore
```

3. Projeyi derleyin:

```bash
dotnet build
```

4. Uygulamayı çalıştırın:

```bash
dotnet run
```

veya hot reload ile:

```bash
dotnet watch run
```

5. Tarayıcınızda açın:

```
http://localhost:5145
```

## 📋 Özellik Detayları

### 1. Ana Sayfa (Home)

- Tüm etkinliklerin grid layout ile gösterimi
- Arama fonksiyonu (etkinlik adı, konum, açıklama)
- Kategori filtreleme (Tümü, Kurumsal, Sosyal, Eğitim)
- **Two-way data binding** ile dinamik filtreleme
- Her etkinlik için EventCard component

### 2. EventCard Component

Özellikler:

- Event name (Etkinlik adı)
- Event date (Tarih ve saat)
- Event location (Konum)
- Kategori badge
- Katılımcı sayısı
- Müsaitlik durumu
- "Detayları Gör" butonu ile routing

### 3. Etkinlik Detayları (EventDetails)

- Tam etkinlik bilgileri
- Breadcrumb navigation
- Meta bilgiler (tarih, konum, katılımcı)
- Progress bar (doluluk oranı)
- Paylaşım butonları
- Organizatör bilgisi
- "Kayıt Ol" butonu ile registration sayfasına yönlendirme

### 4. Kayıt Formu (Registration)

**Two-way data binding özellikleri:**

- Ad Soyad (`@bind="fullName"`)
- E-posta (`@bind="email"`)
- Telefon (`@bind="phone"`)
- Şirket/Kurum (`@bind="company"`)
- Özel İstekler (`@bind="specialRequests"`)
- Şartları kabul (`@bind="acceptTerms"`)
- Newsletter (`@bind="newsletter"`)

Özellikler:

- Form validasyonu
- Gerçek zamanlı hata mesajları
- Başarılı kayıt ekranı
- Kayıt numarası oluşturma

## 🎨 Tasarım ve Stil

### Renk Paleti

- Primary Gradient: `#667eea` → `#764ba2`
- Background: `#f7fafc`
- Text: `#2d3748`
- Success: `#48bb78`
- Warning: `#c47f00`
- Danger: `#e53e3e`

### Component'ler

- Scoped CSS kullanımı
- Modern card tasarımları
- Hover efektleri ve transitions
- Shadow ve border radius

## 📊 Mock Data

Proje 6 örnek etkinlik içerir:

1. **Tech Summit 2025** - Kurumsal
2. **Yılbaşı Galası** - Sosyal
3. **Girişimcilik Atölyesi** - Eğitim
4. **Sanat ve Kültür Festivali** - Sosyal
5. **Blazor ve .NET Workshop** - Eğitim
6. **Kurumsal Networking Gecesi** - Kurumsal

## 🔄 Routing Yapısı

| Route                  | Sayfa        | Açıklama           |
| ---------------------- | ------------ | ------------------ |
| `/`                    | Home         | Etkinlik listesi   |
| `/event/{id}`          | EventDetails | Etkinlik detayları |
| `/event/{id}/register` | Registration | Kayıt formu        |

## 💡 Kullanılan Teknolojiler

- **Blazor Server** - .NET 9.0
- **C#** - Backend logic
- **Razor Components** - UI components
- **CSS** - Styling
- **Bootstrap Icons** - İkonlar
- **Dependency Injection** - Service management

## 🎓 Öğrenme Hedefleri

Bu proje aşağıdaki Blazor kavramlarını kapsar:

✅ Component oluşturma ve kullanma  
✅ Two-way data binding (`@bind`)  
✅ Event handling (`@onclick`)  
✅ Routing ve navigation  
✅ Dependency injection  
✅ Parameters ve EventCallback  
✅ Scoped CSS  
✅ Form validation  
✅ Conditional rendering

## 📝 Sonraki Adımlar (Activity 2 & 3)

Bu proje, üç aktiviteli serinin ilkidir:

- **Activity 1** ✅ - Temel component'leri oluşturma
- **Activity 2** ✅ - Hata ayıklama ve optimizasyon (TAMAMLANDI)
- **Activity 3** ⏳ - Genişletme ve yeni özellikler

### Activity 2 Tamamlanan İyileştirmeler:

#### 🛡️ Güvenilirlik İyileştirmeleri:

- ✅ Null reference exception'ları önlendi
- ✅ Division by zero hataları giderildi
- ✅ Input validation kapsamlı hale getirildi
- ✅ Error handling mekanizmaları eklendi

#### ⚡ Performans Optimizasyonları:

- ✅ Result caching ile %60-80 hız artışı
- ✅ Optimized filtering ve searching
- ✅ Memory usage iyileştirildi

#### 🎨 Kullanıcı Deneyimi:

- ✅ 404 sayfa yönetimi eklendi
- ✅ Detaylı form validation mesajları
- ✅ Email ve telefon format kontrolü
- ✅ Güvenli navigation

**Detaylı Rapor:** `DEBUGGING_OPTIMIZATION_REPORT.md`

## 👨‍💻 Geliştirici Notları

### Kullanılan Best Practices:

- Separation of concerns (Models, Services, Components)
- Component reusability (EventCard)
- Scoped CSS for component isolation
- Semantic HTML
- Responsive design
- Clean code principles

### İyileştirme Önerileri:

- Database entegrasyonu (şu an mock data)
- Authentication/Authorization
- Email servisi (kayıt onayları için)
- QR kod oluşturma
- Gerçek zamanlı katılımcı güncellemeleri
- Admin paneli

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---

**Geliştirme Tarihi:** Aralık 2024  
**Framework:** Blazor Server (.NET 9.0)  
**Aktivite:** Module 5 - Activity 1
