namespace PRDH.Extensions
{
    public static class StringExtensions
    {
        public static bool Eq(this string src, string dest) {
        
            return string.Equals(src, dest, StringComparison.InvariantCultureIgnoreCase);
        } 
    }
}
