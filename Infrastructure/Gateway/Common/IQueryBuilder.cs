using System.Collections.Generic;
using Core.Model.Parameters;
using Core.Reference;

namespace Core.Model
{
	public interface IQueryBuilder
	{
		IQueryBuilder SetQueryArguments(FilterGroup group);

		IQueryBuilder SetQueryArguments(List<FilterGroup> groups);

		IQueryBuilder SetQueryArguments(List<FilterParameter> parameters, FilterProperty property,
										ExpressionType expressionType = ExpressionType.Or);

		IQueryBuilder SetQueryArguments(FilterProperty property, ExpressionType expressionType = ExpressionType.In,
										params FilterParameter[] parameters);


		RequestQuery Build();

		IQueryBuilder Reset();
	}
}