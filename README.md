# PhoneBookProject

Rise Technology firmasının assessment projesidir. Uygulama microservis mimarisiyle tasarlanmış bir telefon rehberi uygulamasıdır. Çalışmadan beklenen genel yapı :

- Minimum, Contact ve Person olmak üzere 2 micro servis olmalıdır.
- Contact micro servisinde kişilere ait temel CRUD işlemleri yapılamtadır. Ayrıca kişilere ait iletişim bilgileride ayrıca tutulmaktadır. Bir kişinin 1'e N ilişkili iletişim bilgisi olabilir. Ayrıca bu iletişim bilgileri içinde CRUD işlemleri yapılabiliyor.
- Report micro servisinde rapor oluşturma talebi, raparun detaylarını görüntüleme ve tüm raporları listeleme özellikleri sağlanmaktadır. Ek olarak rapor oluşturma talebinden sonra rapor oluşturma süreci mesaj kuyruğu kullanan bir yapı ile arkaplan işlemi olarak devam edecektir. Diğer micro servis ile iletişimi HTTP veya AMQ üzerinden yapacaktır.

# Kullanılan Teknolojiler

- [.NET 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)

- [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)

- [PostGreSQL](https://www.postgresql.org/)

- [RabbitMQ](https://www.rabbitmq.com/)

- [Docker](https://www.docker.com/)

- [xUnit](https://xunit.net/)

- [Moq](https://github.com/moq)

- [Coverlet](https://github.com/coverlet-coverage/coverlet)

# Başlarken

1) [**Docker**](https://www.docker.com/)'ın bilgisayarınızda yüklü olduğundan emin olunuz. Ardından aşağıdaki komut ile RabbitMq'u Docker üzerinden çalıştırınız.

```bash
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
```

2. [**PostgreSQL**](https://www.postgresql.org/)'nin bilgisayarınızda yüklü olduğundan emin olunuz. Ardından aşağıdaki adımları uygulayınız.
   1. **Contact.API** içerisinde bulunan **appsettings.json** dosyasındaki **"ConnectionStrings"** içerisinde bulunan **User ID**, **Password** ve **Host** bilgilerini kendinize uygun şekilde düzenleyeniz.
   2. **Report.API** içerisinde bulunan **appsettings.json** dosyasındaki **"ConnectionStrings"** içerisinde bulunan **User ID**, **Password** ve **Host** bilgilerini kendinize uygun şekilde düzenleyeniz.

3. **RabbitMQ** bağlantı bilgisini kendinize göre düzenlemek isterseniz eğer aşağıdaki adımları uygulayınız. 

   1. **Contact.API** içerisinde bulunan **appsettings.json** dosyasındaki **"Options"** içerisinde bulunan **RabbitMqCon** bilgisini kendinize uygun şekilde düzenleyeniz.

      **NOT:** Default ayarlarla kullanmak isterseniz eğer değişiklik yapmanıza gerek yoktur.
   
   2. **Report.API** içerisinde bulunan **appsettings.json** dosyasındaki **"Options"** içerisinde bulunan **RabbitMqCon** bilgisini kendinize uygun şekilde düzenleyeniz.
   
         **NOT:** Default ayarlarla kullanmak isterseniz eğer değişiklik yapmanıza gerek yoktur.
   
4. **Contact.API** için **Report.API** içerisinde bulunan **appsettings.json** dosyasındaki **ApiUrl** bilgisini kendinize uygun şekilde düzenleyiniz.
   
5. **Report.API** için **Contact.API** içerisinde bulunan **appsettings.json** dosyasındaki **ApiUrl** bilgisini kendinize uygun şekilde düzenleyiniz.

   > **NOT:** Projeler **IIS** üzerinden ayağa kaldırılacaksa eğer **4**. ve **5.** maddelerde değişiklik yapmanıza gerek yoktur.
6.  **Contact.API** klasörü içerisinde bir **terminal** açıp aşağıdaki komut ile **Contact.API** projesini çalıştırabilirsiniz.

      ```bash
      dotnet run
      ```

7. **Report.API** klasörü içerisinde bir **terminal** açıp aşağıdaki komut ile **Report.API** projesini çalıştırabilirsiniz.

      ```bash
      dotnet run
      ```

8. Projeler varsayılan ayarlar ile derlenip, çalıştırıldığında aşağıdaki url'ler üzerinden **swagger** arayüzüne ulaşabilirsiniz.

      ```
      Contact.API Url: https://localhost:7136/swagger/index.html
      Report.API Url   : https://localhost:7183/swagger/index.html
      ```

# Unit Test Code Coverage Sonuçları

![image](https://raw.githubusercontent.com/cildiz/EducationManagementSystem/master/EducationManagementSystem.WebAPI/screenshots/ss1.PNG)


![image](https://raw.githubusercontent.com/cildiz/EducationManagementSystem/master/EducationManagementSystem.WebAPI/screenshots/ss2.PNG)


![image](https://raw.githubusercontent.com/cildiz/EducationManagementSystem/master/EducationManagementSystem.WebAPI/screenshots/ss3.PNG)

