using System;
using FeedEveryone.Core.Service.WorldGeneration;

namespace FeedEveryone.ComponentTesting;

public class DummyNameGenerator : INameGenerator
{
    public string Generate()
    {
        return "Paradise";
    }
}
