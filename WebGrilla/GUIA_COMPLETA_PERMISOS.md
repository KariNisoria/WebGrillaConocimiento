# ?? Sistema de Permisos - Guía Completa

## ?? **RESUMEN EJECUTIVO**

He implementado un sistema completo de permisos por módulo que te permite:
- ? **Gestionar permisos desde base de datos** (recomendado)
- ? **Sistema de fallback** por código (actual)
- ? **Interfaz web** para administrar permisos
- ? **Control granular** por rol y módulo

---

## ??? **ESTRUCTURA ACTUAL DE PERMISOS**

### **Formato de permisos:**
`MÓDULO_ACCIÓN` donde:
- **MÓDULO**: RECURSOS, GRILLAS, EVALUACIONES, etc.
- **ACCIÓN**: READ (consultar), WRITE (crear/editar), DELETE (eliminar)

### **Permisos por rol actual:**

| ROL | PERMISOS | DESCRIPCIÓN |
|-----|----------|-------------|
| **Administrador** | Todos los permisos | Control completo del sistema |
| **Manager** | READ/WRITE sin DELETE críticos | Gestión operativa |
| **Evaluador** | EVALUACIONES + READ básicos | Enfocado en evaluaciones |
| **Desarrollador** | Solo READ básicos | Consulta únicamente |

---

## ?? **CONFIGURACIÓN PASO A PASO**

### **PASO 1: Ejecutar Script de Configuración**

1. **Ubicación del script:**
   ```
   Scripts/06_CrearTablaRolPermisos.sql
   ```

2. **Ejecutar en SQL Server:**
   - Abre SQL Server Management Studio
   - Conecta a tu base de datos `WebGrillaConocimiento`
   - Ejecuta el script completo
   - Verifica que no haya errores

3. **Lo que hace el script:**
   - ? Crea la tabla `RolPermisos`
   - ? Configura permisos para todos los roles
   - ? Establece permisos granulares por módulo
   - ? Muestra estadísticas de configuración

### **PASO 2: Verificar la Configuración**

1. **Consulta para ver todos los permisos:**
   ```sql
   SELECT 
       r.Nombre as Rol,
       rp.CodigoPermiso,
       rp.Descripcion,
       rp.Activo
   FROM RolPermisos rp
   INNER JOIN Roles r ON r.IdRol = rp.IdRol
   ORDER BY r.Nombre, rp.CodigoPermiso
   ```

2. **Verificar que la aplicación funcione:**
   - Compila sin errores ?
   - Los menús se muestran según permisos ?
   - La página `/permisos` está disponible ?

---

## ?? **INTERFAZ DE ADMINISTRACIÓN**

### **Página de Administración de Permisos**
- **URL:** `/permisos`
- **Acceso:** Solo usuarios con permiso `PERMISOS_ADMIN`
- **Funciones:**
  - Ver estado del sistema de permisos
  - Consultas SQL predefinidas
  - Guía de configuración
  - Ayuda contextual

### **Ubicación en el menú:**
- Solo aparece para administradores
- Sección "ADMINISTRACIÓN" en el menú lateral

---

## ?? **GESTIÓN DE PERMISOS**

### **OPCIÓN A: Por SQL (Actual)**

1. **Agregar permiso a un rol:**
   ```sql
   INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion)
   VALUES (
       (SELECT IdRol FROM Roles WHERE Nombre = 'Manager'),
       'NUEVO_MODULO_READ',
       'Consultar nuevo módulo'
   )
   ```

2. **Desactivar permiso:**
   ```sql
   UPDATE RolPermisos 
   SET Activo = 0
   WHERE IdRol = (SELECT IdRol FROM Roles WHERE Nombre = 'Evaluador')
   AND CodigoPermiso = 'RECURSOS_WRITE'
   ```

3. **Ver permisos de un rol específico:**
   ```sql
   SELECT rp.CodigoPermiso, rp.Descripcion
   FROM RolPermisos rp
   INNER JOIN Roles r ON r.IdRol = rp.IdRol
   WHERE r.Nombre = 'Manager' AND rp.Activo = 1
   ORDER BY rp.CodigoPermiso
   ```

### **OPCIÓN B: Por Código (Fallback)**
Si no hay permisos en BD, el sistema usa los permisos hardcodeados en:
`WebGrilla/Services/AuthenticationService.cs` método `GetPermisosDefaultPorRol()`

---

## ?? **CÓMO USAR EL SISTEMA**

### **En Blazor (.razor):**
```csharp
@if (AuthState.UserCanAccess("RECURSOS"))
{
    <a href="/recursos" class="btn btn-primary">Ver Recursos</a>
}

@if (AuthState.UserHasPermission("RECURSOS_WRITE"))
{
    <button @onclick="CrearRecurso">Crear Recurso</button>
}
```

### **En C# (Controllers/Services):**
```csharp
// En el futuro podrás usar atributos como:
[RequirePermission("RECURSOS_READ")]
public async Task<IActionResult> GetRecursos()
{
    // Lógica del método
}
```

---

## ?? **DIAGNÓSTICO Y TROUBLESHOOTING**

### **Problemas Comunes:**

1. **No se muestran los menús:**
   - ? Verificar que el usuario tenga permisos
   - ? Ejecutar el script de configuración
   - ? Comprobar que `AuthState.IsAuthenticated` sea true

2. **Script SQL falla:**
   - Verificar que existan las tablas `Roles` y usuarios
   - Ejecutar primero `Scripts/05_ConfiguracionAutenticacion.sql`

3. **Permisos no se actualizan:**
   - Cerrar sesión y volver a iniciar sesión
   - Los permisos se cargan al hacer login

### **Consultas de diagnóstico:**

```sql
-- Ver usuarios y sus roles
SELECT r.Nombre, r.Apellido, r.CorreoElectronico, rol.Nombre as Rol
FROM Recursos r
INNER JOIN Roles rol ON r.IdRol = rol.IdRol

-- Contar permisos por rol
SELECT r.Nombre, COUNT(rp.IdRolPermiso) as TotalPermisos
FROM Roles r
LEFT JOIN RolPermisos rp ON r.IdRol = rp.IdRol AND rp.Activo = 1
GROUP BY r.Nombre

-- Ver qué permisos tiene un usuario específico
SELECT DISTINCT rp.CodigoPermiso
FROM Recursos r
INNER JOIN RolPermisos rp ON r.IdRol = rp.IdRol
WHERE r.CorreoElectronico = 'admin@censys.com' AND rp.Activo = 1
```

---

## ?? **PRÓXIMOS PASOS RECOMENDADOS**

1. **Inmediato:**
   - ? Ejecutar el script `06_CrearTablaRolPermisos.sql`
   - ? Probar el sistema con diferentes roles
   - ? Acceder a `/permisos` como administrador

2. **Corto plazo:**
   - Crear más usuarios de prueba con diferentes roles
   - Personalizar permisos según necesidades específicas
   - Documentar permisos empresariales específicos

3. **Mediano plazo:**
   - Implementar interfaz gráfica para gestión de permisos
   - Agregar auditoría de cambios de permisos
   - Implementar permisos por usuario individual (no solo por rol)

---

## ?? **CONTACTO Y SOPORTE**

Si necesitas ayuda con:
- ? Configuración de permisos específicos
- ? Creación de nuevos módulos
- ? Problemas de acceso
- ? Personalizaciones del sistema

Solo pregúntame y te ayudo con consultas SQL específicas o modificaciones del código.

---

## ? **CHECKLIST DE IMPLEMENTACIÓN**

- [x] Modelo `RolPermiso` creado
- [x] Base de datos actualizada
- [x] Servicio de autenticación modificado
- [x] Sistema de fallback implementado
- [x] Interfaz web de administración creada
- [x] Menú actualizado con permisos
- [x] Scripts de configuración listos
- [x] Documentación completa
- [ ] **Script SQL ejecutado** ? (PENDIENTE)
- [ ] **Pruebas con usuarios reales** (RECOMENDADO)

**?? El sistema está listo para usar. Solo falta ejecutar el script SQL y comenzar a gestionar permisos.**