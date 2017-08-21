namespace HidLights
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using HidLibrary;

    public abstract class Device
    {
        protected readonly HidDevice device;

        protected Device(HidDevice device)
        {
            this.device = device;
        }

        public string ManufacturerName
        {
            get
            {
                device.ReadManufacturer(out byte[] buffer);
                return GetString(buffer);
            }
        }

        public string ProductName
        {
            get
            {
                device.ReadProduct(out byte[] buffer);
                return GetString(buffer);
            }
        }

        public virtual Color Color
        {
            set => device.WriteFeatureData(GetColorCommandBytes(value));
        }

        public static IEnumerable<T> FindDevices<T>()
            where T : Device
        {
            DevicePropertiesAttribute properties =
                typeof(T).GetCustomAttributes(typeof(DevicePropertiesAttribute), true).FirstOrDefault() as
                    DevicePropertiesAttribute ??
                throw new Exception($"Missing {nameof(DevicePropertiesAttribute)} attribute");

            return FindDevices<T>(properties.VendorId, properties.ProductId);
        }

        public static IEnumerable<T> FindDevices<T>(int vendorId, int productId)
            where T : Device
        {
            return FindHidDevices(vendorId, productId).
                Select(d => (T)Activator.CreateInstance(typeof(T), d));
        }

        protected abstract byte[] GetColorCommandBytes(Color color);

        private static IEnumerable<HidDevice> FindHidDevices(int vendorId, int productId)
        {
            return HidDevices.Enumerate()
                .Where(d =>
                    d.Attributes.VendorId == vendorId &&
                    d.Attributes.ProductId == productId);
        }

        private string GetString(byte[] buffer)
        {
            return Encoding.Unicode.GetString(buffer).TrimEnd('\0');
        }
    }
}
