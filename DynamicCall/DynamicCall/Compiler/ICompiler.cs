using System;
using System.Reflection;

namespace DynamicCall.Compiler
{
    public interface ICompiler<TDel>
    {
        int Parameter<T>(string name);
        int Variable<T>(string name);
        int Constant<T>(T value);
        int ArrayAccess(int array, int index);
        int Property(int target, string propertyName, params int[] arguments);
        int Call(int target, MethodInfo method, params int[] arguments);
        int Convert(int value, Type type);
        void Emit(int exp);
        TDel Compile();
    }
}
