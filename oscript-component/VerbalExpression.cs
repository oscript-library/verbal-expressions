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
		public IRuntimeContextInstance Find(string value)
		{
			_verbalExpression.Find(value);
			return this;
		}
		
		[ContextMethod("Затем")]
		public IRuntimeContextInstance Then(string value)
		{
			_verbalExpression.Then(value);
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

