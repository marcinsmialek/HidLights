namespace HidLights
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class DevicePropertiesAttribute : Attribute
    {
        public DevicePropertiesAttribute(int vendorId, int productId)
        {
            VendorId = vendorId;
            ProductId = productId;
        }

        public int VendorId { get; }
        public int ProductId { get; }
    }
}
