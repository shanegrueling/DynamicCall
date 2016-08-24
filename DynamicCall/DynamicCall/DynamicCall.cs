using System.Collections.Generic;
using System.Reflection;
using System;
using DynamicCall.Compiler;
using DynamicCall.Serializer;

namespace DynamicCall
{
    public class DynamicCall
    {
        private Dictionary<string, Func<ISerializer, object>> _dict;

        public DynamicCall()
        {
            _dict = new Dictionary<string, Func<ISerializer, object>>();
        }

        /// <summary>
        /// Adds a method. It will be callable by the name of the type of <paramref name="obj"/> and it's own name.
        /// Either can be substituted through <paramref name="interfaceName"/> and <paramref name="methodName"/>.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="obj"/></typeparam>
        /// <param name="obj">The object which can be called later.</param>
        /// <param name="method">The method which should later be called.</param>
        /// <param name="interfaceName">The optional interfaceName if you don't want to use the name of the class.</param>
        /// <param name="methodName">The optional methodName if you don't want to use the name of the method.</param>
        public void Add<T>(T obj, MethodInfo method, string interfaceName = "", string methodName = "")
        {
            var compiler = new ExpressionCompiler<Func<ISerializer, object>>();

            var serializer = compiler.Parameter<ISerializer>("serializer");

            var target = compiler.Constant(obj);
            var toObject = typeof(ISerializer).GetMethod("Get");
            var arguments = new List<int>();
            foreach (var parameter in method.GetParameters())
            {
                var correctToObject = toObject.MakeGenericMethod(parameter.ParameterType);
                var position = compiler.Constant(parameter.Position);
                arguments.Add(compiler.Call(serializer, correctToObject, position));
            }

            var call = compiler.Call(target, method, arguments.ToArray());

            compiler.Emit(call);

            if (method.ReturnType.IsValueType)
            {
                var convert = compiler.Convert(call, typeof(object));

                compiler.Emit(convert);
            }

            if(string.IsNullOrWhiteSpace(interfaceName))
            {
                interfaceName = obj.GetType().Name;
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                methodName = method.Name;
            }

            _dict.Add(interfaceName + "::" + methodName, compiler.Compile());
        }

        /// <summary>
        /// Calls the method that was registered as <paramref name="i"/>.<paramref name="m"/>.
        /// </summary>
        /// <param name="i">The interface to call</param>
        /// <param name="m">The method to call</param>
        /// <param name="parameters">The parameter array form which, which the function should be called.</param>
        /// <returns>The return value of the function as object.</returns>
        public object Call(string i, string m, ISerializer parameters)
        {
            var lambda = _dict[$"{i}::{m}"];
            if (lambda == null) throw new ArgumentException($"Know method registered. Method: {i}::{m}");

            return lambda(parameters);
        }
    }
}
