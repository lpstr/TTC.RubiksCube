using Microsoft.Extensions.DependencyInjection;
using TTC.RubiksCube.Contracts;
using TTC.RubiksCube.Domain;
using TTC.RubiksCube.Factories;
using TTC.RubiksCube.Rendering;

var services = new ServiceCollection();

services.AddSingleton<Cube>();

services.AddSingleton<ICubeRendering, ConsoleCubeRenderer>();

var serviceProvider = services.BuildServiceProvider();

var cube = serviceProvider.GetRequiredService<Cube>();
var renderer = serviceProvider.GetRequiredService<ICubeRendering>();



while (true)
{
    renderer.Print(cube);

    Console.Write("Move: ");

    var key = Console.ReadKey(true).KeyChar;

    switch (char.ToUpperInvariant(key))
    {
        case 'X':
            return;

        case 'Z':
            cube.Undo();
            continue;

        case 'T':
            cube.ExecuteTtcChallenge();
            continue;
    }

    var move = MoveFactory.FromKey(key);

    if (move is not null)
    {
        cube.Rotate(move);
    }
}