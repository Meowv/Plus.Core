using NUglify;
using Plus.Minify.Styles;

namespace Plus.Minify.NUglify
{
    public class NUglifyCssMinifier : NUglifyMinifierBase, ICssMinifier
    {
        protected override UglifyResult UglifySource(string source, string fileName)
        {
            return Uglify.Css(source, fileName);
        }
    }
}