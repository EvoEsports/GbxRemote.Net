using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GbxRemoteNet;

/// <summary>
///     A multicall builder.
/// </summary>
public class MultiCall
{
    /// <summary>
    ///     The calls for this multicall.
    /// </summary>
    public List<MethodInfo> MethodCalls = new();

    /// <summary>
    ///     Adds information about a method call to the list of calls
    ///     for this multicall.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    private MultiCall AddMethod(string method, params object[] args)
    {
        MethodCalls.Add(new MethodInfo(method, args));
        return this;
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn>(Func<Task<TReturn>> method)
    {
        return AddMethod(method.Method.Name);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1>(Func<T1, Task<TReturn>> method, T1 a1, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2>(Func<T1, T2, Task<TReturn>> method, T1 a1, T2 a2, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3>(Func<T1, T2, T3, Task<TReturn>> method, T1 a1, T2 a2, T3 a3, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task<TReturn>> method, T1 a1, T2 a2, T3 a3, T4 a4,
        params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, Task<TReturn>> method, T1 a1, T2 a2, T3 a3, T4 a4,
        T5 a5, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, Task<TReturn>> method, T1 a1, T2 a2, T3 a3,
        T4 a4, T5 a5, T6 a6, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, a6, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, Task<TReturn>> method, T1 a1, T2 a2,
        T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, a6, a7, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TReturn>> method, T1 a1,
        T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, a6, a7, a8, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TReturn>> method, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7,
        T8 a8, T9 a9, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, a6, a7, a8, a9, args);
    }

    /// <summary>
    ///     Add a call to this multicall using a c# method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add<TReturn, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TReturn>> method, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7,
        T8 a8, T9 a9, T10 a10, params object[] args)
    {
        return AddMethod(method.Method.Name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, args);
    }

    /// <summary>
    ///     Add a call to this multicall using the name of a async method or directly a XML-RPC method.
    /// </summary>
    /// <returns>Multicall object so you can chain these methods.</returns>
    public MultiCall Add(string method, params object[] args)
    {
        return AddMethod(method, args);
    }

    /// <summary>
    ///     Holds information about a method call.
    /// </summary>
    public class MethodInfo
    {
        public object[] Arguments;
        public string MethodName;

        /// <summary>
        ///     Create a new MethodInfo instance from a method name and arbitrary arguments.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public MethodInfo(string method, object[] args)
        {
            MethodName = method;
            Arguments = args;
        }
    }
}