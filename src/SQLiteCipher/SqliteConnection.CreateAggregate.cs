using System;

namespace System.Data.SQLiteCipher
{
    partial class SqliteConnection
    {
        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate>(string name, Func<TAccumulate, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 0, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a)), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, TAccumulate>(string name, Func<TAccumulate, T1, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 1, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, TAccumulate>(string name, Func<TAccumulate, T1, T2, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 2, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 3, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 4, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 5, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 6, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 7, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 8, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 9, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 10, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 11, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 12, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 13, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 14, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate>(string name, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 15, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13), r.GetFieldValue<T15>(14))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate>(string name, Func<TAccumulate, object[], TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, -1, default, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, GetValues(r))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 0, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a)), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 1, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 2, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 3, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 4, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 5, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 6, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 7, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 8, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 9, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 10, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 11, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 12, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 13, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 14, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, 15, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13), r.GetFieldValue<T15>(14))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate>(string name, TAccumulate seed, Func<TAccumulate, object[], TAccumulate> func, bool isDeterministic = false)
            => CreateAggregateCore(name, -1, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, GetValues(r))), a => a, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 0, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a)), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 1, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 2, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 3, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 4, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 5, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 6, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 7, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 8, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 9, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 10, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 11, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 12, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 13, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 14, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the function.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the function.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the function.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the function.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the function.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the function.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the function.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the function.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the function.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the function.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the function.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the function.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the function.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the function.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, 15, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, r.GetFieldValue<T1>(0), r.GetFieldValue<T2>(1), r.GetFieldValue<T3>(2), r.GetFieldValue<T4>(3), r.GetFieldValue<T5>(4), r.GetFieldValue<T6>(5), r.GetFieldValue<T7>(6), r.GetFieldValue<T8>(7), r.GetFieldValue<T9>(8), r.GetFieldValue<T10>(9), r.GetFieldValue<T11>(10), r.GetFieldValue<T12>(11), r.GetFieldValue<T13>(12), r.GetFieldValue<T14>(13), r.GetFieldValue<T15>(14))), resultSelector, isDeterministic);

        /// <summary>
        ///     Creates or redefines an aggregate SQL function.
        /// </summary>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="name">The name of the SQL function.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element. Pass null to delete a function.</param>
        /// <param name="resultSelector">
        ///     A function to transform the final accumulator value into the result value. Pass null to
        ///     delete a function.
        /// </param>
        /// <param name="isDeterministic">Flag indicating whether the aggregate is deterministic.</param>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/user-defined-functions">User-Defined Functions</seealso>
        /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
        public virtual void CreateAggregate<TAccumulate, TResult>(string name, TAccumulate seed, Func<TAccumulate, object[], TAccumulate> func, Func<TAccumulate, TResult> resultSelector, bool isDeterministic = false)
            => CreateAggregateCore(name, -1, seed, IfNotNull<TAccumulate, TAccumulate>(func, (a, r) => func(a, GetValues(r))), resultSelector, isDeterministic);
    }
}
