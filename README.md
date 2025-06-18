# Deconstruct 🚧

## 🚩 Table of Contents

- [Packages](#-packages)
- [Why Deconstruct?](#-why-deconstruct)
- [Examples](#-examples)


## 📦 Packages

### This section will contain reference to published NuGet packages.


## 🤖 Why Deconstruct?

Have you ever experienced whilst developing your next killer app, that a custom type you defined didn't have a deconstructor? I know I have!

Deconstruct is a **Source Generator** which will do just that! By simply adding a specific attribute to your type(s), Deconstruct will generate the *deconstructor* for that type.

### Deconstructor by attribute

After adding Deconstruct to your project, it will add a special *DeconstructorAttribute* attribute class to your compilation. By simply adding this attribute to your class declaration(s), a *deconstructor* for that type will be generated.

```csharp
[Deconstructor]
public partial class Person(string name, int age)
{
    public string Name { get; } = name;

    public int Age { get; } = age;
}
```

Deconstruct will by default:
* **Public by default** : Scan your type for public properties and include those properties in the *deconstructor*.
* **Copy accessibility** : Copy the accessibility modifier of the declared type, ensuring that a *deconstructor* will be generated for both *public* and *internal* types.

The above example will generate in the following *deconstructor* being generated:

```csharp
public partial class Person
{
    public void Deconstruct(out string name, out int age) =>
        (name, age) = (Name, Age);
}
```

### Ignore properties by (yet again) attribute

If the default behavior of Deconstruct is not desired, you can also specify which properties of your type to exclude from being included in the generated *deconstructor* by means of (yet another) attribute.

```csharp
[Deconstructor]
public partial class Person(string name, int age)
{
    public string Name { get; } = name;
    
    [IgnoreProperty]
    public int Age { get; } = age;
}
```

Properties marked with the *IgnorePropertyAttribute* attribute class will be ignored in the scanning process of Deconstruct. Above example will result in the following *deconstructor* being generated:

```csharp
public partial class Person
{
    public void Deconstruct(out string name) =>
        (name) = (Name);
}
```

## 🐾 Examples

With Deconstruct, your defined types (`class`) will get the same *deconstructor* behavior as if you used a `record` type. 

The example below demonstrates how to use the generated *deconstructor* and how this might help in your development flow:

```csharp
var p = new Person("John Doe", 38, Guid.NewGuid());

// use deconstructor to quickly get access to properties
var (name, age) = p;

Console.WriteLine($"Hi {name}!"); // prints "Hi John Doe!"
Console.WriteLine($"You're {age} years of age."); // prints "You're 38 years of age."

[Deconstructor]
internal partial class Person(string name, int age, Guid userId)
{
    public string Name { get; } = name;

    public int Age { get; } = age;

    [IgnoreProperty]
    public Guid UserId { get; } = userId;
}
```

When not all properties in the *deconstructor* are used, you can simply use a discard (`_`) to ignore those properties

```csharp
var (name, _) = p;
```

Please note that for types with just a single property, a *deconstructor* makes no sense and therefore Deconstruct does not support those types.

```mermaid
graph TD;
    A-->B;
    A-->C;
    B-->D;
    C-->D;
```
