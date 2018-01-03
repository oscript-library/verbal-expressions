# Как создать компонент для Односкрипта

1.  Создаём новый проект-библиотеку
2.  Подключаем NuGet пакет "OneScript runtime core" и "OneScript Main Client Libraries".
    Первый подключать обязательно, второй подключается для возможности использования
    встроенных типов Массив, ТаблицаЗначений и т.д.
3.  Подключаем модули:
        
        using ScriptEngine.Machine.Contexts;
        using ScriptEngine.Machine;
        using ScriptEngine.HostedScript.Library; // только если подключили OneScript Main Client Libraries
        
4.  Ставим на класс пометку `[ContextClass("МойКласс", "MyClass")]` и добавляем классу наследование от `AutoContext<MyClass>`
5.  Прописываем в класс конструктор

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new MyClass();
        }

6.  После чего в коде можно использовать вызов вида

        ПодключитьВнешнююКомпоненту("oscript-component/bin/Debug/oscript-component.dll");
        ОбъектМоегоКласса = Новый МойКласс;


