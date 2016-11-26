Развертывание версии на локальном компьютере:

1) Проект не содержит бинарных и лишних dll-файлов. 
Для успешной сборки требуется включить Nuget Package restore
https://docs.nuget.org/ndocs/consume-packages/package-restore

2) Требуется развернуть базы данных из 
Database\WorkFlowBilling.Logs
Database\WorkFlowBilling.Service

А так же поменять connectionStrings в web.config'e;
------------------------------------------------------------------------------------------------------------------
Запуск тестов

1) Для тестов используется NUnit и для запуска тестов из студии требуется установить NUnitAdapter for Visual Studio
2) Функциональные тесты требуют наличия тестовых баз данных. 
Их необходимо развернуть из 
Database\WorkFlowBilling.Logs
Database\WorkFlowBilling.Service 
под другими именами, например WorkFlowBilling.LogsTests / \WorkFlowBilling.ServiceTests

А так же поменять connectionStrings в app.config'e тестовых проектов;
