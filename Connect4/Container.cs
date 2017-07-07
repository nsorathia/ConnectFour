using System;
using Connect4.Interfaces;
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
                .BuildServiceProvider();

        }


        public static T GetObject<T>()
        {
            var service = _instance.GetService<T>();
            return service;
        }

    }
}
