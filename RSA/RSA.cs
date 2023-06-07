using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace RSA
{
    class RSA
    {
        public void CompressFile(string dataFileName, string archFileName, int p, int q)
        {
            byte[] data = File.ReadAllBytes(dataFileName);
            byte[] arch = Compress(data, p, q);
            File.WriteAllBytes(archFileName, arch);
        }

        private byte[] Compress(byte[] data, int p, int q)
        {
            int m = p * q;
            int fun_el = (p - 1) * (q - 1);
            int e = 2;
            while (NOD(e, fun_el) != 1)
                e++;
            int d = 1;
            int ed = e * d % fun_el;
            while(ed != 1)
            {
                d++;
                ed = e * d % fun_el;
            }

            File.WriteAllText("keys.txt", e.ToString() + " " + m.ToString() + "\n" + d.ToString() + " " + m.ToString());

            for(int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(BigInteger.Pow(data[i], e) % m);
            }
            return data;
        }

        private int NOD(int e, int fun_el)
        {
            while(fun_el > 0)
            {
                int a = e % fun_el;
                e = fun_el;
                fun_el = a;
            }
            return e;
        }

        public void DecompressFile(string archFileName, string dataFileName, int m, int d)
        {
            byte[] data = File.ReadAllBytes(archFileName);
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(BigInteger.Pow(data[i], d) % m);
            }
            File.WriteAllBytes(dataFileName, data);
        }


        public void createSignature(string dataFileName, int d, int m)
        {
            byte[] data = File.ReadAllBytes(dataFileName);
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)(BigInteger.Pow(data[i], d) % m);
            File.WriteAllBytes("signature.txt", data);
        }

        public bool signatureVerification(string dataFileName, string signatureFileName, int e, int m)
        {
            byte[] data = File.ReadAllBytes(dataFileName);
            byte[] signature = File.ReadAllBytes(signatureFileName);
            for (int i = 0; i < signature.Length; i++)
            {
                signature[i] = (byte)(BigInteger.Pow(signature[i], e) % m);
                if (signature[i] != data[i])
                    return false;
            }
            return true;
        }
    }
}
