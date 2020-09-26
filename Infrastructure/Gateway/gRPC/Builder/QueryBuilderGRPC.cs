using System;
using System.Collections.Generic;
using System.Linq;
using Core.Attributes;
using Core.Extension;
using Core.Model;
using Core.Model.Parameters;
using Core.Reference;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC.Builder
{
	public class QueryBuilderGRPC : IQueryBuilder
	{
		public IQueryBuilder SetQueryArguments(FilterGroup group)
		{
			_queryGroups = new List<FilterGroup> {group};
			return this;
		}

		public IQueryBuilder SetQueryArguments(List<FilterGroup> groups)
		{
			_queryGroups = groups;
			return this;
		}

		private List<FilterGroup> _queryGroups;

		public IQueryBuilder SetQueryArguments(List<FilterParameter> parameters, FilterProperty property, ExpressionType expressionType = ExpressionType.Or)
		{
			(_queryGroups ??= new List<FilterGroup>())
				.Add(new FilterGroup(property, expressionType).AssignParameters(parameters.ToArray()));
			return this;
		}

		public IQueryBuilder SetQueryArguments(FilterProperty property, ExpressionType expressionType = ExpressionType.In, params FilterParameter[] parameters)
		{
			(_queryGroups ??= new List<FilterGroup>())
				.Add(new FilterGroup(property, expressionType).AssignParameters(parameters.ToArray()));
			return this;
		}

		public RequestQuery Build()
		{
			var resultQuery = new Struct();
			foreach (var filterGroup in _queryGroups)
			{
				if (filterGroup.Property.IsForeignEntity) //handle subservice entity query
				{
					var subgroup = new FilterGroup(filterGroup.Caption, filterGroup.Property.FieldName, filterGroup.ExpressionType)
							.AssignParameters(filterGroup.CheckedParameters.ToArray());
					if (!BuildForGroup(subgroup, out var subqueryPair)) continue;
					var subquery = new Struct
					{
						Fields = { [subqueryPair.property] = subqueryPair.query }
					};
					var subservice = $"*/{filterGroup.Property.ForeignResource}";
					if (!resultQuery.Fields.TryAdd(subservice, subquery.AsValue()))
					{
						if (resultQuery.Fields[subservice].KindCase == Value.KindOneofCase.StructValue)
						{
							var existedQuery = resultQuery.Fields[subservice].StructValue;
							existedQuery.Fields.TryAdd(subqueryPair.property, subquery.Fields[subqueryPair.property]);
							resultQuery.Fields[subservice] = existedQuery.AsValue();
						}
					}
					continue;
				}

				if (!BuildForGroup(filterGroup, out var queryPair)) continue;

				resultQuery.Fields.TryAdd(queryPair.property, queryPair.query);
			}
			return RequestQuery.FromValue(resultQuery);
		}

		private bool BuildForGroup(FilterGroup filterGroup, out (string property, Value query) resultQuery)
		{
			resultQuery = default;
			var queryProperty = FormatProperty(filterGroup.Property.FieldName);
			var checkedParams = filterGroup.CheckedParameters;
			if (!checkedParams.Any()) return false;

			var multiply = checkedParams.Count > 1;
			if (multiply)
			{
				var groupExpression = filterGroup.ExpressionType.GetEnumAttribute<QueryExpressionAttribute>();
				switch (groupExpression.Target)
				{
					case ExpressionTarget.Group:
					case ExpressionTarget.Both:
					{
						var listQuery = Value.ForStruct(new Struct
						{
							Fields =
							{
								[groupExpression.OperatorSyntax] = Value.ForList(
									checkedParams.Select(
											param => FormatQueryValue(groupExpression, param.Value)
										).ToArray()
									)
							}
						});
						resultQuery = (queryProperty, listQuery);
						break;
					}
					case ExpressionTarget.Each:
					{
						var eachParamQuery = new List<Value>();
						foreach (var param in checkedParams)
						{
							if (param.ExpressionType == ExpressionType.Equal)
							{
								eachParamQuery.Add(new Struct
								{
									Fields = { [queryProperty] = param.Value.AsValue() }
								}.AsValue());
							}
							else
							{
								var nodeExpression = param.ExpressionType.GetEnumAttribute<QueryExpressionAttribute>();
								var operatorCondition = new Struct
								{
									Fields = { [nodeExpression.OperatorSyntax] = FormatQueryValue(nodeExpression, param.Value) }
								};
								eachParamQuery.Add(new Struct
								{
									Fields = {[queryProperty] = operatorCondition.AsValue()}
								}.AsValue());
							}
						}
						resultQuery = (groupExpression.OperatorSyntax, eachParamQuery.AsValue());
						break;
					}
					case ExpressionTarget.Node:
					{
						var eachParamQuery = new Struct();
						foreach (var param in checkedParams)
						{
							var nodeExpression = param.ExpressionType.GetEnumAttribute<QueryExpressionAttribute>();
							eachParamQuery.Fields.Add(nodeExpression.OperatorSyntax, FormatQueryValue(nodeExpression, param.Value));
						}
						resultQuery = (queryProperty, eachParamQuery.AsValue());
						break;
					}
					default:
						throw new ArgumentOutOfRangeException(nameof(groupExpression.Target));
				}

				return true;
			}

			var singleNode = checkedParams.First();
			if (singleNode.ExpressionType != ExpressionType.Equal)
			{
				var nodeOperator = singleNode.ExpressionType.GetEnumAttribute<QueryExpressionAttribute>();
				var operatorCondition = new Struct
				{
					Fields = { [nodeOperator.OperatorSyntax] = FormatQueryValue(nodeOperator, singleNode.Value) }
				};
				resultQuery = (queryProperty, operatorCondition.AsValue());
				return true;
			}
			resultQuery = (queryProperty, singleNode.Value.AsValue());
			return true;
		}

		private static Value FormatQueryValue(QueryExpressionAttribute expAttr, object value)
		{
			value = !string.IsNullOrEmpty(expAttr.ValueWrapperFormat)
				? string.Format(expAttr.ValueWrapperFormat, value)
				: value;
			if (expAttr.OperatorSyntax.Contains("regex"))
			{
				return new Struct
				{
					Fields =
					{
						["pattern"] = value.AsValue(),
						["options"] = Value.ForString(string.Empty)
					}
				}.AsValue();
			}
			return value.AsValue();
		}

		private static string FormatProperty(string property) => property.ToLower();

		public IQueryBuilder Reset()
		{
			_queryGroups = null;
			return this;
		}
	}
}