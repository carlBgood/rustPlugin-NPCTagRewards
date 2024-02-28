# NPC Tag Rewards Plugin

## Overview

The Rust NPC Tag Rewards Plugin is a Rust game plugin designed to introduce an interactive NPC dropbox mechanic within the Outpost monument. Players can deposit ID tags into the dropbox, and based on the color of the tags, they will receive server reward points (RP). This plugin is dependent on the Monument Addon and Server Rewards plugins.

## Features

- **NPC Dropbox Search**: Searches for a dropbox within the Outpost monument, providing players with a place to deposit id tags.

- **ID Tag Rewards**: Players can deposit ID tags into the NPC dropbox, and the plugin will reward them with server reward points (RP) based on the color of the tags.

- **Dependency on Monument Addon and Server Rewards**: Ensure you have Monument Addon and Server Rewards plugins installed and properly configured for the Rust NPC Tag Rewards Plugin to function seamlessly.

## Installation

1. **Dependencies**: Make sure you have Monument Addon and Server Rewards installed.
2. **Download**: Clone or download the Rust NPC Tag Rewards Plugin repository.
3. **Installation**: Place the plugin in the appropriate directory of your Rust server (oxide/plugins).

## Usage

No additional configuration is needed. The plugin will automatically integrate with the Outpost monument, allowing players to interact with the NPC dropbox. It will run a quick search to find the dropbox every 10 seconds if it does not exist.
