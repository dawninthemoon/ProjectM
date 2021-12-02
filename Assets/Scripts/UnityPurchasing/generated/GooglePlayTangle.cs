// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("6Gtlalroa2Bo6GtravewZrz3yX84OSmESQiJnsZ0veIEWQ0cNvV8G4v3bjw6hVlLCQFWmyB2h7eOj8JKWuhrSFpnbGNA7CLsnWdra2tvamlMfpmguj4n2ejMuy2X1nvC+oBoNZ9p0kPEaF7p8i13Uk1e3LU57s9rV323ybPYYVyN2YHPq6eNYj28/kMNUyZNS39AWJKPT5Nd6FpaoNSzDLtVgsaZynag1gyH/a6NvaHDqqMigo8eZfh3LTXsa+MzHffxccOjXgN2Ge6++p7axvKgf2B/F1GZYbVjV7XvAjey/36Li/F39cfiouQA22hB3euhBcP4GgmW4llpznTjUxus2lrN0SwjvsaS5vbUFb5s01uCwZZx62I9xjpqtkwp0Whpa2pr");
        private static int[] order = new int[] { 1,13,12,13,4,10,7,8,12,12,12,13,12,13,14 };
        private static int key = 106;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
