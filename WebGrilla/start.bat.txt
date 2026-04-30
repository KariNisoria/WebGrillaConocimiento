@echo off
chcp 65001 >nul
color 0A
title WebGrilla - Inicio en Red Local

echo ╔════════════════════════════════════════════════════════════╗
echo ║        🚀 INICIANDO WEBGRILLA - ACCESO EN RED             ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

echo 📋 Paso 1/3: Iniciando Backend API...
echo ════════════════════════════════════════════════════════════
start "WebGrilla Backend API" cmd /k "cd /d %~dp0.. && dotnet run --project WebGrilla"

echo.
echo ⏱️  Esperando 8 segundos para que el backend inicie...
timeout /t 8 /nobreak >nul

echo.
echo 📋 Paso 2/3: Iniciando Frontend...
echo ════════════════════════════════════════════════════════════
start "WebGrilla Frontend" cmd /k "cd /d %~dp0..\WebGrillaBlazor && dotnet run"

echo.
echo ⏱️  Esperando 5 segundos para que el frontend inicie...
timeout /t 5 /nobreak >nul

echo.
echo ╔════════════════════════════════════════════════════════════╗
echo ║              ✅ AMBOS SERVIDORES INICIADOS                 ║
echo ╚════════════════════════════════════════════════════════════╝
echo.
echo 📡 BACKEND API:
echo    └─ Local:  http://localhost:8080
echo    └─ Red:    Verifica la IP en la ventana del backend
echo.
echo 🌐 FRONTEND:
echo    └─ Local:  http://localhost:5086
echo    └─ Red:    http://[TU_IP]:5086
echo.
echo 💡 INSTRUCCIONES PARA OTROS PCs:
echo    1. Anota la IP que aparece en la ventana del Backend
echo    2. Actualiza WebGrillaBlazor\wwwroot\appsettings.json
echo    3. Comparte la URL: http://[TU_IP]:5086
echo.
echo ════════════════════════════════════════════════════════════
echo Presiona cualquier tecla para cerrar esta ventana...
pause >nul