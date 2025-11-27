<h1 align="center">
  <a href="https://www.nuget.org/packages/MqttTopicBuilder">
    MqttTopicBuilder
  </a>
</h1>

<p align="center">
    [![NuGet](https://img.shields.io/nuget/v/MqttTopicBuilder.svg)](https://www.nuget.org/packages/MqttTopicBuilder/)
    [![NuGet Downloads](https://img.shields.io/nuget/dt/MqttTopicBuilder.svg)](https://www.nuget.org/packages/MqttTopicBuilder/)
    [![License](https://img.shields.io/github/license/pBouillon/MqttTopicBuilder.svg)](https://github.com/pBouillon/MqttTopicBuilder/blob/main/LICENSE)
</p>

<p align="center">
    Build valid and verified MQTT topics
</p>

## Overview

MqttTopicBuilder helps you construct valid [MQTT topics](https://www.hivemq.com/blog/mqtt-essentials-part-5-mqtt-topics-best-practices/) using a fluent builder API. It enforces MQTT topic rules at build time, preventing common mistakes like using wildcards in publisher topics or placing wildcards in invalid positions.

## Installation

Install via .NET CLI:

```bash
dotnet add package MqttTopicBuilder
```

Or via Package Manager Console:

```bash
Install-Package MqttTopicBuilder
```

## Quick Start

### Building Topics

Use the `TopicBuilder` to construct topics with validation based on whether you're publishing or subscribing:

```csharp
// Subscriber topic with wildcards
var subscribeTopic = new TopicBuilder(TopicConsumer.Subscriber)
    .AddTopic("sensors")
    .AddTopic("temperature")
    .AddMultiLevelWildcard()
    .Build();

Console.WriteLine(subscribeTopic);
// Output: "sensors/temperature/#"

// Publisher topic (wildcards not allowed)
var publishTopic = new TopicBuilder(TopicConsumer.Publisher)
    .AddTopic("sensors")
    .AddTopic("temperature")
    .AddTopic("room1")
    .Build();

Console.WriteLine(publishTopic);
// Output: "sensors/temperature/room1"
```

### Topic Properties

Access topic metadata through the `Topic` object:

```csharp
var topic = new TopicBuilder(TopicConsumer.Subscriber)
    .AddTopic("home")
    .AddTopic("livingroom")
    .AddTopic("temperature")
    .Build();

Console.WriteLine(topic.Value);   // "home/livingroom/temperature"
Console.WriteLine(topic.Levels);  // 3
```

### Creating Topics from Strings

Convert existing topic strings into `Topic` objects:

```csharp
var topic = Topic.FromString("home/kitchen/humidity");
// or using implicit conversion:
// var topic = (Topic)"home/kitchen/humidity";

Console.WriteLine(topic.Value);   // "home/kitchen/humidity"
Console.WriteLine(topic.Levels);  // 3
```

### Validation

Validate topic strings before using them:

Topic integrity can also be checked sing the `TopicValidator` methods:

```csharp
TopicValidator.ValidateTopic("a/wrong/#/Topic");
// Will throw an exception since no topic is allowed after '#'

"wrong+Topic".ValidateTopicForAppending();
// Will throw an exception since '+' is not allowed in a topic
```

MQTT Topics rules are checked, such as if the topic is not blank,
is utf-8, does not contains any forbidden character, and so on.

If any rule isn't respected, this will throw a specific exception, inheriting from the
`MqttBaseException`.

## Features

- **Fluent API** - Intuitive builder pattern for constructing topics
- **Validation** - Enforces MQTT specification rules
- **Wildcard support** - Proper handling of single-level (`+`) and multi-level (`#`) wildcards
- **Publisher and Subscriber modes** - Build topics with validation rules specific to each use case

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
