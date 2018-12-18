using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks
{
    enum TestEnum
    {
        [EnumMember(Value = "a")]
        A,

        [EnumMember(Value = "a")]
        B
    }
}