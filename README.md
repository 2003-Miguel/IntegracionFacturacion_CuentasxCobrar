# 💊 Integración de Facturación y Cuentas por Cobrar – Farmacia Carol

## 📌 Descripción del Proyecto
Este proyecto implementa una solución de integración entre el módulo de **Facturación** y el módulo de **Cuentas por Cobrar** para la Farmacia Carol, usando **ASP.NET Core**, **Entity Framework Core** y **SQL Server**, aprovechando el uso de **Triggers** como tecnología propietaria para asegurar la consistencia entre ambas aplicaciones.


## 🧩 Arquitectura de la Solución

- **Aplicación de Facturación**:
  - Permite registrar facturas con campos como: ClienteID, Fecha, Hora, Total, etc.
  - Al guardar una factura, se activa un **Trigger** en la base de datos que actualiza automáticamente el módulo de Cuentas por Cobrar.

- **Aplicación de Cuentas por Cobrar**:
  - Consulta los saldos pendientes por cliente.
  - Agrupa las facturas pendientes con su fecha, monto y estado.

- **Base de Datos**:
  - SQL Server con tablas relacionadas entre `Clientes`, `Facturas` y `CuentasPorCobrar`.
  - Trigger en la tabla `Facturas` para insertar automáticamente registros en `CuentasPorCobrar`.

## 🛠️ Tecnologías Utilizadas

- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- Triggers (SQL)
- REST API
- HTML + JavaScript (Interfaz cliente)

## 🔁 Disparador (Trigger) usado

```sql
CREATE TRIGGER trg_InsertarCxC_AfterFactura
ON Facturas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CuentasPorCobrar (ClienteID, FacturaID, Fecha, Hora, Monto, Estado)
    SELECT
        ClienteID,
        FacturaID,
        Fecha,
        Hora,
        Total,
        Estado
    FROM inserted;
END
