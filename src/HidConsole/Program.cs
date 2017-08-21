namespace HidConsole
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using HidLights;
    using HidLights.Devices;

    public class Program
    {
        public static void Main(string[] args)
        {
            var color = ReadColor(args);
            if (color == null)
            {
                var fileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                Console.WriteLine($"Usage:{Environment.NewLine}" +
                                  $"{fileName} R G B{Environment.NewLine}" +
                                  "Where R, G, B are 0-255.");
                return;
            }

            SetColor((Color)color);
        }

        private static Color? ReadColor(string[] args)
        {
            var components = new byte[3];

            if (args.Length != components.Length)
                return null;

            for (var i = 0; i < components.Length; i++)
                if (!byte.TryParse(args[i], out components[i]))
                    return null;

            return Color.FromArgb(components[0], components[1], components[2]);
        }

        private static void SetColor(Color color)
        {
            var plumbobs = Device.FindDevices<Plumbob>();
            if (plumbobs == null)
            {
                Console.WriteLine("Plumbob not found!");
                return;
            }

            foreach (Plumbob plumbob in plumbobs)
            {
                plumbob.Color = color;
            }
        }
    }
}
