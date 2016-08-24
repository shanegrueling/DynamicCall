using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicCall.Compiler
{
    public class ExpressionCompiler<TDel> : ICompiler<TDel>
    {
        private readonly List<Expression> _content = new List<Expression>();
        private readonly List<Expression> _expressions = new List<Expression>();
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<ParameterExpression> _variables = new List<ParameterExpression>();

        public int Parameter<T>(string name)
        {
            var exp = Expression.Parameter(typeof(T), name);
            _parameters.Add(exp);
            _expressions.Add(exp);
            return _expressions.Count - 1;
        }

        public int Variable<T>(string name)
        {
            var exp = Expression.Variable(typeof(T), name);
            _variables.Add(exp);
            _expressions.Add(exp);

            return _expressions.Count - 1;
        }

        public int Constant<T>(T value)
        {
            var objExp = Expression.Constant(value, typeof(T));
            _expressions.Add(objExp);

            return _expressions.Count - 1;
        }

        public int ArrayAccess(int array, int index)
        {
            var exp = Expression.ArrayAccess(_expressions[array], _expressions[index]);
            _expressions.Add(exp);

            return _expressions.Count - 1;
        }

        public int Property(int target, string propertyName, params int[] arguments)
        {
            var targetExp = _expressions[target];
            var argumentsExp = arguments.Select(n => _expressions[n]).ToArray();
            var exp = Expression.Property(targetExp, propertyName, argumentsExp);
            _expressions.Add(exp);

            return _expressions.Count - 1;
        }

        public int Call(int target, MethodInfo method, params int[] arguments)
        {
            var targetExp = _expressions[target];
            var argumentsExp = arguments.Select(n => _expressions[n]).ToArray();
            var call = Expression.Call(targetExp, method, argumentsExp);
            _expressions.Add(call);

            return _expressions.Count - 1;
        }

        public int Convert(int value, Type type)
        {
            var valueExp = _expressions[value];
            var conv = (Expression)Expression.Convert(valueExp, type);
            _expressions.Add(conv);
            return _expressions.Count - 1;
        }

        public void Emit(int exp)
        {
            _content.Add(_expressions[exp]);
        }

        public Expression ToBlock()
        {
            if (!_content.Any())
            {
                _content.Add(Expression.Empty());
            }

            return Expression.Block(_variables.ToArray(), _content);
        }

        public TDel Compile()
        {
            var body = ToBlock();
            var parameters = _parameters.ToArray();
            return Expression.Lambda<TDel>(body, parameters).Compile();
        }
    }
}
