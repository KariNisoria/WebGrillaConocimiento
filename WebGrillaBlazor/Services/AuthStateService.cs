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
        /// Establecer usuario autenticado
        /// </summary>
        public async Task SetCurrentUserAsync(RecursoSessionDTO user)
        {
            CurrentUser = user;
            await SaveUserToLocalStorageAsync(user);
        }

        /// <summary>
        /// Cerrar sesión
        /// </summary>
        public async Task LogoutAsync()
        {
            CurrentUser = null;
            await ClearUserFromLocalStorageAsync();
            OnLogout?.Invoke();
        }

        /// <summary>
        /// Cargar usuario desde localStorage al inicializar la aplicación
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "currentUser");
                
                if (!string.IsNullOrEmpty(userJson))
                {
                    var user = JsonSerializer.Deserialize<RecursoSessionDTO>(userJson);
                    CurrentUser = user;
                }
            }
            catch (Exception)
            {
                // Si hay error al cargar, limpiar localStorage
                await ClearUserFromLocalStorageAsync();
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
            }
            catch (Exception)
            {
                // Manejar error silenciosamente
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
            }
            catch (Exception)
            {
                // Manejar error silenciosamente
            }
        }
    }
}