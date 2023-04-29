using System.Security.Cryptography;
using System.Text;

namespace AEFood.API.Helpers;
public class AEFoodCodeBuilder
{
    private const string Cluster = "ACDEFGHKLMNPRTXYZ234579";
    private readonly RandomNumberGenerator RandomNumberGenerator;
    private char[] ClusterChar = Cluster.ToCharArray();
    private readonly RandomBuilder rng;

    public AEFoodCodeBuilder()
    {
        rng = new RandomBuilder();
        RandomNumberGenerator = new RandomBuilder();
    }

    public string Generate()
    {
        StringBuilder sb = new();
        for (var i = 0; i < 7; i++)
        {
            sb.Append(GetRandomChar());
        }

        sb.Append(CheckAlgorithm(sb.ToString()));

        return string.Concat("Ürün Kodu : ", sb.ToString());
    }

    public string Validate(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new Exception("Provide a code to be validated");


        code = new string(Array.FindAll(code.ToCharArray(), char.IsLetterOrDigit)).ToUpper();

        if (code.Length != 8)
            return "Kod uzunluğu hatalı!";

        if (!CheckClusterChar(code))
            return "Geçersiz kod!";

        var data = code.Substring(0, 7);
        var check = code.Substring(7, 1);

        if (Convert.ToChar(check) != CheckAlgorithm(data))
            return "Kod doğrulanamadı!";

        return $"Kampanyadan yararlanabilirsiniz. Code : {code}";
    }

    private char GetRandomChar()
    {
        var pos = rng.Next(ClusterChar.Length);
        return ClusterChar[pos];
    }

    private char CheckAlgorithm(string data)
    {
        var ClusterChar = data.ToCharArray();
        long check = 1;
        foreach (var item in ClusterChar)
        {
            check = check * Convert.ToChar(item);
        }

        return ClusterChar[check % (ClusterChar.Length - 1)];
    }

    private bool CheckClusterChar(string code)
    {
        var chars = code.ToCharArray();

            return Enumerable.Range(0, chars.Length)
                .All(i => ClusterChar.Any(c => c == chars[i]));
    }
}