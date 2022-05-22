# MqttTopicBuilder

[![NuGet Badge](https://buildstats.info/nuget/MqttTopicBuilder)](https://www.nuget.org/packages/MqttTopicBuilder/)

📮 `MqttTopicBuilder` is a tool to build valid and verified MQTT topics

The project is built using *.NET Standard 2.0* (compatible with *.NET Core 2* and *.NET Framework 4.6.1*)

## Installation

You can find this projet [on NuGet](https://www.nuget.org/packages/MqttTopicBuilder/).

To install it from the command line, use:  
`~$ dotnet add package MqttTopicBuilder`

or, from the package manager:  
`Install-Package MqttTopicBuilder`

## Usage

> More detailed instructions and documentation are available [here](https://pbouillon.gitbook.io/mqtttopicbuilder/)  
> For changelog, see [the changelog](./CHANGELOG.md)

Using a custom builder, `MqttTopicBuilder` allows you to build topics and ensure
their validity regarding the way you are planning to use them.

```csharp
var subscribeTo = new TopicBuilder(TopicConsumer.Subscriber)
    .AddTopic("Hello")
    .AddTopic("From")
    .AddTopics("Mqtt", "Topic", "Builder")
    .AddMultiLevelWildcard()
    .Build();

Console.WriteLine(subscribeTo);
// -> "Hello/From/Mqtt/Topic/Builder/#"

var publishTop = new TopicBuilder(TopicConsumer.Publisher)
    .AddTopic("Hello")
    .AddTopic("From")
    .AddTopics("Mqtt", "Topic", "Builder")
    .AddMultiLevelWildcard()
    .Build();
// Will throw an exception since wildcards are not allowed when publishing
// on a topic

```

The built object is a `Topic` object. It can be used to both access the topic
but also gather informations about it such as its level.

```csharp
var topic = new TopicBuilder(TopicConsumer.Subscriber)
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
// or: var topic = (Topic) "Hello/World";

Console.WriteLine(topic.Value);
// -> "Hello/World"

Console.WriteLine(topic.Levels);
// -> 2
```

Topic integrity can also be checked using the `TopicValidator` methods

```csharp
TopicValidator.ValidateTopic("a/wrong/#/Topic");
// Will throw an exception since no topic is allowed after '#'

"wrong+Topic".ValidateTopicForAppending();
// Will throw an exception since '+' is not allowed in a topic
```

## Contributions

All contributions are welcome, please feel free to suggest pull requests !
You can read more about it in the [CONTRIBUTING.md](https://github.com/pBouillon/MqttTopicBuilder/blob/master/CONTRIBUTING.md).
