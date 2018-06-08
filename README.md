# OneScript Verbal Expressions

[![Build status](https://ci.appveyor.com/api/projects/status/y3j2uvwgthf4rmfu/branch/develop?svg=true)](https://ci.appveyor.com/project/nixel2007/verbal-expressions/branch/develop)
[![Build Status](https://travis-ci.org/oscript-library/verbal-expressions.svg?branch=develop)](https://travis-ci.org/oscript-library/verbal-expressions)

## Регулярные выражения - это просто!

verbal-expressions - это библиотека для OneScript, помогающая собирать сложные регулярные выражения.

## Установка

### С хаба пакетов

`opm install verbal-expressions`

### С релизов GitHub

1. Перейти на [страницу релизов](https://github.com/oscript-library/verbal-expessions/releases)
1. Скачать артефакт verbal-expressions-x.y.z.ospx
1. Установить с помощью opm: `opm install -f verbal-expressions-x.y.z.ospx`

### С AppVeyor

1. Перейти на страницу [последней сборки](https://ci.appveyor.com/project/nixel2007/oscript-verbal-expessions) или [истории сборок](https://ci.appveyor.com/project/nixel2007/oscript-verbal-expessions/history) и выбрать интересующую сборку
1. Перейти в раздел Artifacts
1. Скачать артефакт verbal-expressions-x.y.z.ospx
1. Установить с помощью opm: `opm install -f verbal-expressions-x.y.z.ospx`

## Использование

Несколько примеров использования Вербальных выражений:

### Проверка валидности URL

```bsl
#Использовать verbal-expressions

// Проверим корректность формирования URL

ВербальноеВыражение = Новый ВербальноеВыражение()
    .НачалоСтроки()
    .Затем("http")
    .МожетБыть("s")
    .Затем("://")
    .ЧтоНибудьНоНе(" ")
    .КонецСтроки();
    
ТекстРегулярногоВыражения = ВербальноеВыражение.ВСтроку();
Сообщить(ТекстРегулярногоВыражения); // ^(http)(s)?(://)([^ ]+)$

РегулярноеВыражение = ВербальноеВыражение.ВРегулярноеВыражение();
ПроверяемаяСтрока = "https://www.google.com";

Если РегулярноеВыражение.Совпадает(ПроверяемаяСтрока) Тогда
    Сообщить("URL корректен");
Иначе
    Сообщить("URL некорректен");
КонецЕсли;
```

### Вложенное "или"

```bsl
#Использовать verbal-expressions

// Проверим корректность формирования URL. Допустимые схемы - http[s] и ftp

ЭкранироватьПереданноеЗначение = Ложь;

ВербальноеВыражение = Новый ВербальноеВыражение()
    .НачалоСтроки()
    .Затем(
        Новый ВербальноеВыражение()
            .Найти("http")
            .МожетБыть("s")
            .Либо("ftp")
            .ВСтроку(),
        ЭкранироватьПереданноеЗначение
    )
    .Затем("://")
    .ЧтоНибудьНоНе(" ")
    .КонецСтроки();
    
ТекстРегулярногоВыражения = ВербальноеВыражение.ВСтроку();
Сообщить(ТекстРегулярногоВыражения); // ^(((http)(s)?)|(ftp))(://)([^ ]+)$
```

Больше примеров в [файле с приемочными тестами](https://github.com/oscript-library/verbal-expessions/blob/master/NUnitTests/Tests/external.os).

## Список методов

Список методов и их описание доступно в файле [docs/Reference.md](docs/Reference.md)

## ToDo

* Пробросить методы объекта РегулярноеВыражение в ВербальноеВыражение для упрощения использования
* Добавить новых ништяков

