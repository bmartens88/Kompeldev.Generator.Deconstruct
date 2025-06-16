using Kompeldev.Generator.Deconstruct;

namespace Kompeldev.Generator.ConsoleApp;

[Deconstruct]
public partial class Testerke
{
    public Testerke(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; }
    public int Age { get; }
}