using System;
using BlockBase.BoidVoid.Data.Contexts;
using BlockBase.BoidVoid.Data.Model.Games;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new BoidVoidContext();
            context.PlayerEntries.BatchInsert(new PlayerEntry());
            context.Turns.BatchInsert(new Turn());
            context.ExecuteQueryBatchAsync().Wait();
        }
    }
}
