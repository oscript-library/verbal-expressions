
# ВербальноеВыражение / VerbalExpression

    
    
Класс предоставляет объектную модель для построения регулярных выражений.


  
  
## Методы
    
### ВРегулярноеВыражение / ToRegex()
    
    
    
Преобразует объект в РегулярноеВыражение.


  
  
#### Возвращаемое значение

Объект РегулярноеВыражение с заполненным шаблоном поиска и флагами

  
### ВСтроку / ToString()
    
    
    
Преобразует объект в строку.


  
  
#### Возвращаемое значение

Текст регулярного выражения в виде строки

  
### Найти / Find()
    
    
    
Ищет в строке определенное значение. То же самое, что и `Затем()`.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### Затем / Then()
    
    
    
Ищет в строке определенное значение. То же самое, что и `Найти()`.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### Либо / Or()
    
    
    
Добавление условия ИЛИ в выражение.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### ЧтоУгодно / Anything()
    
    
    
Поиск "чего угодно", в том числе отсутствия символов.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ЧтоУгодноНоНе / AnythingBut()
    
    
    
Поиск "чего угодно, кроме" указанных символов, в том числе их отсутствие.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### ЧтоНибудь / Something()
    
    
    
Поиск "чего-нибудь" - любого символа хотя бы один раз.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ЧтоНибудьНоНе / SomethingBut()
    
    
    
Поиск "чего-нибудь, кроме" указанных символов хотя бы один раз.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### МожетБыть / Maybe()
    
    
    
Поиск необязательного значения.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### НачалоСтроки / StartOfLine()
    
    
    
Добавляет в поиск префикс "начала строки".


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### КонецСтроки / EndOfLine()
    
    
    
Добавляет в поиск суффикс "конца строки".


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ПереводСтроки / LineBreak()
    
    
    
Перевод строки.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ПС / LF()
    
    
    
Перевод строки.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### Отступ / Tab()
    
    
    
Символ табуляции.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### Слово / Word()
    
    
    
Буквенно-цифровая последовательность символов.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### Число / Digit()
    
    
    
Любое число.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ПробельныйСимвол / Whitespace()
    
    
    
Любой пробельный символ.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### Диапазон / Range()
    
    
    
Диапазон символов.


  
  
#### Параметры

* *from*: Нижняя граница диапазона

* *to*: Верхняя граница диапазона

#### Возвращаемое значение

ВербальноеВыражение

  
### Любой / Any()
    
    
    
Любой из указанных символов. То же самое, что и `ЛюбойИз()`


  
  
#### Параметры

* *value*: Искомое значение

#### Возвращаемое значение

ВербальноеВыражение

  
### ЛюбойИз / AnyOf()
    
    
    
Любой из указанных символов. То же самое, что и `Любой()`


  
  
#### Параметры

* *value*: Искомое значение

#### Возвращаемое значение

ВербальноеВыражение

  
### ОдинИлиБольше / OneOrMore()
    
    
    
Поиск "одного или более".
Если передан параметр `value`, то мультипликатор применяется к нему.
Если параметр `value` не передан, то мультипликатор применяется к предыдущей группе регулярного выражения.


  
  
#### Параметры

* *value*: Искомое значение

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### НачатьЗахват / BeginCapture()
    
    
    
Начать захват группы.


  
  
#### Параметры

* *groupName*: Имя группы

#### Возвращаемое значение

ВербальноеВыражение

  
### ЗакончитьЗахват / EndCapture()
    
    
    
Закончить захват группы.


  
  
#### Возвращаемое значение

ВербальноеВыражение

  
### ПовторитьПредыдущее / RepeatPrevious()
    
    
    
Повторить предыдущую группу поиска n раз.


  
  
#### Параметры

* *n*: Число повторений

#### Возвращаемое значение

ВербальноеВыражение

  
### ПовторитьПредыдущееОтИДо / RepeatPreviousFromAndTo()
    
    
    
Повторить предыдущую группу поиска от n до m раз.


  
  
#### Параметры

* *n*: Нижняя гранница

* *m*: Верхняя граница

#### Возвращаемое значение

ВербальноеВыражение

  
### СЛюбымРегистром / WithAnyCase()
    
    
    
Включить поиск без учета регистра символов.


  
  
#### Параметры

* *enable*: Флаг включения/выключения поиска

#### Возвращаемое значение

ВербальноеВыражение

  
### ОднострочныйПоиск / SearchOneLine()
    
    
    
Включить поиск только по одной строке.


  
  
#### Параметры

* *enable*: Флаг включения/выключения поиска

#### Возвращаемое значение

ВербальноеВыражение

  
### ДобавитьМодификатор / AddModifier()
    
    
    
Добавление модификатора регулярного выражения.
Допустимые значения: i, m.


  
  
#### Параметры

* *modifier*: Устанавливаемый модификатор

#### Возвращаемое значение

ВербальноеВыражение

  
### УдалитьМодификатор / RemoveModifier()
    
    
    
Удаление модификатора регулярного выражения.
Допустимые значения: i, m.


  
  
#### Параметры

* *modifier*: Удаляемый модификатор

#### Возвращаемое значение

ВербальноеВыражение

  
### Добавить / Add()
    
    
    
Добавление произвольного текста в регулярное выражение.


  
  
#### Параметры

* *value*: Добавляемый текст

* *sanitize*: Экранировать переданное значение

#### Возвращаемое значение

ВербальноеВыражение

  
### Экранировать / Sanitize()
    
    
    
Экранирование управляющих символов регулярного выражения в переданной строке.


  
  
#### Параметры

* *value*: Экранируемая строка

#### Возвращаемое значение

Экранированная строка

  
## Конструкторы

  
### По умолчанию
    
    
Конструктор Вербального выражения.


  
  
