using Kompeldev.Generator.Deconstruct;

namespace Kompeldev.Generator.ConsoleApp;

[Deconstruct]
internal partial class Testerke
{
    public Testerke(string name, int age, string emailAddress)
    {
        Name = name;
        Age = age;
        EmailAddress = emailAddress;
    }

    public string Name { get; }
    
    public int Age { get; }
    
    [IgnoreProperty]
    public string EmailAddress { get; }
}