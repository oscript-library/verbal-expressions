using System;
using System.Reflection;
using System.Text.RegularExpressions;
using CSharpVerbalExpressions;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace OnescriptVerbalExpressions
{
	/// <summary>
	/// Класс предоставляет объектную модель для построения регулярных выражений.
	/// </summary>
	[ContextClass("ВербальноеВыражение", "VerbalExpression")]
	public class VerbalExpression : AutoContext<VerbalExpression>
	{
		private readonly VerbalExpressions _verbalExpression = new VerbalExpressions();
		private RegexOptions _modifiers = RegexOptions.IgnoreCase | RegexOptions.Multiline;
		
		#region Terminals

		/// <summary>
		/// Преобразует объект в РегулярноеВыражение.
		/// </summary>
		/// <returns>Объект РегулярноеВыражение с заполненным шаблоном поиска и флагами</returns>
		/// <exception cref="TypeLoadException"></exception>
		[ContextMethod("ВРегулярноеВыражение")]
		public IRuntimeContextInstance ToRegex()
		{
			var regexString = ToStringImpl();
			var oscriptRegExpType = Assembly.Load("ScriptEngine.HostedScript")
				.GetType("ScriptEngine.HostedScript.Library.Regex.RegExpImpl");
			var methodInfo = oscriptRegExpType.GetMethod("Constructor");
			if (methodInfo == null)
			{
				throw new TypeLoadException();
			}

			var oscriptRegExpImpl = (IRuntimeContextInstance) methodInfo.Invoke(
				null, 
				new object[] {ValueFactory.Create(regexString)}
			);

			var propertyMultiline = oscriptRegExpImpl.FindProperty("Multiline");
			var valueMultiline = _modifiers.HasFlag(RegexOptions.Multiline);
			oscriptRegExpImpl.SetPropValue(propertyMultiline, ValueFactory.Create(valueMultiline));
			
			var propertyIgnoreCase = oscriptRegExpImpl.FindProperty("IgnoreCase");
			var valueIgnoreCase = _modifiers.HasFlag(RegexOptions.IgnoreCase);
			oscriptRegExpImpl.SetPropValue(propertyIgnoreCase, ValueFactory.Create(valueIgnoreCase));
			
			return oscriptRegExpImpl;

		}
		
		/// <summary>
		/// Преобразует объект в строку.
		/// </summary>
		/// <returns>Текст регулярного выражения в виде строки</returns>
		[ContextMethod("ВСтроку", "ToString")]
		public string ToStringImpl()
		{
			return _verbalExpression.ToString();
		}
		
		#endregion

		#region Modifiers

		/// <summary>
		/// Ищет в строке определенное значение. То же самое, что и `Затем()`.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Найти")]
		public VerbalExpression Find(string value, bool sanitize = true)
		{
			return Then(value, sanitize);
		}
		
		/// <summary>
		/// Ищет в строке определенное значение. То же самое, что и `Найти()`.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Затем")]
		public VerbalExpression Then(string value, bool sanitize = true)
		{
			_verbalExpression.Then(value, sanitize);
			return this;
		}
		
		/// <summary>
		/// Добавление условия ИЛИ в выражение.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Либо")]
		public VerbalExpression Or(string value, bool sanitize = true)
		{
			_verbalExpression.Or(value, sanitize);
			return this;
		}
		
		/// <summary>
		/// Поиск "чего угодно", в том числе отсутствия символов.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЧтоУгодно")]
		public VerbalExpression Anything()
		{
			_verbalExpression.Anything();
			return this;
		}
		
		/// <summary>
		/// Поиск "чего угодно, кроме" указанных символов, в том числе их отсутствие.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЧтоУгодноНоНе")]
		public VerbalExpression AnythingBut(string value, bool sanitize = true)
		{
			_verbalExpression.AnythingBut(value, sanitize);
			return this;
		}
		
		/// <summary>
		/// Поиск "чего-нибудь" - любого символа хотя бы один раз.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЧтоНибудь")]
		public VerbalExpression Something()
		{
			_verbalExpression.Something();
			return this;
		}
		
		/// <summary>
		/// Поиск "чего-нибудь, кроме" указанных символов хотя бы один раз.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЧтоНибудьНоНе")]
		public VerbalExpression SomethingBut(string value, bool sanitize = true)
		{
			_verbalExpression.SomethingBut(value, sanitize);
			return this;
		}
		
		/// <summary>
		/// Поиск необязательного значения. 
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("МожетБыть")]
		public VerbalExpression Maybe(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.Maybe(value);
			return this;
		}
				
		/// <summary>
		/// Добавляет в поиск префикс "начала строки".
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("НачалоСтроки")]
		public VerbalExpression StartOfLine()
		{
			_verbalExpression.StartOfLine();
			return this;
		}
		
		/// <summary>
		/// Добавляет в поиск суффикс "конца строки".
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("КонецСтроки")]
		public VerbalExpression EndOfLine()
		{
			_verbalExpression.EndOfLine();
			return this;
		}
		
		#endregion

		#region Special characters and groups

		/// <summary>
		/// Перевод строки.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ПереводСтроки")]
		public VerbalExpression LineBreak()
		{
			_verbalExpression.LineBreak();
			return this;
		}
		
		/// <summary>
		/// Перевод строки.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ПС")]
		public VerbalExpression LF()
		{
			_verbalExpression.Br();
			return this;
		}
		
		/// <summary>
		/// Символ табуляции.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Отступ")]
		public VerbalExpression Tab()
		{
			_verbalExpression.Tab();
			return this;
		}
		
		/// <summary>
		/// Буквенно-цифровая последовательность символов.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Слово")]
		public VerbalExpression Word()
		{
			_verbalExpression.Word();
			return this;
		}
		
		/// <summary>
		/// Любое число.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Число")]
		public VerbalExpression Digit()
		{
			_verbalExpression.Add("\\d", false);
			return this;
		}
		
		/// <summary>
		/// Любой пробельный символ.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ПробельныйСимвол")]
		public VerbalExpression Whitespace()
		{
			_verbalExpression.Add("\\s", false);
			return this;
		}
		
		/// <summary>
		/// Диапазон символов.
		/// </summary>
		/// <param name="from">Нижняя граница диапазона</param>
		/// <param name="to">Верхняя граница диапазона</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Диапазон")]
		public VerbalExpression Range(string from, string to)
		{
			_verbalExpression.Range(from, to);
			return this;
		}
			
		/// <summary>
		/// Любой из указанных символов. То же самое, что и `ЛюбойИз()`
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Любой")]
		public VerbalExpression Any(string value)
		{
			_verbalExpression.Any(value);
			return this;
		}
		
		/// <summary>
		/// Любой из указанных символов. То же самое, что и `Любой()`
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЛюбойИз")]
		public VerbalExpression AnyOf(string value)
		{
			_verbalExpression.AnyOf(value);
			return this;
		}
		
		/// <summary>
		/// Поиск "одного или более".
		/// Если передан параметр `value`, то мультипликатор применяется к нему.
		/// Если параметр `value` не передан, то мультипликатор применяется к предыдущей группе регулярного выражения.
		/// </summary>
		/// <param name="value">Искомое значение</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ОдинИлиБольше")]
		public VerbalExpression OneOrMore(string value = "", bool sanitize = true)
		{
			if (value == null)
			{
				_verbalExpression.Add("+", false);
			}
			else
			{
				_verbalExpression.Multiple(value, sanitize);
			}
			return this;
		}
		
		/// <summary>
		/// Начать захват группы.
		/// </summary>
		/// <param name="groupName">Имя группы</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("НачатьЗахват")]
		public VerbalExpression BeginCapture(string groupName = "")
		{
			if (groupName == null)
			{
				_verbalExpression.BeginCapture();
			}
			else
			{
				_verbalExpression.BeginCapture(groupName);
			}
			return this;
		}
		
		/// <summary>
		/// Закончить захват группы.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ЗакончитьЗахват")]
		public VerbalExpression EndCapture()
		{
			_verbalExpression.EndCapture();
			return this;
		}
		
		/// <summary>
		/// Повторить предыдущую группу поиска n раз.
		/// </summary>
		/// <param name="n">Число повторений</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ПовторитьПредыдущее")]
		public VerbalExpression RepeatPrevious(int n)
		{
			_verbalExpression.RepeatPrevious(n);
			return this;
		}
		
		/// <summary>
		/// Повторить предыдущую группу поиска от n до m раз.
		/// </summary>
		/// <param name="n">Нижняя гранница</param>
		/// <param name="m">Верхняя граница</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ПовторитьПредыдущееОтИДо")]
		public VerbalExpression RepeatPreviousFromAndTo(int n, int m)
		{
			_verbalExpression.RepeatPrevious(n, m);
			return this;
		}

		#endregion

		#region RegExp options

		/// <summary>
		/// Включить поиск без учета регистра символов.
		/// </summary>
		/// <param name="enable">Флаг включения/выключения поиска</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("СЛюбымРегистром")]
		public VerbalExpression WithAnyCase(bool enable)
		{
			if (enable)
			{
				AddModifier("i");
			}
			else
			{
				RemoveModifier("i");
			}
			_verbalExpression.WithAnyCase(enable);
			return this;
		}
		
		/// <summary>
		/// Включить поиск только по одной строке. 
		/// </summary>
		/// <param name="enable">Флаг включения/выключения поиска</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ОднострочныйПоиск")]
		public VerbalExpression SearchOneLine(bool enable)
		{
			if (enable)
			{
				RemoveModifier("m");
			}
			else
			{
				AddModifier("m");
			}
			_verbalExpression.UseOneLineSearchOption(enable);
			return this;
		}
		
		/// <summary>
		/// Добавление модификатора регулярного выражения.
		/// Допустимые значения: i, m.
		/// </summary>
		/// <param name="modifier">Устанавливаемый модификатор</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("ДобавитьМодификатор")]
		public VerbalExpression AddModifier(string modifier)
		{
			switch (modifier)
			{
				case "i":
					_modifiers |= RegexOptions.IgnoreCase;
					break;
				case "m":
					_modifiers |= RegexOptions.Multiline;
					break;
				case "s":
					_modifiers |= RegexOptions.Singleline;
					break;
				case "x":
					_modifiers |= RegexOptions.IgnorePatternWhitespace;
					break;
			}

			_verbalExpression.AddModifier(modifier.ToCharArray(0, 1)[0]);
			return this;
		}

		/// <summary>
		/// Удаление модификатора регулярного выражения.
		/// Допустимые значения: i, m.
		/// </summary>
		/// <param name="modifier">Удаляемый модификатор</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("УдалитьМодификатор")]
		public VerbalExpression RemoveModifier(string modifier)
		{
			switch (modifier)
			{
				case "i":
					_modifiers &= ~RegexOptions.IgnoreCase;
					break;
				case "m":
					_modifiers &= ~RegexOptions.Multiline;
					break;
				case "s":
					_modifiers &= ~RegexOptions.Singleline;
					break;
				case "x":
					_modifiers &= ~RegexOptions.IgnorePatternWhitespace;
					break;
			}

			_verbalExpression.RemoveModifier(modifier.ToCharArray(0, 1)[0]);
			return this;
		}
		#endregion
		
		#region Other

		/// <summary>
		/// Добавление произвольного текста в регулярное выражение.
		/// </summary>
		/// <param name="value">Добавляемый текст</param>
		/// <param name="sanitize">Экранировать переданное значение</param>
		/// <returns>ВербальноеВыражение</returns>
		[ContextMethod("Добавить")]
		public VerbalExpression Add(string value, bool sanitize = true)
		{
			_verbalExpression.Add(value, sanitize);
			return this;
		}
		
		/// <summary>
		/// Экранирование управляющих символов регулярного выражения в переданной строке.
		/// </summary>
		/// <param name="value">Экранируемая строка</param>
		/// <returns>Экранированная строка</returns>
		[ContextMethod("Экранировать")]
		public string Sanitize(string value)
		{
			return _verbalExpression.Sanitize(value);
		}

		#endregion
		
		/// <summary>
		/// Конструктор Вербального выражения.
		/// </summary>
		/// <returns>ВербальноеВыражение</returns>
		[ScriptConstructor]
		public static VerbalExpression Constructor()
		{
			return new VerbalExpression();
		}
	}
}

