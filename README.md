# MqttTopicBuilder ![Build](https://github.com/pBouillon/MqttTopicBuilder/workflows/.NET%20Core/badge.svg) ![Deployment](https://github.com/pBouillon/MqttTopicBuilder/workflows/Push%20NuGet%20package/badge.svg)

`MqttTopicBuilder` is a tool to build valid and verified MQTT topics.

## Installation

You can find this projet [on NuGet](https://www.nuget.org/packages/MqttTopicBuilder/).

To install it from the command line, use:  
`~$ dotnet add package MqttTopicBuilder`

or, from the package manager:  
`Install-Package MqttTopicBuilder`

## Usage

> More detailed instructions and documentation is available in [the wiki](https://github.com/pBouillon/MqttTopicBuilder/wiki)

Using a custom builder, `MqttTopicBuilder` allows you to build topics and ensure
their veracity.

```csharp
    var topicBuilder = new TopicBuilder();

    topicBuilder.AddTopic("Hello")
        .AddTopic("World")
        .AddTopic("From GitHub")
        .AddWildcardMultiLevel();

    var resultingTopic = builder.Build();

    Console.WriteLine(resultingTopic);
    // output: "Hello/World/FromGitHub/#
```

The built object is a `Topic` object. It can be used to both access the topic
but also gather informations about it such as its level.

```csharp
    var topic = new TopicBuilder()
        .AddTopic("Hello")
        .AddTopic("World");

    Console.WriteLine(resultingTopic.Level);
    // output: 2

    Console.WriteLine(resultingTopic.Path);
    // output: Hello/World
```

## Dependencies

This project is using [FluentAssertions](https://fluentassertions.com/) and [AutoFixture](https://github.com/AutoFixture/AutoFixture) for its unit tests.

## Contributions

All contributions are welcome, please feel free to suggest pull requests ! You can read more about it in the [CONTRIBUTING.md](https://github.com/pBouillon/MqttTopicBuilder/blob/master/CONTRIBUTING.md).
