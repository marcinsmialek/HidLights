namespace HidLights.Devices
{
    using System.Drawing;
    using HidLibrary;

    [DeviceProperties(0x1038, 0x1500)]
    public class Plumbob : Device
    {
        public Plumbob(HidDevice device) : base(device)
        {
        }

        protected override byte[] GetColorCommandBytes(Color color)
        {
            return new byte[]
            {
                0,
                7,
                0,
                color.R,
                color.G,
                color.B
            };
        }
    }
}
