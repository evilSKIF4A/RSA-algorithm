using System;

namespace RSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RSA rsa = new RSA();

            int p = 13, q = 19;
            rsa.CompressFile("data.txt", "arch_data.bin", p, q);

            int d = 173, m = 247;
            rsa.DecompressFile("arch_data.bin", "dearch_data.txt", m, d);

            int e = 5;
            //rsa.createSignature("data.txt", d, m);
            //Console.WriteLine(rsa.signatureVerification("data.txt", "signature.txt", e, m));
        }
    }
}
