# Rubik's Cube Simulator (TTC Technical Challenge)
 
## Overview
 
This project is a console-based Rubik's Cube simulator developed as part of the TTC technical challenge.
 
The application simulates a standard 3x3 Rubik's Cube and supports clockwise and counter-clockwise rotations of all six faces.
 
The cube starts in the required solved state:
 
- Front: Green
- Right: Red
- Up: White
 
matching the orientation specified in the challenge and used by the Ruwix online Rubik's Cube solver.
 
The application does not solve the cube. Its purpose is to correctly model cube state and rotations.
 
---
 
## Features
 
### Cube Operations
 
Supported face rotations:
 
| Face | Clockwise | Counter-Clockwise |
|--------|--------|--------|
| Front | F | f |
| Right | R | r |
| Up | U | u |
| Back | B | b |
| Left | L | l |
| Down | D | d |
 
### Additional Features
 
- Interactive console controls
- ANSI colorized cube rendering
- Undo last move (`Z`)
- Execute TTC challenge sequence (`T`)
- Dependency Injection using Microsoft.Extensions.DependencyInjection
- Separation of domain and rendering concerns
- Move abstraction using `Move` record and `RotationDirection` enum
 
---
 
## Project Structure
 
```text
TTC.RubiksCube
│
├── Contracts
│ └── ICubeRenderer.cs
│
├── Domain
│ ├── Cube.cs
│ ├── Face.cs
│ ├── Color.cs
│ ├── Move.cs
│ └── RotationDirection.cs
│
├── Factories
│ └── MoveFactory.cs
│
├── Rendering
│ └── ConsoleCubeRenderer.cs
│
└── Program.cs
```
 
---
 
## Requirements
 
- .NET 10 SDK
- Windows Console
- No additional software required
 
---
 
## Build Instructions
 
Clone the repository:
 
```bash
git clone https://github.com/<your-account>/TTC.RubiksCube.git
```
 
Navigate to the project directory:
 
```bash
cd TTC.RubiksCube
```
 
Restore packages:
 
```bash
dotnet restore
```
 
Build:
 
```bash
dotnet build
```
 
---
 
## Run Instructions
 
Execute:
 
```bash
dotnet run
```
 
or
 
```bash
dotnet run --project TTC.RubiksCube
```
 
---
 
## Controls
 
| Key | Action |
|------|--------|
| F | Front clockwise |
| f | Front counter-clockwise |
| R | Right clockwise |
| r | Right counter-clockwise |
| U | Up clockwise |
| u | Up counter-clockwise |
| B | Back clockwise |
| b | Back counter-clockwise |
| L | Left clockwise |
| l | Left counter-clockwise |
| D | Down clockwise |
| d | Down counter-clockwise |
| Z | Undo last move |
| T | Execute TTC challenge sequence |
| X | Exit application |
 
---
 
## TTC Challenge Sequence
 
The challenge sequence:
 
```text
F R' U B' L D'
```
 
can be executed automatically by pressing:
 
```text
T
```
 
This corresponds to:
 
```text
F r U b L d
```
 
using the application's keyboard controls.
 
---
 
## Design Notes
 
The solution focuses on:
 
- Readability
- Maintainability
- Separation of concerns
- Testability
- Clear domain modeling
 
The cube state and rotation logic are contained within the domain layer, while console rendering responsibilities are isolated in a dedicated renderer implementation.
 
---
 
## Author
 
Bogdan Bekyarov
