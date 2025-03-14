# Documentación del Proyecto de Reserva de Butacas

## Introducción
Este repositorio contiene una aplicación que permite la reserva de butacas en un sistema de reservas. La aplicación está dividida en dos partes principales:

- **`reserva-butacas/`**: Contiene la API desarrollada en .NET para gestionar las reservas.
- **`reserva-react/`**: Contiene el frontend desarrollado en React para la interfaz de usuario.

---

## Tecnologías Utilizadas

### Backend (`reserva-butacas/`)
- **Lenguaje**: C#
- **Framework**: .NET 8
- **Base de Datos**: SQL Server 
- **ORM**: Entity Framework Core
- **Otros**: Swagger para documentación de API, CORS

### Frontend (`reserva-react/`)
- **Lenguaje**: JTypeScript
- **Framework**: React 18
- **Estado Global**: Context API 
- **Estilizado**: TailwindCSS 
- **Manejo de Rutas**: React Router

---

## Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone https://github.com/tu-repo/reserva-butacas.git
cd reserva-butacas/
```

### 2. Configuración del Backend (`reserva-butacas/`)

1. Ir a la carpeta del backend:
```bash
cd reserva-butacas
```
2. Restaurar paquetes:
```bash
dotnet restore
```
3. Configurar la base de datos en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=ReservaDB;User Id=usuario;Password=contraseña;"
  }
}
```
4. Aplicar migraciones y actualizar la base de datos:
```bash
dotnet ef database update
```
5. Ejecutar el backend:
```bash
dotnet run
```

El backend estará disponible en `http://localhost:5000` (o `https://localhost:5001` para HTTPS).

---

### 3. Configuración del Frontend (`reserva-react/`)

1. Ir a la carpeta del frontend:
```bash
cd reserva-react
```
2. Instalar dependencias:
```bash
npm install
```
3. Configurar la URL del backend en `.env`:
```env
REACT_APP_API_URL=http://localhost:5000/api
```
4. Ejecutar el frontend:
```bash
npm start
```

La aplicación estará disponible en `http://localhost:3000`.

---



## Contribución
1. Hacer un fork del repositorio.
2. Crear una rama con la funcionalidad deseada (`feature/nueva-funcionalidad`).
3. Hacer un commit con cambios relevantes.
4. Enviar un Pull Request para revisión.

