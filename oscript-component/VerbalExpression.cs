using System;
using System.Reflection;
using System.Text.RegularExpressions;
using CSharpVerbalExpressions;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace OnescriptVerbalExpressions
{
	/// <summary>
	/// Некоторый класс
	/// </summary>
	[ContextClass("ВербальноеВыражение", "VerbalExpression")]
	public class VerbalExpression : AutoContext<VerbalExpression>
	{
		private readonly VerbalExpressions _verbalExpression = new VerbalExpressions();
		private RegexOptions _modifiers = RegexOptions.IgnoreCase | RegexOptions.Multiline;
		
		#region Terminals

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
		
		[ContextMethod("ВСтроку", "ToString")]
		public string ToStringImpl()
		{
			return _verbalExpression.ToString();
		}
		
		#endregion

		#region Modifiers

		[ContextMethod("Найти")]
		public VerbalExpression Find(string value, bool sanitize = true)
		{
			return Then(value, sanitize);
		}
		
		[ContextMethod("Затем")]
		public VerbalExpression Then(string value, bool sanitize = true)
		{
			_verbalExpression.Then(value, sanitize);
			return this;
		}
		
		[ContextMethod("Либо")]
		public VerbalExpression Or(string value)
		{
			_verbalExpression.Or(value);
			return this;
		}
		
		[ContextMethod("ЧтоУгодно")]
		public VerbalExpression Anything()
		{
			_verbalExpression.Anything();
			return this;
		}
		
		[ContextMethod("ЧтоУгодноНоНе")]
		public VerbalExpression AnythingBut(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.AnythingBut(value);
			return this;
		}
		
		[ContextMethod("ЧтоНибудь")]
		public VerbalExpression Something()
		{
			_verbalExpression.Something();
			return this;
		}
		
		[ContextMethod("ЧтоНибудьНоНе")]
		public VerbalExpression SomethingBut(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.SomethingBut(value);
			return this;
		}
		
		[ContextMethod("МожетБыть")]
		public VerbalExpression Maybe(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.Maybe(value);
			return this;
		}
				
		[ContextMethod("НачалоСтроки")]
		public VerbalExpression StartOfLine()
		{
			// TODO: enable?
			_verbalExpression.StartOfLine();
			return this;
		}
		
		[ContextMethod("КонецСтроки")]
		public VerbalExpression EndOfLine()
		{
			// TODO: enable?
			_verbalExpression.EndOfLine();
			return this;
		}
		
		#endregion

		#region Special characters and groups

		[ContextMethod("ПереводСтроки")]
		public VerbalExpression LineBreak()
		{
			_verbalExpression.LineBreak();
			return this;
		}
		
		[ContextMethod("ПС")]
		public VerbalExpression LF()
		{
			_verbalExpression.Br();
			return this;
		}
		
		[ContextMethod("Отступ")]
		public VerbalExpression Tab()
		{
			_verbalExpression.Tab();
			return this;
		}
		
		[ContextMethod("Слово")]
		public VerbalExpression Word()
		{
			_verbalExpression.Word();
			return this;
		}
		
		[ContextMethod("Число")]
		public VerbalExpression Digit()
		{
			_verbalExpression.Add("\\d", false);
			return this;
		}
		
		[ContextMethod("ПробельныйСимвол")]
		public VerbalExpression Whitespace()
		{
			_verbalExpression.Add("\\s", false);
			return this;
		}
		
		[ContextMethod("Диапазон")]
		public VerbalExpression Range(string from, string to)
		{
			_verbalExpression.Range(from, to);
			return this;
		}
			
		[ContextMethod("Любой")]
		public VerbalExpression Any(string value)
		{
			_verbalExpression.Any(value);
			return this;
		}
		
		[ContextMethod("ЛюбойИз")]
		public VerbalExpression AnyOf(string value)
		{
			_verbalExpression.AnyOf(value);
			return this;
		}
		
		[ContextMethod("ОдинИлиБольше")]
		public VerbalExpression OneOrMore(string value = "")
		{
			if (value == null)
			{
				_verbalExpression.Add("+", false);
			}
			else
			{
				_verbalExpression.Multiple(value);
			}
			return this;
		}
		
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
		
		[ContextMethod("ЗакончитьЗахват")]
		public VerbalExpression EndCapture()
		{
			_verbalExpression.EndCapture();
			return this;
		}
		
		[ContextMethod("ПовторитьПредыдущее")]
		public VerbalExpression RepeatPrevious(int n)
		{
			_verbalExpression.RepeatPrevious(n);
			return this;
		}
		
		[ContextMethod("ПовторитьПредыдущееОтИДо")]
		public VerbalExpression RepeatPreviousFromAndTo(int n, int m)
		{
			_verbalExpression.RepeatPrevious(n, m);
			return this;
		}

		#endregion

		#region RegExp options

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

		[ContextMethod("Добавить")]
		public VerbalExpression Add(string value)
		{
			_verbalExpression.Add(value);
			return this;
		}
		
		[ContextMethod("Экранировать")]
		public string Sanitize(string value)
		{
			return _verbalExpression.Sanitize(value);
		}

		#endregion
		
		[ScriptConstructor]
		public static VerbalExpression Constructor()
		{
			return new VerbalExpression();
		}
	}
}

