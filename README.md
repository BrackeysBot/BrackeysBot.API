<h1 align="center">BrackeysBot.API</h1>
<p align="center"><i>A C# Discord framework tailored for the Brackeys Discord server, with support for hot-swappable plugins.</i></p>
<p align="center">
<a href="https://github.com/oliverbooth/BrackeysBot.API/releases"><img src="https://img.shields.io/github/v/release/oliverbooth/BrackeysBot?include_prereleases"></a>
<a href="https://github.com/oliverbooth/BrackeysBot.API/actions?query=workflow%3A%22.NET%22"><img src="https://img.shields.io/github/workflow/status/oliverbooth/BrackeysBot.API/.NET" alt="GitHub Workflow Status" title="GitHub Workflow Status"></a>
<a href="https://github.com/oliverbooth/BrackeysBot.API/issues"><img src="https://img.shields.io/github/issues/oliverbooth/BrackeysBot" alt="GitHub Issues" title="GitHub Issues"></a>
<a href="https://github.com/oliverbooth/BrackeysBot.API/blob/main/LICENSE.md"><img src="https://img.shields.io/github/license/oliverbooth/BrackeysBot" alt="MIT License" title="MIT License"></a>
<a href="https://discord.gg/brackeys"><img src="https://discordapp.com/api/guilds/243005537342586880/widget.png?style=shield"></a>
</p>

## About
BrackeysBot is a Discord bot framework with the goal of segregating functionality through the use of hot-swappable plugins.

The aim of this project is to better organise the featureset of the ever-growing [BrackeysBot version 3](https://github.com/yiliansource/brackeys-bot/), by splitting functionality into single-purpose bots while maintaining feature parity with version 3.

## Contributing
Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

# Code Requirements
* For the most part, BrackeysBot and BrackeysBot.API use Microsoft's defined [coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) and [framework design guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/).
* When bumping the target .NET version, only target LTS releases (even-numbered releases starting with .NET 5).
* No tabs; use 4 spaces instead.
    * Blank lines should contain no spaces.
* No trailing whitespaces at the end of lines.
* Each file must be terminated with a blank line.
* Try to keep lines at 130 characters maximum.
* No one-line methods.
* Do not use expression bodies for methods. Expression-bodied properties are okay.
* All major additions should have documentation.
* All code should be free of magic values. If this is not possible, it should be marked with a `TODO` comment indicating it should be addressed in the future.
* No unnecessary code changes. Look through all your changes before submitting a PR.
* Do not attempt to fix multiple problems with a single patch or PR.
* Avoid moving or renaming classes.
* All type members must have explicit nullability annotations through the use of the `?` operator. **Do not use `CanBeNullAttribute` or `NotNullAttribute`**.

## License
BrackeysBot and most of its plugins are under the [MIT License](LICENSE.md).

## Disclaimer
This bot and its plugins is tailored for use within the [Brackeys Discord server](https://discord.gg/brackeys). While this framework is open source and you are free to use it in your own servers, you accept responsibility for any mishaps which may arise from the use of this software. Use at your own risk.
