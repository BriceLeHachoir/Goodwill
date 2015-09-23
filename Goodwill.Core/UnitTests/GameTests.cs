﻿using System.Linq;
using NFluent;
using Xunit;

namespace UnitTests
{
    public class GameTests
    {
        [Fact]
        public void Default_config_for_companies()
        {
            var defaultGameParameters = new DefaultGameParameters();
            var game = new Goodwill(defaultGameParameters);
            var player1 = game.AddPlayer("Player 1");
            var player2 = game.AddPlayer("Player 2");
            var player3 = game.AddPlayer("Player 3");

            game.Start();
            
            Check.That(game.Companies.Extracting("Name")).ContainsExactly(defaultGameParameters.Companies);
            Check.That(game.Companies.Extracting("MarketPart")).ContainsExactly(35, 35, 30);
            Check.That(game.Companies.Extracting("Money")).ContainsExactly(100, 100, 110);
            Check.That(player1.Money).IsEqualTo(20);
            Check.That(player1.Actions.Count).IsEqualTo(10);
            Check.That(player2.Money).IsEqualTo(20);
            Check.That(player2.Actions.Count).IsEqualTo(10);
            Check.That(player3.Money).IsEqualTo(20);
            Check.That(player3.Actions.Count).IsEqualTo(10);
        }


        [Fact]
        public void Default_config_for_players()
        {
            var defaultGameParameters = new DefaultGameParameters();
            var game = new Goodwill(defaultGameParameters);
            game.AddPlayer("Player 1");
            game.AddPlayer("Player 2");
            game.AddPlayer("Player 3");
            game.AddPlayer("Player 4");

            game.Start();

            
            Check.That(game.Companies.Extracting("Name")).ContainsExactly(defaultGameParameters.Companies);
            Check.That(game.Companies.Extracting("MarketPart")).ContainsExactly(35, 35, 30);
            Check.That(game.Companies.Extracting("Money")).ContainsExactly(100, 100, 110);

            foreach (var player in game.Players)
            {
                if (player.Money == 20)
                {
                    Check.That(player.Money).IsEqualTo(20);
                    Check.That(player.Actions.Count).IsEqualTo(8);
                }
                else
                {
                    Check.That(player.Money).IsEqualTo(33);
                    Check.That(player.Actions.Count).IsEqualTo(7);
                }
            }
            
            Check.That(game.Players.SelectMany(x=>x.Actions)).HasSize(30);
        }
    }
}
