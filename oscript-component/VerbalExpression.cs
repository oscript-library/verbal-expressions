using System;
using System.Reflection;
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
		public IRuntimeContextInstance Find(string value, bool sanitize = true)
		{
			return Then(value, sanitize);
		}
		
		[ContextMethod("Затем")]
		public IRuntimeContextInstance Then(string value, bool sanitize = true)
		{
			_verbalExpression.Then(value, sanitize);
			return this;
		}
		
		[ContextMethod("Либо")]
		public IRuntimeContextInstance Or(string value)
		{
			_verbalExpression.Or(value);
			return this;
		}
		
		[ContextMethod("ЧтоУгодно")]
		public IRuntimeContextInstance Anything()
		{
			_verbalExpression.Anything();
			return this;
		}
		
		[ContextMethod("ЧтоУгодноНоНе")]
		public IRuntimeContextInstance AnythingBut(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.AnythingBut(value);
			return this;
		}
		
		[ContextMethod("ЧтоНибудь")]
		public IRuntimeContextInstance Something()
		{
			_verbalExpression.Something();
			return this;
		}
		
		[ContextMethod("ЧтоНибудьНоНе")]
		public IRuntimeContextInstance SomethingBut(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.SomethingBut(value);
			return this;
		}
		
		[ContextMethod("МожетБыть")]
		public IRuntimeContextInstance Maybe(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.Maybe(value);
			return this;
		}
				
		[ContextMethod("НачалоСтроки")]
		public IRuntimeContextInstance StartOfLine()
		{
			// TODO: enable?
			_verbalExpression.StartOfLine();
			return this;
		}
		
		[ContextMethod("КонецСтроки")]
		public IRuntimeContextInstance EndOfLine()
		{
			// TODO: enable?
			_verbalExpression.EndOfLine();
			return this;
		}
		
		#endregion

		#region Special characters and groups

		[ContextMethod("ПереводСтроки")]
		public IRuntimeContextInstance LineBreak()
		{
			_verbalExpression.LineBreak();
			return this;
		}
		
		[ContextMethod("ПС")]
		public IRuntimeContextInstance LF()
		{
			_verbalExpression.Br();
			return this;
		}
		
		[ContextMethod("Отступ")]
		public IRuntimeContextInstance Tab()
		{
			_verbalExpression.Tab();
			return this;
		}
		
		[ContextMethod("Слово")]
		public IRuntimeContextInstance Word()
		{
			_verbalExpression.Word();
			return this;
		}
		
		[ContextMethod("Диапазон")]
		public IRuntimeContextInstance Range(string from, string to)
		{
			_verbalExpression.Range(from, to);
			return this;
		}
			
		[ContextMethod("Любой")]
		public IRuntimeContextInstance Any(string value)
		{
			_verbalExpression.Any(value);
			return this;
		}
		
		[ContextMethod("ЛюбойИз")]
		public IRuntimeContextInstance AnyOf(string value)
		{
			_verbalExpression.AnyOf(value);
			return this;
		}
		
		[ContextMethod("ОдинИлиБольше")]
		public IRuntimeContextInstance OneOrMore(string value)
		{
			_verbalExpression.Multiple(value);
			return this;
		}
		
		[ContextMethod("НачатьЗахват")]
		public IRuntimeContextInstance BeginCapture(string groupName = "")
		{
			if (groupName == "")
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
		public IRuntimeContextInstance EndCapture()
		{
			_verbalExpression.EndCapture();
			return this;
		}
		
		[ContextMethod("ПовторитьПредыдущее")]
		public IRuntimeContextInstance RepeatPrevious(int n)
		{
			_verbalExpression.RepeatPrevious(n);
			return this;
		}
		
		[ContextMethod("ПовторитьПредыдущееОтИДо")]
		public IRuntimeContextInstance RepeatPreviousFromAndTo(int n, int m)
		{
			_verbalExpression.RepeatPrevious(n, m);
			return this;
		}

		#endregion

		#region Other

		[ContextMethod("Добавить")]
		public IRuntimeContextInstance Add(string value)
		{
			_verbalExpression.Add(value);
			return this;
		}

		#endregion
		
		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor()
		{
			return new VerbalExpression();
		}
	}
}

