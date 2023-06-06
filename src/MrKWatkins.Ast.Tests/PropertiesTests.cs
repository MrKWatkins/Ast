namespace MrKWatkins.Ast.Tests;

public sealed class PropertiesTests
{
    [Test]
    public void GetOrThrow()
    {
        var properties = new Properties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrThrow<int>("One").Should().Be(1);
        properties.GetOrThrow<string>("Two").Should().Be("2");
    }
        
    [Test]
    public void GetOrThrow_ThrowsIfValueIsAMultiple()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.GetOrThrow<string>("Key"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has multiple values.");
    }
        
    [Test]
    public void GetOrThrow_ThrowsIfNoValueFoundForKey()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrThrow<string>("Two"))
            .Should().Throw<KeyNotFoundException>()
            .WithMessage("No value for property with key \"Two\".");
    }
        
    [Test]
    public void GetOrThrow_ExceptionCreator_ThrowsIfNoValueFoundForKey()
    {
        var properties = new Properties();
        
        var exception = new InvalidOperationException("Test");

        Exception Creator() => exception;

        properties.Invoking(p => p.GetOrThrow<string>("Key", Creator))
            .Should().Throw<InvalidOperationException>()
            .Which.Should().BeSameAs(exception);
    }
    
    [Test]
    public void GetOrThrow_KeyedExceptionCreator_ThrowsIfNoValueFoundForKey()
    {
        var properties = new Properties();
        
        var exception = new InvalidOperationException("Test");

        Exception Creator(string key)
        {
            key.Should().Be("Key");
            return exception;
        }

        properties.Invoking(p => p.GetOrThrow<string>("Key", Creator))
            .Should().Throw<InvalidOperationException>()
            .Which.Should().BeSameAs(exception);
    }
        
    [Test]
    public void GetOrThrow_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrThrow<string>("One"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has a value of type Int32; cannot change to String.");
    }

    [Test]
    public void GetOrDefault()
    {
        var properties = new Properties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrDefault<int>("One").Should().Be(1);
        properties.GetOrDefault<string>("Two").Should().Be("2");
    }
        
    [Test]
    public void GetOrDefault_ReturnsTheDefaultIfNoValueFoundForKey()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.GetOrDefault<string>("Two").Should().BeNull();
        properties.GetOrDefault("Two", "Default").Should().Be("Default");
            
        properties.GetOrDefault<int>("Two").Should().Be(default);
        properties.GetOrDefault("Two", 123).Should().Be(123);
    }
        
    [Test]
    public void GetOrDefault_ThrowsIfValueIsAMultiple()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.GetOrDefault<string>("Key"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has multiple values.");
    }

    [Test]
    public void GetOrDefault_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrDefault<string>("One"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has a value of type Int32; cannot change to String.");
    }
    
    [Test]
    public void GetOrAdd_GetsExistingValue()
    {
        var properties = new Properties();
        properties.Set("Key", 1);
        
        properties.GetOrAdd("Key", ShouldNotBeCalled<int>).Should().Be(1);
    }
    
    [Test]
    public void GetOrAdd_AddsNewValue()
    {
        var properties = new Properties();
        
        static string Creator(string key)
        {
            key.Should().Be("Key");
            return "Value";
        }

        properties.GetOrAdd("Key", Creator).Should().Be("Value");

        properties.GetOrThrow<string>("Key").Should().Be("Value");
    }
    
    [Test]
    public void GetOrAdd_CanAddSubTypes()
    {
        var properties = new Properties();
        
        static string Creator(string key)
        {
            key.Should().Be("Key");
            return "Value";
        }

        properties.GetOrAdd<object>("Key", Creator).Should().Be("Value");

        properties.GetOrThrow<object>("Key").Should().Be("Value");
    }
        
    [Test]
    public void GetOrAdd_ThrowsIfValueIsAMultiple()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.GetOrAdd("Key", ShouldNotBeCalled<string>))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has multiple values.");
    }
        
    [Test]
    public void GetOrAdd_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrAdd("One", ShouldNotBeCalled<string>))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has a value of type Int32; cannot change to String.");
    }
    
    [Test]
    public void TryGet_ValueExists()
    {
        var properties = new Properties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.TryGet<int>("One", out var one).Should().BeTrue();
        one.Should().Be(1);
        
        properties.TryGet<string>("Two", out var two).Should().BeTrue();
        two.Should().Be("2");
    }
        
    [Test]
    public void TryGet_ValueDoesNotExist()
    {
        var properties = new Properties();

        properties.TryGet<int>("One", out _).Should().BeFalse();
        properties.TryGet<string>("Two", out _).Should().BeFalse();
    }
        
    [Test]
    public void TryGet_ThrowsIfValueIsAMultiple()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.TryGet<string>("Key", out _))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has multiple values.");
    }
        
    [Test]
    public void TryGet_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new Properties();
        properties.Set("One", 1);

       properties.Invoking(p => p.TryGet<string>("One", out _))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has a value of type Int32; cannot change to String.");
    }
        
    [Test]
    public void ContainsKey()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.ContainsKey("One").Should().BeTrue();
        properties.ContainsKey("Two").Should().BeFalse();
    }
        
    [Test]
    public void Count()
    {
        var properties = new Properties();
        properties.Count.Should().Be(0);
        
        properties.Set("One", 1);
        properties.Count.Should().Be(1);
        
        properties.SetMultiple("Two", new [] { 1, 2 });
        properties.Count.Should().Be(2);
        
        properties.AddToMultiple("Two", 3);
        properties.Count.Should().Be(2);
    }
    
    [Test]
    public void Set()
    {
        var properties = new Properties();
        properties.GetOrDefault<int>("Key").Should().Be(default);
        
        properties.Set("Key", 1);
        properties.GetOrDefault<int>("Key").Should().Be(1);
        
        properties.Set("Key", 2);
        properties.GetOrDefault<int>("Key").Should().Be(2);
    }
    
    [Test]
    public void Set_ThrowsIfValueIsAMultiple()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.Set("Key", 1))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has multiple values.");
    }
    
    [Test]
    public void Set_ThrowsIfExistingValueOfADifferentType()
    {
        var properties = new Properties();
        properties.Set("One", 1);

        properties.Invoking(p => p.Set("One", "1"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has a value of type Int32; cannot change to String.");
    }
    
    [Test]
    public void Set_CanAddSubTypes()
    {
        var properties = new Properties();

        properties.Set<object>("Key", "Value");

        properties.GetOrThrow<object>("Key").Should().Be("Value");
    }

    [Test]
    public void GetMultiple()
    {
        var properties = new Properties();

        properties.GetMultiple<int>("Key").Should().BeEmpty();
        
        properties.AddToMultiple("Key", 1);
        properties.GetMultiple<int>("Key").Should().Equal(1);

        properties.AddToMultiple("Key", 2);
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);
    }

    [Test]
    public void GetMultiple_ThrowsIfValueIsASingle()
    {
        var properties = new Properties();
        properties.Set("Key", 1);

        properties.Invoking(p => p.GetMultiple<string>("Key"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" is a single value.");
    }

    [Test]
    public void GetMultiple_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new Properties();
        properties.AddToMultiple("Key", 1);

        properties.Invoking(p => p.GetMultiple<string>("Key"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" has values of type Int32; cannot change to String.");
    }
    
    [Test]
    public void SetMultiple()
    {
        var properties = new Properties();
        properties.GetMultiple<int>("Key").Should().BeEmpty();
        
        properties.SetMultiple("Key", new[] { 1 });
        properties.GetMultiple<int>("Key").Should().Equal(1);

        properties.SetMultiple("Key", new[] { 1, 2 });
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);
    }
    
    [Test]
    public void SetMultiple_ThrowsIfValueIsASingle()
    {
        var properties = new Properties();
        properties.Set("Key", 1);

        properties.Invoking(p => p.SetMultiple("Key", new[] { 1, 2 }))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" is a single value.");
    }
    
    [Test]
    public void SetMultiple_ThrowsIfExistingValueOfADifferentType()
    {
        var properties = new Properties();
        properties.SetMultiple("One", new[] { 1, 2 });

        properties.Invoking(p => p.SetMultiple("One", new[] { "1", "2" }))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has values of type Int32; cannot change to String.");
    }
    
    [Test]
    public void SetMultiple_ThrowsIfValueIsNull()
    {
        var properties = new Properties();

        properties.Invoking(p => p.SetMultiple<string>("One", null!))
            .Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void SetMultiple_CanAddSubTypes()
    {
        var properties = new Properties();

        properties.SetMultiple<object>("Key", new [] { "Value" });

        properties.GetMultiple<object>("Key").Should().Equal("Value");
    }
    
    [Test]
    public void AddToMultiple()
    {
        var properties = new Properties();
        properties.GetMultiple<int>("Key").Should().BeEmpty();
        
        properties.AddToMultiple("Key", 1);
        properties.GetMultiple<int>("Key").Should().Equal(1);

        properties.AddToMultiple("Key", 2);
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);
    }
    
    [Test]
    public void AddToMultiple_ThrowsIfValueIsASingle()
    {
        var properties = new Properties();
        properties.Set("Key", 1);

        properties.Invoking(p => p.AddToMultiple("Key", 1))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" is a single value.");
    }
    
    [Test]
    public void AddToMultiple_ThrowsIfExistingValueOfADifferentType()
    {
        var properties = new Properties();
        properties.AddToMultiple("One", 1);

        properties.Invoking(p => p.AddToMultiple("One", "2"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has values of type Int32; cannot change to String.");
    }

    [Test]
    public void AddToMultiple_CanAddSubTypes()
    {
        var properties = new Properties();

        properties.AddToMultiple<object>("Key", "Value");

        properties.GetMultiple<object>("Key").Should().Equal("Value");
    }
    
    [Test]
    public void TryAddToMultiple()
    {
        var properties = new Properties();
        properties.GetMultiple<int>("Key").Should().BeEmpty();
        
        properties.TryAddToMultiple("Key", 1).Should().BeTrue();
        properties.GetMultiple<int>("Key").Should().Equal(1);

        properties.TryAddToMultiple("Key", 2).Should().BeTrue();
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);
        
        properties.TryAddToMultiple("Key", 1).Should().BeFalse();
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);
    }
    
    [Test]
    public void TryAddToMultiple_ValueComparer()
    {
        var properties = new Properties();
        properties.TryAddToMultiple("Key", "One", StringComparer.OrdinalIgnoreCase).Should().BeTrue();
        properties.GetMultiple<string>("Key").Should().Equal("One");
        
        properties.TryAddToMultiple("Key", "one", StringComparer.OrdinalIgnoreCase).Should().BeFalse();
        properties.GetMultiple<string>("Key").Should().Equal("One");
        
        properties.TryAddToMultiple("Key", "one").Should().BeTrue();
        properties.GetMultiple<string>("Key").Should().Equal("One", "one");
    }
    
    [Test]
    public void TryAddToMultiple_ThrowsIfValueIsASingle()
    {
        var properties = new Properties();
        properties.Set("Key", 1);

        properties.Invoking(p => p.TryAddToMultiple("Key", 1))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" is a single value.");
    }
    
    [Test]
    public void TryAddToMultiple_ThrowsIfExistingValueOfADifferentType()
    {
        var properties = new Properties();
        properties.AddToMultiple("One", 1);

        properties.Invoking(p => p.TryAddToMultiple("One", "2"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has values of type Int32; cannot change to String.");
    }

    [Test]
    public void TryAddToMultiple_CanAddSubTypes()
    {
        var properties = new Properties();

        properties.TryAddToMultiple<object>("Key", "Value").Should().BeTrue();

        properties.GetMultiple<object>("Key").Should().Equal("Value");
    }
    
    [Test]
    public void AddRangeToMultiple()
    {
        var properties = new Properties();
        properties.GetMultiple<int>("Key").Should().BeEmpty();
        
        properties.AddRangeToMultiple("Key", new [] { 1, 2 });
        properties.GetMultiple<int>("Key").Should().Equal(1, 2);

        properties.AddRangeToMultiple("Key", new [] { 3, 4 });
        properties.GetMultiple<int>("Key").Should().Equal(1, 2, 3, 4);
    }
    
    [Test]
    public void AddRangeToMultiple_ThrowsIfValueIsASingle()
    {
        var properties = new Properties();
        properties.Set("Key", 1);

        properties.Invoking(p => p.AddRangeToMultiple("Key", new [] { 1, 2 }))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"Key\" is a single value.");
    }

    [Test]
    public void AddRangeToMultiple_ThrowsIfExistingValueOfADifferentType()
    {
        var properties = new Properties();
        properties.AddToMultiple("One", 1);

        properties.Invoking(p => p.AddRangeToMultiple("One", new[] { "1", "2" }))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property \"One\" has values of type Int32; cannot change to String.");
    }

    [Test]
    public void AddRangeToMultiple_CanAddSubTypes()
    {
        var properties = new Properties();

        properties.AddRangeToMultiple<object>("Key", new[] { "1", "2" });

        properties.GetMultiple<object>("Key").Should().Equal("1", "2");
    }

    [Test]
    public void Copy()
    {
        var properties = new Properties();
        properties.Set("SingleOne", 1);
        properties.Set("SingleTwo", "Two");
        properties.Set<object>("SingleThree", "Two");
        
        properties.SetMultiple("MultipleOne", new [] { 1, 2 });
        properties.SetMultiple("MultipleTwo", new [] { "1", "2" });
        properties.SetMultiple("MultipleThree", new object[] { 1, "2" });

        var copy = properties.Copy();
        copy.GetOrThrow<int>("SingleOne").Should().Be(1);
        copy.GetOrThrow<string>("SingleTwo").Should().Be("Two");
        copy.GetOrThrow<object>("SingleThree").Should().Be("Two");

        copy.GetMultiple<int>("MultipleOne").Should().Equal(1, 2);
        copy.GetMultiple<string>("MultipleTwo").Should().Equal("1", "2");
        copy.GetMultiple<object>("MultipleThree").Should().Equal(1, "2");
        
        // Mutating the copy should not change the original.
        copy.Set("SingleOne", 2);
        properties.GetOrThrow<int>("SingleOne").Should().Be(1);
        
        copy.AddToMultiple("MultipleOne", 3);
        properties.GetMultiple<int>("MultipleOne").Should().Equal(1, 2);
    }
    
    [DoesNotReturn]
    private static T ShouldNotBeCalled<T>(string _) => throw new InvalidOperationException("Should not be called.");
}