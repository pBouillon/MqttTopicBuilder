<h1 align="center">
  <a href="https://www.nuget.org/packages/MqttTopicBuilder">
    MqttTopicBuilder
  </a>
</h1>

<p align="center">
    Build valid and verified MQTT topics
</p>

MqttTopicBuilder is a small library without any dependency that can help you to
enforce the validity of your [MQTT topics](https://www.hivemq.com/blog/mqtt-essentials-part-5-mqtt-topics-best-practices/)
by using the provided builder.

## Installation

You can find this projet [on NuGet](https://www.nuget.org/packages/MqttTopicBuilder/).

From the command line:

```bash
dotnet add package MqttTopicBuilder
```

From the package manager:

```bash
Install-Package MqttTopicBuilder
```

## Usage

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

The object built is a `Topic` object. It can be used to both access the topic
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
