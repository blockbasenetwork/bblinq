using System;
using BlockBase.BBLinq.Contexts;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace Test
{

    public class A
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Unique]
        public string TestA { get; set; }

    }


    public class TestCtx : BlockBaseContext
    {
        public TestCtx(BlockBaseSettings settings) : base(settings)
        {
        }

        public BlockBaseSet<A> As { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using var ctx = new TestCtx(new BlockBaseSettings()
            {
                DatabaseName = "Test",
                Host = "https://20.51.248.230:5000",
                Password = "5JEJ5TtiEtyoWAUiBF2iTiMcji9yZ6nBqTJy79mE54a7MHaR1WE",
                UserAccount = "bbtestacc124",
            });
            ctx.DropDatabase().Wait();
            ctx.CreateDatabase().Wait();
            try
            {
                ctx.As.InsertAsync(new A() {Id = Guid.NewGuid(), TestA = "A"}).Wait();
                ctx.As.InsertAsync(new A() {Id = Guid.NewGuid(), TestA = "B"}).Wait();
               ctx.As.InsertAsync(new A() {Id = Guid.NewGuid(), TestA = "A"}).Wait();
            }
            catch (Exception e)
            {

            }

        }
    }
}
