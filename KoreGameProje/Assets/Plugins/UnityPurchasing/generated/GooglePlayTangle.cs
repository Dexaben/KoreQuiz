#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("M2L1+T+BBPi9ad72WP0b9LY2DoDkBar/YesUD4s2vfJn47QcVVOeCasoJikZqygjK6soKCmiIIPJAHGrEtcntJbVRYEcWdY5kc7i/0uILfZFlRHEuhJJJJT/bG5z/NJ0Vt9bZhmrKAsZJC8gA69hr94kKCgoLCkqVxt2Fw0FuDAAWPkajnTLldQ8D9nrLD7OiEZYzVNciXbVEGNpIH3UcV33HqOkfjnaEykK6HAcHNtkwIq/oQLRdYmY9bWBYZpU814mbNnWvz2M4Q8T4IeC096457sBxtaUCmtaAZAYzS3fB5H+1A4IC2oL5i2oxOe9w0FUzHnL6MPOu6ohrPDsKmEt1QakXnNY4EE+621EgnSjqO8nZqnLf1u9BO4Y+EbFRisqKCko");
        private static int[] order = new int[] { 9,13,13,11,12,9,9,11,12,12,11,12,13,13,14 };
        private static int key = 41;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
