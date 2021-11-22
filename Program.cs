using System;
using System.Text;
using System.Threading;

namespace FlappyBird
{

    public struct Vector2f
    {
        public double x, y;
    }

    class Program
    {
        static void Render(double deltatime, ref Bird bird)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            char[,] buffer = new char[height, width];

            buffer[Convert.ToInt32(Math.Floor(bird.pos.y)), Convert.ToInt32(Math.Floor(bird.pos.x))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y)), Convert.ToInt32(Math.Floor(bird.pos.x + 1))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y)), Convert.ToInt32(Math.Floor(bird.pos.x + 2))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y)), Convert.ToInt32(Math.Floor(bird.pos.x + 3))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y)), Convert.ToInt32(Math.Floor(bird.pos.x + 4))] = '\\';

            buffer[Convert.ToInt32(Math.Floor(bird.pos.y + 1)), Convert.ToInt32(Math.Floor(bird.pos.x))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y + 1)), Convert.ToInt32(Math.Floor(bird.pos.x + 1))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y + 1)), Convert.ToInt32(Math.Floor(bird.pos.x + 2))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y + 1)), Convert.ToInt32(Math.Floor(bird.pos.x + 3))] = '–';
            buffer[Convert.ToInt32(Math.Floor(bird.pos.y + 1)), Convert.ToInt32(Math.Floor(bird.pos.x + 4))] = '/';


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i<height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (buffer[i, j] == default(char))
                    {
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(buffer[i, j]);
                    }
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
        public static void Main()
        {
            Bird bird = new Bird(Console.WindowWidth / 5, Console.WindowHeight / 2);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Hallo");

            while (true)
            {
                watch.Stop();
                watch.Start();
                Thread.Sleep(50);
                bird.update(watch.ElapsedMilliseconds);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(false).Key;
                    if (ConsoleKey.Spacebar == key)
                    {
                        bird.push();
                    }
                }
                Render(watch.ElapsedMilliseconds, ref bird);
            }
        }
    }

    public class Bird
    {
        public Vector2f pos;
        double velocity = 1;

        public Bird(double x, double y)
        {
            this.pos.x = x;
            this.pos.y = y;
        }

        void update_velocity(double deltatime)
        {
            double a = 1;
            this.velocity = a * Math.Abs(this.velocity) ;
        }

        public void update(double deltatime)
        {
            update_velocity(deltatime);
            if (this.pos.y < Console.WindowHeight - 2)
            {
                this.pos.y = this.pos.y + velocity;
                if (this.pos.y > Console.WindowHeight - 2)
                {
                    this.pos.y = Console.WindowHeight - 2;
                }
            }
        }

        public void push()
        {
            this.pos.y = this.pos.y - 10;
            this.velocity = 1;
            if (this.pos.y < 0)
            {
                this.pos.y = 0;
            }
        }
    }

}