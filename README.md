# KoopSatış - Kooperatif Satış Sistemi

Bu proje, kooperatifler için özel olarak geliştirilmiş bir satış ve stok yönetim sistemidir. ASP.NET Core 7 kullanılarak geliştirilmiştir.

## Özellikler

- **Kullanıcı Yönetimi**: Farklı rollere sahip kullanıcılar (Admin, Manager, User, KooperatifÜyesi, SatışPersoneli, Depocu)
- **Üye Yönetimi**: Kooperatif üyelerinin kayıt ve takip sistemi
- **Ürün Yönetimi**: Ürün ekleme, düzenleme, silme işlemleri
- **Stok Takibi**: Depo bazlı stok yönetimi
- **Satış İşlemleri**: Satış yapma, fatura oluşturma
- **Raporlama**: Satış, stok ve kullanıcı aktivite raporları

## Teknolojiler

- ASP.NET Core 7.0
- Entity Framework Core
- Identity Framework
- SQLite Veritabanı
- Bootstrap 5
- jQuery

## Başlangıç

### Gereksinimler

- .NET 7.0 SDK
- Visual Studio 2022 veya Visual Studio Code

### Kurulum

1. Repoyu klonlayın:
   ```
   git clone https://github.com/kullanici/KoopSaleDotnet.git
   ```

2. Proje dizinine gidin:
   ```
   cd KoopSaleDotnet
   ```

3. Bağımlılıkları yükleyin:
   ```
   dotnet restore
   ```

4. Veritabanını oluşturun:
   ```
   dotnet ef database update
   ```

5. Uygulamayı çalıştırın:
   ```
   dotnet run
   ```

6. Tarayıcınızda aşağıdaki adrese gidin:
   ```
   https://localhost:7128
   ```

### Varsayılan Kullanıcılar

Uygulama ilk çalıştırıldığında otomatik olarak bir admin kullanıcısı oluşturulur:

- **E-posta**: admin@koopsatis.com
- **Şifre**: Admin123!

## Katkıda Bulunma

1. Bu repoyu forklayın
2. Yeni özellik dalı oluşturun (`git checkout -b yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik: Açıklama'`)
4. Dalınıza push yapın (`git push origin yeni-ozellik`)
5. Pull Request oluşturun
