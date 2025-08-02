# 🧩 Microservices-Based RESTful API Architecture

Bu repoda, farklı veritabanları ve servislerle çalışan, RESTful prensiplere uygun geliştirilmiş çeşitli mikroservisler bulunmaktadır. Proje, modern mikroservis mimarisi ve API yönetimi üzerine örnek uygulamalar sunmaktadır.

## 🔧 Genel Mimarî

- Her mikroservis kendi veritabanına sahiptir.
- RESTful API prensiplerine uygun geliştirilmiştir.
- API Gateway olarak **Ocelot** kullanılmıştır (C#).
- API'ler arasında bağımsızlık mevcuttur.
- Farklı veri kaynakları entegre edilmiştir: TCMB Döviz Kurları, Deprem API vb.

---

## 📁 Mikroservisler ve Özellikleri

### 1. 🟢 MongoDB Tabanlı RESTful API
- **Teknoloji:** ASP.NET Core + MongoDB
- **Amaç:** MongoDB üzerinde CRUD işlemleri yapan RESTful API.
- **Kullanım:** `GET /products`, `POST /products`, `PUT /products/{id}`, `DELETE /products/{id}`

### 2. ⚙️ RESTful Mikroservis (Microservice)
- **Yapı:** Her servis bağımsız, tek bir görevi yerine getirir.
- **İletişim:** HTTP (REST)
- **Özellik:** Servisler loosely coupled şekilde çalışır.

### 3. 📦 SQLite Tabanlı API
- **Amaç:** Endpoint’ten alınan veriyi SQLite’a kaydetme ve veri çekme.
- **Teknoloji:** ASP.NET Core + Dapper/EF Core + SQLite
- **Örnek Endpoint:** `POST /data/save`, `GET /data/all`

### 4. 💱 TCMB Döviz API Entegrasyonu
- **API Kaynağı:** [TCMB XML servisleri](https://www.tcmb.gov.tr/kurlar/today.xml)
- **Amaç:** Döviz kurlarını çekip, JSON formatında sunmak.
- **Kullanım:** `GET /exchange-rates`

### 5. 🌍 Deprem API Entegrasyonu
- **Kaynak:** Kandilli Rasathanesi veya AFAD
- **Amaç:** Türkiye'deki güncel depremleri çekmek.
- **Kullanım:** `GET /earthquakes`

### 6. 🔐 API Gateway (Ocelot - C#)
- **Teknoloji:** Ocelot + ASP.NET Core
- **Amaç:** Tüm servisleri tek bir noktadan yönlendirmek.
- **Gateway URL:** `https://localhost:5000/api/{servis-adı}`

### 7. 🐘 PostgreSQL Tabanlı RESTful API
- **Teknoloji:** ASP.NET Core + Npgsql + PostgreSQL
- **Amaç:** PostgreSQL üzerinde veri işlemleri.
- **Kullanım:** `GET /users`, `POST /users`, `PUT /users/{id}`, `DELETE /users/{id}`

### 8. 🟦 MSSQL Tabanlı RESTful API
- **Teknoloji:** ASP.NET Core + Entity Framework + MS SQL Server
- **Amaç:** MSSQL üzerinde CRUD işlemleri gerçekleştiren REST API.
- **Kullanım:** `GET /employees`, `POST /employees`

---

## 🔄 Kurulum ve Çalıştırma

### 1. Clone edin
```bash
git clone https://github.com/yigitsener7/MicroServices.git
cd MicroServices
