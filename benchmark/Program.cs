using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Buffers;
using DocumentoHelper;

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
            .Select(_ => CPF.GenerateUnformatted())
            .ToArray();
    }

    [Benchmark]
    public void Current()
    {
        foreach (var x in CPFs)
            if (new CPF(x).IsValid is false)
                Fail(x);
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