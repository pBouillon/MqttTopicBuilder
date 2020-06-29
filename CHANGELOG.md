# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
