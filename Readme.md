Test Assessment: Application Developer Technical Skill [ชุดที่ 9]

---

ภาพรวมโปรเจกต์

โปรเจกต์นี้เป็นระบบ Fullstack สำหรับจัดการ **Post และ Comment** พัฒนาโดยใช้เทคโนโลยีดังนี้:

* **Backend:** ASP.NET Core (Minimal API)
* **Frontend:** Vue.js
* **Database:** PostgreSQL (ผ่าน Docker)
* **ORM:** Entity Framework Core
* **Architecture:** Minimal API + Service Layer (แยก Business Logic)

---

สิ่งที่ต้องติดตั้งก่อนใช้งาน

* .NET SDK (เวอร์ชัน 8 ขึ้นไป)
* Node.js (เวอร์ชัน 18 ขึ้นไป)
* Docker Desktop

---

วิธีเริ่มต้นใช้งาน (Quick Start)

```bash
git clone https://github.com/Noom-Thanawat/IT09.git
cd it09

cd it09-backend
docker compose up -d

dotnet run

cd ../it09-frontend
npm install
npm run dev
```

---

ขั้นตอนการรันระบบ (แบบละเอียด)

### 1. Clone โปรเจกต์

```bash
git clone https://github.com/Noom-Thanawat/IT09.git
cd it09
```

---

### 2. เปิด PostgreSQL ด้วย Docker

```bash
cd it09-backend
docker compose up -d
```

ข้อมูล Database:

* Host: `localhost`
* Port: `5432`
* Database: `it09db`
* Username: `postgres`
* Password: `postgres`

---

### 3. รัน Backend

```bash
dotnet run
```

**ระบบจะทำงานอัตโนมัติ**

* สร้าง/อัปเดต Database จาก Migration
* Seed ข้อมูลตัวอย่าง (ครั้งแรกเท่านั้น)
* เปิด API Server

**Swagger:**

```
https://localhost:7207/swagger
```

> หาก port ไม่ตรง ให้ตรวจสอบในไฟล์ `launchSettings.json`

---

### 4. รัน Frontend

```bash
cd ../it09-frontend
npm install
npm run dev
```

**Frontend**

```
http://localhost:5173
```

---

### 5. รัน Unit Test

```bash
cd ../it09-backend.Test
dotnet test
```

---
