using FluentAssertions;
using Battleship;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Battleship.Tests;

public class UnitTests
{
    [Fact]
    public void GetShips_should_return_lists_of_coordinates()
    {
        // Arrange
        SetInput("test-ships.in");
        int height = int.Parse(Program.ReadLineAndSplit()[1]);
        var game = new BattleShip(height, -1);

        // Act
        var shipsPlayerOne = game.GetShips();

        // Assert
        shipsPlayerOne.Count().Should().Be(1);
        shipsPlayerOne.First().X.Should().Be(0);
        shipsPlayerOne.First().Y.Should().Be(1);
    }

    [Fact]
    public void GetShips_should_handle_for_two_players()
    {
        // Arrange
        SetInput("test-ships.in");
        int height = int.Parse(Program.ReadLineAndSplit()[1]);
        var game = new BattleShip(height, -1);

        // Act
        var shipsPlayerOne = game.GetShips();
        var shipsPlayerTwo = game.GetShips();


        // Assert
        shipsPlayerOne.Count().Should().Be(1);
        shipsPlayerOne.First().Should().Be(new Coordinate(0, 1));

        shipsPlayerTwo.Count().Should().Be(2);
        shipsPlayerTwo.First().Should().Be(new Coordinate(0, 3));
        shipsPlayerTwo.Last().Should().Be(new Coordinate(3, 2));
    }

    [Fact]
    public void Play_should_return_player_one_wins()
    {
        // Arrange
        SetInput("player-one-wins.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("player one wins");
    }

    [Fact]
    public void Play_should_return_player_two_wins()
    {
        // Arrange
        SetInput("player-two-wins.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("player two wins");
    }

    [Fact]
    public void Play_should_return_draw_if_both_players_have_ships()
    {
        // Arrange
        SetInput("draw-both-have-ships.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("draw");
    }

    [Fact]
    public void Play_should_return_draw_if_neither_player_have_ships()
    {
        // Arrange
        SetInput("draw-neither-have-ships.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("draw");
    }

    [Fact]
    public void Play_should_not_read_shots_after_configured_max()
    {
        // Arrange
        SetInput("draw-has-more-shots-than-configured-max.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("draw");
    }

    [Fact]
    public void Play_should_not_read_shots_after_player_one_wins()
    {
        // Arrange
        SetInput("player-one-wins-should-not-keep-reading-shots.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("player one wins");
    }

    [Fact]
    public void Play_should_not_read_shots_after_player_two_wins()
    {
        // Arrange
        SetInput("player-two-wins-should-not-keep-reading-shots.in");
        string[] gameParameters = Program.ReadLineAndSplit();
        var game = new BattleShip(int.Parse(gameParameters[1]),
                                  int.Parse(gameParameters[2]));

        // Act
        var result = game.Play();

        // Assert
        result.Should().Be("player two wins");
    }

    [Fact]
    public void Program_should_handle_multiple_tests_with_superfluous_shots()
    {
        // Arrange
        SetInput("draw-multiple-tests-with-superfluous-shots.in");
        int testCases = int.Parse(Program.ReadLineAndSplit()[0]);

        var results = new string[testCases];

        // Act & Assert
        try
        {
            for (var test = 0; test < testCases; test++)
            {
                string[] gameParameters = Program.ReadLineAndSplit();
                var game = new BattleShip(int.Parse(gameParameters[1]),
                                          int.Parse(gameParameters[2]));
                results[test] = game.Play();
            }
        }
        catch (Exception e)
        {
            Assert.True(false, "Expected no exception to be thrown but got: " + e.Message);
        }

        results[0].Should().Be("player two wins");
        results[1].Should().Be("draw");
        results[2].Should().Be("player two wins");

    }

    private void SetInput(string fileName)
    {
        var sourceFile = new FileInfo($"{Directory.GetCurrentDirectory()}/{fileName}");
        var sourceFileReader = new StreamReader(sourceFile.FullName);
        Console.SetIn(sourceFileReader);
    }
}