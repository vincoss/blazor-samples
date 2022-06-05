namespace BlazorWebAssemblyApp_GameCanvas_Samples.Model
{
    public class SampleElevator
    {
        public void Run()
        {
            var elevator = new Elevator
            {
                Height = 1000
            };

            elevator.Build();


            /*
             00000 00000 00000 00000 00000 00000
               0
              0
             0
            */

        }


    }    


    public class Elevator // escalator
    {
        // x = 0, x = max
        // y = 0, y = max
        public int Height { get; set; }

        public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();
    }

    public class Item
    {
        public int X { get; set; }
        public int Y { get; set; } = 0;

        public int Height { get; set; } = 100; // this must be proportional to screen size
        public int Width { get; set; } = 100;// this must be proportional to screen size

        public void IncrementY()
        {
            Y++;
        }

        public static IEnumerable<Item> Start()
        {
            return new[]
            {
                new Item()
            };
        }
    }

    public static class Extensions
    {
        public static Elevator Build(this Elevator elevator)
        {
            var itemSize = 100;
            var count = elevator.Height / itemSize;

            for(int i = 0; i < itemSize; i++)
            {
               
            }
            return elevator;
        }

        public static Item Reset(this Item item)
        {
            return item;
        }
    }

}
