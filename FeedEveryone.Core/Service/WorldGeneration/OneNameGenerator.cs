using System;

namespace FeedEveryone.Core.Service.WorldGeneration;

public class OneNameGenerator : INameGenerator
{
    public OneNameGenerator(string name)
    {
        this.name = name;
    }

    public string Generate()
    {
        return name;
    }

    private readonly string name;
}
