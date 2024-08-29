using System;
using FeedEveryone.Service.WorldGeneration;

namespace FeedEveryoneComponentTesting;

public class DummyNameGenerator : INameGenerator
{
    public string Generate()
    {
        return "Paradise";
    }
}
