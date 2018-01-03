using System;
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
		
		#region Terminals

		[ContextMethod("ВРегулярноеВыражение")]
		public Regex ToRegex()
		{
			throw new NotImplementedException();
		}
		
		[ContextMethod("ВСтроку")]
		public string ToString()
		{
			return _verbalExpression.ToString();
		}
		
		#endregion

		#region Modifiers

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
		
		[ContextMethod("МожетБыть")]
		public IRuntimeContextInstance Maybe(string value)
		{
			// TODO: sanitaze?
			_verbalExpression.Maybe(value);
			return this;
		}
		
		#endregion

		#region Special characters and groups

		[ContextMethod("Слово")]
		public IRuntimeContextInstance Word()
		{
			_verbalExpression.Word();
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

