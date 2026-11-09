# NexaStore | فروشگاه اینترنتی مدرن و مقیاس‌پذیر

**NexaStore** یک پروژه فروشگاه اینترنتی با معماری مدرن و ساختار قابل توسعه است که با هدف شبیه‌سازی یک سیستم تجارت الکترونیک واقعی و مطابق با استانداردهای توسعه نرم‌افزار طراحی شده است.

این پروژه با استفاده از معماری **Modular Monolith** و اصول **Clean Architecture** توسعه داده می‌شود تا ضمن حفظ جداسازی مسئولیت‌ها، امکان توسعه، نگهداری و گسترش سیستم در آینده فراهم باشد.

### تکنولوژی‌ها و ابزارهای مورد استفاده

- **Backend:** .NET 10, ASP.NET Core Web API, C#
- **Frontend:** React, TypeScript, Tailwind CSS
- **Database:** PostgreSQL, Entity Framework Core
- **Caching:** Redis
- **Message Broker:** RabbitMQ
- **Containerization:** Docker, Docker Compose
- **Monitoring & Observability:** OpenTelemetry
- **Architecture:** Modular Monolith, Clean Architecture

### ویژگی‌های فنی و معماری

- جداسازی لایه‌های Domain، Application، Infrastructure و API
- طراحی ماژولار برای بخش‌هایی مانند محصولات، کاربران، سفارش‌ها، موجودی و پرداخت
- طراحی APIهای استاندارد برای ارتباط Frontend و Backend
- استفاده از پایگاه داده رابطه‌ای و ORM
- استفاده از Cache برای بهبود عملکرد
- پردازش پیام‌ها و عملیات غیرهمزمان با Message Broker
- اجرای سرویس‌ها در محیط کانتینری با Docker
- قابلیت پایش و عیب‌یابی سیستم با ابزارهای Observability

**هدف پروژه:** پیاده‌سازی یک فروشگاه اینترنتی با رویکرد Production-oriented، با تمرکز بر معماری اصولی، توسعه‌پذیری، نگهداری آسان و عملکرد مناسب.