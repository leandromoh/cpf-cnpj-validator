using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Buffers;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<CPFBenchmark>();
    }
}

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
public class CPFBenchmark
{
    [Params(1_000_000)]
    public int Count { get; set; }

    public IEnumerable<string> CPFs;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();

        CPFs = Enumerable
            .Range(0, Count)
            .Select(_ => BuildCPF())
            .ToArray();

        string BuildCPF()
        {
            Span<int> digits = stackalloc int[11];
            for (var i = 0; i < 9; i++)
                digits[i] = random.Next(0, 9);

            digits[9] = CPFImp3.CriaDigitoVerificador1(digits);
            digits[10] = CPFImp3.CriaDigitoVerificador2(digits);

            Span<char> chars = stackalloc char[11];
            for (var i = 0; i < digits.Length; i++)
                chars[i] = (char)(digits[i] + '0');

            return new string(chars);
        }
    }

    [Benchmark]
    public void Imp1()
    {
        foreach (var x in CPFs)
            if (new CPFImp1(x).IsValid is false)
                Fail(x);
    }

    [Benchmark]
    public void Imp2()
    {
        foreach (var x in CPFs)
            if (new CPFImp2(x).IsValid is false)
                Fail(x);
    }

    [Benchmark]
    public void Imp3()
    {
        foreach (var x in CPFs)
            if (new CPFImp3(x).IsValid is false)
                Fail(x);
    }

    [Benchmark]
    public void Imp4()
    {
        foreach (var x in CPFs)
            if (new CPFImp4(x).IsValid is false)
                Fail(x);
    }

    [Benchmark]
    public void Imp5()
    {
        foreach (var x in CPFs)
            if (new CPFImp5(x).IsValid is false)
                Fail(x);
    }

    [Benchmark]
    public void Imp6()
    {
        foreach (var x in CPFs)
            if (new CPFImp6(x).IsValid is false)
                Fail(x);
    }

    private void Fail(string cpf) =>
        throw new Exception($"failed for '{cpf}' cpf");
}