// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("8Z5pOX0ZXUF1J/jn+JDWHuYy5NC/vq4Dzo8OGUHzOmWD3oqbsXL7nDzSBUEeTfEnUYsAeikKOiZELSSlWmwmgkR/nY4RZd7uSfNk1JwrXd2K1KHKzPjH3xUIyBTab93dJ1M0i0pWq6Q5QRVhcVOSOetU3AVGEfZsGO5VxEPv2W51qvDVytlbMr5pSOwFCJnif/CqsmvsZLSacHb2RCTZhAxw6bu9At7MjobRHKfxADAJCEXN0PowTjRf5tsKXgZILCAK5bo7ecQyaIWwNXj5DAx28HJAZSVjh1zvxt1v7M/d4Ovkx2ulaxrg7Ozs6O3uy/keJz25oF5vSzyqEFH8RX0H77Jv7OLt3W/s5+9v7OztcDfhO3BO+OW6Qb3tMcuuVu/u7O3s");
        private static int[] order = new int[] { 9,13,2,5,8,11,10,10,12,10,11,11,12,13,14 };
        private static int key = 237;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
