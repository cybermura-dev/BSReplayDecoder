# BLReplayDecoder

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies](#technologies)
- [Installation](#installation)
  - [Prequisites](#prequisites)
  - [Steps](#steps)
- [Usage](#usage)
  - [Basic Example](#basic-example)
  - [Full Replay Example](#full-replay-example)
  - [Extracting Player Stats](#extracting-player-stats)
  - [Analyzing Block Information](#analyzing-block-information)
- [Code Structure](#code-structure)
  - [Main Classes](#main-classes)
  - [ReplayData Class](#replaydata-class)
  - [Block Class](#block-class)
  - [Player Class](#player-class)
- [How it Works](#how-it-works)
  - [Replay File Parsing](#replay-file-parsing)
  - [Data Structures](#data-structures)
  - [AI Training and Data Analysis](#ai-training-and-data-analysis)
  - [Error Handling](#error-handling)
  - [Optimizations](#optimizations)
- [License](#license)

## Overview

**BLReplayDecoder** is a C# library designed to decode and extract data from **Beat Saber** replay files (.bsor). The library processes the replay files to extract essential game data such as player actions, block positions, block types, timings, and player statistics (e.g., score, combo, accuracy). This information can be used for analyzing gameplay, training AI models, or developing Beat Saber-related tools and utilities.
The **BLReplayDecoder** is built to be easy to integrate into your existing projects. It provides both low-level access to raw replay data and higher-level abstractions, enabling both simple usage and advanced analysis.

## Features

- **Decodes Beat Saber Replay Files:** Parses `.bsor` files and extracts detailed replay data.
- **Block Data:** Provides data on each block in the replay, including type, position, direction, and timing.
- **Player Stats:** Includes information such as score, accuracy, combo count, and other gameplay metrics.
- **Event Timing:** Decodes timestamps for each block to enable precise analysis of player actions.
- **AI Training:** Use the extracted data to train AI models that simulate or predict gameplay.
- **Replay Validation:** Handles errors or missing data within .bsor files, ensuring graceful failure.
- **High-Level API:** Offers both low-level and high-level methods for interacting with replay files, making it accessible for various needs.

## Technologies

- **C# 13.0:** The latest stable version of C# for maximum performance and language features.
- **.NET 9.0:** The framework that powers the library, ensuring compatibility with modern tools and efficient execution.
- **NUnit 3:** Unit testing framework to ensure code quality and correctness.
- **System.IO:** For efficient handling of file I/O operations, including reading and writing binary data.

## Installation

### Prerequisites

- **.NET SDK 9.0:** Ensure you have **.NET 9.0** SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/ru-ru/download).
- **Beat Saber Replay File:** Obtain a `.bsor` replay file from your Beat Saber installation or community resources.

### Steps

1. **Clone the Repository:**
   
    Start by cloning the **BLReplayDecoder** repository:
    ```bash
    git clone https://github.com/cybermura-dev/BLReplayDecoder.git
    cd BLReplayDecoder
    ```

1. **Restore Dependencies:**
   
    Run the following command to restore the required dependencies for the project:
    ```bash
    dotnet restore
    ```
   
2. **Build the Project:**
   
    Build the project using the .NET CLI:
    ```bash
    dotnet build
    ```

3. **Run the Project:**
   
    TBo quickly test the decoder, run the following:
    ```bash
    dotnet run
    ```
    
    This will execute a sample replay file if one is provided and output decoded information to the console.

## Usage

### Basic Example

The simplest use case for **BLReplayDecoder** involves loading a replay and outputting basic stats. Below is a simple example that prints the player’s score and the total number of blocks hit:
```csharp
using BLReplayDecoder.Core;

string filePath = "path_to_replay.bsor";
byte[] replayData = File.ReadAllBytes(filePath);

Replay replay = ReplayDecoder.Decode(replayData);

Console.WriteLine($"Player Score: {replay.Score}");
Console.WriteLine($"Total Blocks Hit: {replay.Blocks.Count}");
```

### Full Replay Example

To explore the replay in more detail, you can extract information about individual blocks, including position, direction, and timing:
```csharp
using BLReplayDecoder.Core;

string filePath = "path_to_replay.bsor";
byte[] replayData = File.ReadAllBytes(filePath);

Replay replay = ReplayDecoder.Decode(replayData);

foreach (Block block in replay.Blocks)
{
    Console.WriteLine($"Block Type: {block.Type}, Position: {block.Position}, Direction: {block.Direction}, Timing: {block.Timing}");
}
```

### Extracting Player Stats

You can extract the full player statistics from the replay, such as score, combo count, accuracy, and more:
```csharp
using BLReplayDecoder.Core;

string filePath = "path_to_replay.bsor";
byte[] replayData = File.ReadAllBytes(filePath);

Replay replay = ReplayDecoder.Decode("path_to_replay.bsor");

Console.WriteLine($"Player: {replay.Player.Name}");
Console.WriteLine($"Score: {replay.Player.Score}");
Console.WriteLine($"Accuracy: {replay.Player.Accuracy}%");
Console.WriteLine($"Max Combo: {replay.Player.ComboCount}");
```

### Analyzing Block Information

To analyze block data, including timing and type of blocks, use the following code to loop through blocks:
```csharp
foreach (Block block in replay.Blocks)
{
    Console.WriteLine($"Block Type: {block.Type}");
    Console.WriteLine($"Block Position: {block.Position}");
    Console.WriteLine($"Block Direction: {block.Direction}");
    Console.WriteLine($"Block Timing: {block.Timing}");
}
```

## Code Structure

```bash
BSReplayDecoder
│   BSReplayDecoder.sln
│
├───.idea
│   └───.idea.BSReplayDecoder
│       └───.idea
│               .gitignore
│               encodings.xml
│               indexLayout.xml
│               projectSettingsUpdater.xml
│
├───BSReplayDecoder.Core
│   │   BSReplayDecoder.Core.csproj
│   │   ReplayDecoder.cs
│   │
│   ├───Handlers
│   │       FrameHandler.cs
│   │       HeightHandler.cs
│   │       InfoHandler.cs
│   │       NoteHandler.cs
│   │       PauseHandler.cs
│   │       WallHandler.cs
│   │
│   ├───Models
│   │       CutInfo.cs
│   │       Euler.cs
│   │       Frame.cs
│   │       Height.cs
│   │       Note.cs
│   │       Pause.cs
│   │       Quaternion.cs
│   │       Replay.cs
│   │       ReplayInfo.cs
│   │       Vector3.cs
│   │       Wall.cs
│   │
│   └───Types
│           NoteEventType.cs
│           StructType.cs
│
├───BSReplayDecoder.Example
│       BSReplayDecoder.Example.csproj
│       Program.cs
│
└───BSReplayDecoder.Tests
    │   BSReplayDecoder.Tests.csproj
    │   ReplayDecoderTests.cs
    │
    └───Resources
            Bluenation_replay.bsor
```

### Main Classes

- **ReplayDecoder.cs:** This class is the core of the decoder. It takes a `.bsor` file, reads the binary data, and converts it into C# objects (such as `ReplayData`, `Block`, and `Player`).
- **ReplayData.cs:** Contains all the replay information, including player stats, blocks, timestamps, and other relevant data.
- **Block.cs:** Represents a block in Beat Saber with properties like type, position, direction, and timing.
- **Player.cs:** Contains information about the player’s gameplay, including score, accuracy, combo count, and timing information.

### ReplayData Class

The `ReplayData` class contains key details about the replay, including:

- `Score:` The final score the player earned.
- `Player:` A `Player` object containing specific player data.
- `Blocks:` A list of `Block` objects, each representing a single block in the replay.
- `TimeStamps:` A list of time stamps marking the exact timing of events during the replay.

### Block Class

The `Block` class contains the following properties:

- `Type:` The type of block (e.g., red, blue, bomb).
- `Position:` The 3D position of the block in the game world.
- `Direction:` The direction in which the block is moving (e.g., left, right, up, down).
- `Timing:` The time at which the block needs to be hit.

### Player Class

The `Player` class contains data such as:

- `Score:` The score the player achieved in the replay.
- `Accuracy:` The accuracy percentage of the player (based on blocks hit vs. missed).
- `ComboCount:` The highest combo the player achieved during the replay.

## How it Works

### Replay File Parsing

1. **Binary Data Parsing:** BLReplayDecoder reads the binary `.bsor` file and processes each byte according to the expected format. This is done using custom deserialization methods that map byte sequences to appropriate data types (e.g., integers, floats, and strings).
2. **Data Extraction:** After reading the binary data, the parser extracts useful information such as block data, player stats, and timestamps.
3. **Error Handling:** If any errors occur during parsing (e.g., corrupted files or invalid formats), **BLReplayDecoder** logs the error and provides a detailed error message, allowing for graceful failure rather than crashing.
   
### Data Structures

The main data structures used by BLReplayDecoder include:
- **ReplayData:** Contains the full replay data, including the player information and block list.
- **Block:** Represents a single block, with properties like type, position, direction, and timing.
- **Player:** Contains player-specific stats like score, accuracy, combo count, and more.

### AI Training and Data Analysis

The extracted replay data can be used for various purposes, such as:
- **AI Training:** By analyzing block patterns and player actions, you can use the data to train an AI model that learns how to play Beat Saber. This could involve training a neural network or reinforcement learning agent to predict block timing, direction, and type.
- **Data Analysis:** The replay data provides insights into player behavior, such as which blocks are often missed, the player’s performance over time, or even identifying difficult sections in a custom map.

### Error Handling

- **Invalid File Format:** If a replay file does not conform to the expected `.bsor` format, the library throws a detailed error message explaining where the issue occurred.
- **Corrupted Data:** If a replay file contains corrupted or missing data, the library gracefully handles it and logs a clear error, allowing users to debug the problem.

### Optimizations

The decoding process is optimized for both speed and memory efficiency:
- **Buffered I/O:** The use of buffered I/O operations ensures that the replay file is read efficiently.
- **Memory Efficiency:** Large replay files are parsed in chunks, and unnecessary memory allocations are avoided, ensuring the library works even with large replay datasets.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Copyright (c) 2025 cybermura
