# MqttTopicBuilder 

`MqttTopicBuilder` is a tool to build valid and verified MQTT topics.

## Installation

You can find this projet [on NuGet](https://www.nuget.org/packages/MqttTopicBuilder/).

To install it from the command line, use:  
`~$ dotnet add package MqttTopicBuilder`

or, from the package manager:  
`Install-Package MqttTopicBuilder -Version 1.0.0`

## Usage

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

This project is using [FluentAssertions](https://fluentassertions.com/) for its unit tests.

## Contributions

All contributions are welcome, please feel free to suggests pull requests !
