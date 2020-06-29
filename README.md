# MqttTopicBuilder  

![Build](https://github.com/pBouillon/MqttTopicBuilder/workflows/Build/badge.svg) ![Deployment](https://github.com/pBouillon/MqttTopicBuilder/workflows/NuGet%20package/badge.svg) ![Nuget](https://img.shields.io/nuget/dt/MqttTopicBuilder?color=%2332ca55&label=Downloads%20on%20NuGet&logo=nuget)

📮 `MqttTopicBuilder` is a tool to build valid and verified MQTT topics

The project is built using *.NET Standard 2.0* (compatible with *.NET Core 2* and *.NET Framework 4.6.1*)

## Installation

You can find this projet [on NuGet](https://www.nuget.org/packages/MqttTopicBuilder/).

To install it from the command line, use:  
`~$ dotnet add package MqttTopicBuilder`

or, from the package manager:  
`Install-Package MqttTopicBuilder`

## Usage

> More detailed instructions and documentation is available in [the wiki](https://github.com/pBouillon/MqttTopicBuilder/wiki)  
> For changelog, see [the changelog](./CHANGELOG.md)

Using a custom builder, `MqttTopicBuilder` allows you to build topics and ensure
their veracity.

```csharp
    var topic = new TopicBuilder()
        .AddTopic("Hello")
        .AddTopic("From")
        .AddTopics("Mqtt", "Topic", "Builder")
        .AddMultiLevelWildcard()
        .Build();

    Console.WriteLine(topic);
    // -> "Hello/From/Mqtt/Topic/Builder/#"
```

The built object is a `Topic` object. It can be used to both access the topic
but also gather informations about it such as its level.

```csharp
    var topic = new TopicBuilder()
        .AddTopic("Hello")
        .AddTopic("World")
        .Build();

    Console.WriteLine(topic.Value);
    // -> "Hello/World"

    Console.WriteLine(topic.Levels);
    // -> 2
```

Topics can also be built using the regular constructor or the extension method:

```csharp
    var topic = Topic.FromString("Hello/World");

    Console.WriteLine(topic.Value);
    // -> "Hello/World"

    Console.WriteLine(topic.Levels);
    // -> 2
```

Topic integrity can also be checked using the `TopicValidator` methods

```csharp
    TopicValidator.ValidateTopic("a/wrong/#/Topic");
    // Will throw an exception since no topic is allowed after '#'

    "wrong+Topic".ValidateForTopicAppending();
    // Will throw an exception since '+' is not allowed in a topic
```

## Dependencies

This project is using [FluentAssertions](https://fluentassertions.com/) and [AutoFixture](https://github.com/AutoFixture/AutoFixture) for its unit tests.

## Contributions

All contributions are welcome, please feel free to suggest pull requests ! You can read more about it in the [CONTRIBUTING.md](https://github.com/pBouillon/MqttTopicBuilder/blob/master/CONTRIBUTING.md).
