using System;
using System.IO;

namespace FeedEveryone.Mono.Components.Service;

public class FromFileJsonProvider : IJsonProvider
{
    public string Json { get; private set; }

    public FromFileJsonProvider(string filename)
    {
        Json = File.ReadAllText(filename);
    }
}
