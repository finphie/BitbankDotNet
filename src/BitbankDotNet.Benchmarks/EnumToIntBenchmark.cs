using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// enumの値を取得
    /// </summary>
    /// <typeparam name="T">列挙型</typeparam>
    [Config(typeof(BenchmarkConfig))]
    [GenericTypeArguments(typeof(TestEnum))]
    public class EnumToIntBenchmark<T>
        where T : struct, Enum
    {
        static readonly Func<T, int> ExpressionConvert;
        static readonly Func<T, int> ExpressionConvertChecked;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline", Justification = "ベンチマーク")]
        static EnumToIntBenchmark()
        {
            var parameter = Expression.Parameter(typeof(T), null);

            ExpressionConvert = Expression
                .Lambda<Func<T, int>>(Expression.Convert(parameter, typeof(int)), parameter)
                .Compile();

            ExpressionConvertChecked = Expression
                .Lambda<Func<T, int>>(Expression.ConvertChecked(parameter, typeof(int)), parameter)
                .Compile();
        }

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int ConvertToInt32(T @enum) => Convert.ToInt32(@enum);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int AsCast(T @enum) => (int)(@enum as object);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int DirectCast(T @enum) => (int)(object)@enum;

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int RefValue(T @enum) => (int)__refvalue(__makeref(@enum), TestEnum);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int UnsafeAs(T @enum) => Unsafe.As<T, int>(ref @enum);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int LinqExpressionConvert(T @enum) => ExpressionConvert(@enum);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int LinqExpressionConvertChecked(T @enum) => ExpressionConvertChecked(@enum);

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int EnumsNetToInt32(T @enum) => @enum.GetMember().ToInt32();

        [Benchmark]
        [Arguments(TestEnum.A)]
        public int GetHashCode(T @enum) => @enum.GetHashCode();
    }
}
