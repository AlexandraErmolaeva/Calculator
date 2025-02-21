# Calculator Project

## Этот проект включает в себя frontend (Vue.js) и backend (ASP.NET Core). Перед запуском необходимо выполнить несколько шагов по установке зависимостей и сборке проекта.

# Установка и запуск
```bash
1. Клонирование репозитория
git clone https://github.com/your-repository/calculator.git
cd calculator

2. Установка зависимостей для frontend
cd ui/calculator-ui
npm install

3. Сборка frontend
npm run build

После сборки файлы frontend автоматически попадут в wwwroot в backend.

4. Запуск backend сервера
Перейти в корень backend и запустить сервер:
cd ../../src/Calculator.Api
dotnet run

После успешного запуска backend API будет доступно по адресу:
https://localhost:7208
