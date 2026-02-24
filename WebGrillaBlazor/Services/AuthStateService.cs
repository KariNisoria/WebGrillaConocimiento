using Microsoft.JSInterop;
using System.Text.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.Services
{
    public class AuthStateService
    {
        private readonly IJSRuntime _jsRuntime;
        private RecursoSessionDTO? _currentUser;
        
        public event Action<RecursoSessionDTO?>? OnUserChanged;
        public event Action? OnLogout;

        public AuthStateService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Usuario actualmente autenticado
        /// </summary>
        public RecursoSessionDTO? CurrentUser 
        { 
            get => _currentUser; 
            private set
            {
                _currentUser = value;
                OnUserChanged?.Invoke(_currentUser);
            }
        }

        /// <summary>
        /// Verificar si hay un usuario autenticado
        /// </summary>
        public bool IsAuthenticated => CurrentUser != null;

        /// <summary>
        /// Establecer usuario autenticado y persistir en localStorage
        /// </summary>
        public async Task SetCurrentUserAsync(RecursoSessionDTO user)
        {
            CurrentUser = user;
            await SaveUserToLocalStorageAsync(user);
            
            Console.WriteLine($"? Usuario establecido: {user.NombreCompleto} ({user.NombreRol})");
            
            // Notificar cambio de usuario
            OnUserChanged?.Invoke(user);
        }

        /// <summary>
        /// Cerrar sesión y limpiar datos
        /// </summary>
        public async Task LogoutAsync()
        {
            Console.WriteLine("?? Cerrando sesión...");
            CurrentUser = null;
            await ClearUserFromLocalStorageAsync();
            
            // Notificar logout
            OnLogout?.Invoke();
            Console.WriteLine("? Sesión cerrada completamente");
        }

        /// <summary>
        /// Forzar limpieza completa de sesión (para debugging/testing)
        /// </summary>
        public async Task ForceLogoutAsync()
        {
            try
            {
                Console.WriteLine("?? FORZANDO logout completo...");
                
                // Verificar estado actual
                var currentLocalStorage = await GetLocalStorageContentAsync();
                Console.WriteLine($"LocalStorage antes de limpiar: {currentLocalStorage}");
                
                _currentUser = null; // Limpiar directamente sin eventos
                await ClearUserFromLocalStorageAsync();
                
                // Verificar que se limpió
                var afterClear = await GetLocalStorageContentAsync();
                Console.WriteLine($"LocalStorage después de limpiar: {afterClear}");
                
                Console.WriteLine("? Sesión forzada a logout - localStorage limpio");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error en ForceLogoutAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener contenido actual del localStorage para debugging
        /// </summary>
        public async Task<string> GetLocalStorageContentAsync()
        {
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "currentUser");
                return string.IsNullOrEmpty(userJson) ? "null" : userJson.Substring(0, Math.Min(100, userJson.Length));
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Cargar usuario desde localStorage al inicializar la aplicación
        /// MODIFICADO: Cargar datos si existen y son válidos, pero permitir limpieza forzada
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                Console.WriteLine("?? Inicializando AuthStateService...");
                
                // Verificar que JSRuntime esté disponible
                await Task.Delay(50); // Pequeño delay para asegurar que JSRuntime esté listo
                
                // Verificar estado del localStorage
                var localStorageContent = await GetLocalStorageContentAsync();
                Console.WriteLine($"?? Estado localStorage: {localStorageContent}");
                
                // Si hay datos en localStorage, intentar cargarlos
                if (!string.IsNullOrEmpty(localStorageContent) && localStorageContent != "null" && !localStorageContent.StartsWith("Error"))
                {
                    try
                    {
                        var userJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "currentUser");
                        
                        if (!string.IsNullOrEmpty(userJson))
                        {
                            var user = JsonSerializer.Deserialize<RecursoSessionDTO>(userJson);
                            if (user != null && !string.IsNullOrEmpty(user.NombreCompleto))
                            {
                                _currentUser = user; // Asignar directamente sin notificar eventos durante inicialización
                                Console.WriteLine($"?? Usuario cargado desde localStorage: {user.NombreCompleto} ({user.NombreRol})");
                                Console.WriteLine($"? AuthStateService inicializado - sesión restaurada");
                                return; // Terminar aquí si se cargó exitosamente
                            }
                            else
                            {
                                Console.WriteLine("?? Datos de usuario en localStorage inválidos");
                            }
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($"? Error deserializando usuario desde localStorage: {jsonEx.Message}");
                        // Limpiar localStorage corrupto
                        await ClearUserFromLocalStorageAsync();
                    }
                }
                
                // Si no hay datos válidos, asegurar estado limpio
                _currentUser = null;
                Console.WriteLine("? AuthStateService inicializado - sesión limpia (requiere login manual)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error al inicializar estado de auth: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Si hay error al cargar, limpiar localStorage
                try
                {
                    await ClearUserFromLocalStorageAsync();
                    _currentUser = null;
                }
                catch 
                {
                    // Ignorar errores al limpiar
                }
            }
        }

        /// <summary>
        /// Verificar si el usuario tiene un permiso específico
        /// </summary>
        public bool UserHasPermission(string permission)
        {
            return IsAuthenticated && CurrentUser!.TienePermiso(permission);
        }

        /// <summary>
        /// Verificar si el usuario puede acceder a un módulo con nivel de acceso específico
        /// </summary>
        public bool UserCanAccess(string module, string accessLevel = "READ")
        {
            if (!IsAuthenticated) return false;
            return CurrentUser!.TienePermiso($"{module}_{accessLevel}");
        }

        /// <summary>
        /// Obtener información del usuario para mostrar
        /// </summary>
        public string GetUserDisplayName()
        {
            return IsAuthenticated ? CurrentUser!.NombreCompleto : "No autenticado";
        }

        /// <summary>
        /// Obtener información completa del usuario
        /// </summary>
        public string GetUserInfo()
        {
            return IsAuthenticated ? CurrentUser!.InfoCompleta : "No autenticado";
        }

        /// <summary>
        /// Verificar si el usuario es administrador
        /// </summary>
        public bool IsAdmin()
        {
            if (!IsAuthenticated) return false;
            var rolName = CurrentUser!.NombreRol.ToLower();
            return rolName.Contains("admin") || rolName.Contains("administrador");
        }

        /// <summary>
        /// Verificar si el usuario es manager/gerente
        /// </summary>
        public bool IsManager()
        {
            if (!IsAuthenticated) return false;
            var rolName = CurrentUser!.NombreRol.ToLower();
            return rolName.Contains("manager") || rolName.Contains("gerente") || 
                   rolName.Contains("líder") || rolName.Contains("lider");
        }

        /// <summary>
        /// Guardar usuario en localStorage
        /// </summary>
        private async Task SaveUserToLocalStorageAsync(RecursoSessionDTO user)
        {
            try
            {
                var userJson = JsonSerializer.Serialize(user);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", userJson);
                Console.WriteLine($"?? Usuario guardado en localStorage: {user.NombreCompleto}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error guardando en localStorage: {ex.Message}");
            }
        }

        /// <summary>
        /// Limpiar usuario de localStorage
        /// </summary>
        private async Task ClearUserFromLocalStorageAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");
                Console.WriteLine("??? localStorage limpiado completamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error limpiando localStorage: {ex.Message}");
            }
        }
    }
}