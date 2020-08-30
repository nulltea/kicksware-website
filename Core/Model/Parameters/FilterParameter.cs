using System;
using System.Text.RegularExpressions;
using Core.Extension;
using Core.Reference;
// using Microsoft.AspNetCore.Html;
// using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Model.Parameters
{
	public class FilterParameter
	{
		public string RenderId => $"filter-control-{new Regex("[\\n\\t;,.\\s()\\/]").Replace((Caption ?? Convert.ToString(Value))!, "_").ToLower()}";

		public string Caption { get; }

		public string Description { get; }

		public object Value
		{
			get => _value;
			set => _value = ImmutableValue ? _value : value;
		}
		private object _value;

		public bool Checked { get; set; }

		public object SourceValue { get; set; }

		public ExpressionType ExpressionType { get; }

		public bool ImmutableValue { get; set; } = true;

		public FilterParameter(string caption, object value, ExpressionType expressionType = ExpressionType.Equal, string description=default)
		{
			Caption = caption;
			_value = value;
			ExpressionType = expressionType;
			Description = description;
		}

		public FilterParameter(object value, ExpressionType expressionType = ExpressionType.Equal)
		{
			_value = value;
			ExpressionType = expressionType;
			Checked = true;
		}

		public T GetSourceValue<T>() => (T)SourceValue;
	}
}