namespace BasicChromeDriverDetectionPatch
{
    internal class BinHelper
    {
        public static List<int> FindOffsets(byte[]? source, byte[] find)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var result = new List<int>();
            int matchIndex = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        result.Add(i - matchIndex);
                        continue;
                    }

                    matchIndex++;
                }
                else if (source[i] == find[0]) matchIndex = 1;
                else matchIndex = 0;
            }

            return result;
        }

        public static byte[] Replace(byte[] source, byte[] search, byte[] replace)
        {
            byte[]? dst = null;
            byte[]? temp = null;

            var offsets = FindOffsets(source, search);

            foreach (var index in offsets)
            {
                temp = temp == null ? source : (dst ?? source);
                dst = new byte[temp.Length - search.Length + replace.Length];

                Buffer.BlockCopy(temp, 0, dst, 0, index);
                Buffer.BlockCopy(replace, 0, dst, index, replace.Length);

                Buffer.BlockCopy(
                    temp,
                    index + search.Length,
                    dst,
                    index + replace.Length,
                    temp.Length - (index + search.Length));
            }

            return dst ?? source;
        }
    }
}
