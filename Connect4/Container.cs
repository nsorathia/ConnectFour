﻿using System;
using Connect4.Interfaces;
using Connect4.Player;
using Connect4.Game;
using Connect4.Board;
using Connect4.Algorithm;
using Microsoft.Extensions.DependencyInjection;

namespace Connect4
{
    public static class Container
    {
        private static IServiceProvider _instance;

        static Container()
        {
            _instance = new ServiceCollection()
                .AddTransient<IGame, OnePlayerGame>()
                //.AddTransient<IGame, TwoPlayerGame>()
                .AddTransient<IBoard, Connect4Board>()
                .AddSingleton<IDataDevice, ConsoleDevice>()
                .AddTransient<IPlayer, Connect4Player>()
                .AddSingleton<IAlgorithm, MinMaxAlgorithm>()
                .BuildServiceProvider();
        }


        public static T GetObject<T>()
        {
            var service = _instance.GetService<T>();
            return service;
        }

    }
}
