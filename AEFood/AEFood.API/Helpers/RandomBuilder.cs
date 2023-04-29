using System.Security.Cryptography;

namespace AEFood.API.Helpers;

public class RandomBuilder : RandomNumberGenerator
{
    private readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();


    public int Next(int maxValue)
    {
        return (int)Math.Floor((0 + (double)maxValue) * NextDouble());
    }

    public double NextDouble()
    {
        var data = new byte[sizeof(uint)];
        rng.GetBytes(data);
        var randUint = BitConverter.ToUInt32(data, 0);
        return randUint / (uint.MaxValue + 1.0);
    }

    public override void GetBytes(byte[] data)
    {
        rng.GetBytes(data);
    }

    public override void GetNonZeroBytes(byte[] data)
    {
        rng.GetNonZeroBytes(data);
    }
}
