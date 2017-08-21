namespace HidLights.Devices
{
    using System.Drawing;
    using HidLibrary;

    [DeviceProperties(0x09DA, 0x55CB)]
    public class BloodyT70 : Device
    {
        public BloodyT70(HidDevice device) : base(device)
        {
        }

        protected override byte[] GetColorCommandBytes(Color color)
        {
            return new byte[]
            {
                7,
                0x18,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                color.R,
                color.G,
                color.B
            };
        }
    }
}
