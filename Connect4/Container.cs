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
                .AddTransient<IGame, TwoPersonGame>()
                .AddTransient<IBoard, Connect4Board>()
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
