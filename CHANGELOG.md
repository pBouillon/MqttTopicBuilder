# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.3.0] - Draft

### Added

- Add C# nullable support
- Add .NET Framework, .NET Core and .NET to the targets

## [2.2.0] - 2020-08-26

### Added

- Add `ITopicBuilder.Clear` method
- Add `TopicBuilder.FromTopic` method
- Add `Topic.ToArray` method
- Add `ITopicBuilder` creation from `Topic`
- Add `ITopicBuilder.ToPublisherBuilder` extension method
- Add `ITopicBuilder.ToSubscriberBuilder` extension method

### Changed

- `TopicBuilder` no longer use `TopicConsumer.Subscriber` by default
- `TopicBuilder` now performs rule checks when built with a collection

### Removed

- Remove obsolete `TopicBuilder` constructors
- Remove unused `ITopicCollection.Clear` method

## [2.1.0] - 2020-06-26

### Added

- Add [codefactor](https://www.codefactor.io/repository/github/pbouillon/mqtttopicbuilder) to CI for quality checks
- Add UTF-8 check on topic creation
- Add modes depending on the consumer of the topic builder (PUB/SUB)

### Changed

- Rework topic validation using validation pipelines and rules
- `Topic` can now be used as a value object
- Set C# version to 8

### Deprecated

- `TopicBuilder` constructors that does not specify the type of the consumer
  using it

### Fixed

- Fixed a unit test that would potentially fail on random values

## [2.0.2] - 2020-06-29

### Changed

- Merged `MqttTopicBuilder.Exceptions` in core project `MqttTopicBuilder`
- Changed deprecated `PackageLicenseUrl` tag to `PackageLicense` in `MqttTopicBuilder.csproj`

### Removed

- Deleted `MqttTopicBuilder.Exceptions` project

## [2.0.1] - 2020-06-27

### Changed

- Renamed CI job from `.NET Core` to `Build`

### Removed

- Removed GitHub from packages registries for the produced NuGet

## [2.0.0] - 2020-06-27

### Added

- Create `CHANGELOG.md`
- Immutable inner-collection (`ITopicCollection`) on which the builder now
  relies
- Both the `TopicBuilder` and the collection aforementionned have
  interfaces for dependency injection
- Multiple topics can now be added in the builder with `AddTopics`
- The builder can now be cloned
- A topic can now be created for a raw string using the `Topic`
  extension method
- Add GitHub registry in the `nuget.yml` file

### Changed

- The `TopicBuilder` is now immutable
- Exceptions have been moved to a dedicated project
- Topic checks are now grouped under the `TopicValidator` class
- `MqttTopicBuilderUnitTests` have been moved to `MqttTopicBuilder.UnitTests`

### Removed

- The staging topics are no longer exposed by the builder
