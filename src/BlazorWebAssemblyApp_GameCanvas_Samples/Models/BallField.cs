
namespace BlazorWebAssemblyApp_GameCanvas_Samples.Models;

public class BallField
{
    public readonly Ball[] Balls;

    public BallField(int ballCount)
    {
        Balls = new Ball[ballCount];
    }

    public void Advance(double timeDelta, double width, double height)
    {
        foreach (Ball ball in Balls)
        {
            ball?.Advance(timeDelta, width, height);
        }
    }

    public void Randomize(double width, double height)
    {
        Random rand = new();
        for (int i = 0; i < Balls.Length; i++)
        {
            Balls[i] = new()
            {
                X = rand.NextDouble() * width,
                Y = rand.NextDouble() * height,
                Radius = rand.NextDouble() * 5 + 5,
                XVel = rand.NextDouble() - .5,
                YVel = rand.NextDouble() - .5,
                R = (byte)rand.Next(50, 255),
                G = (byte)rand.Next(50, 255),
                B = (byte)rand.Next(50, 255),
            };
        }
    }
}