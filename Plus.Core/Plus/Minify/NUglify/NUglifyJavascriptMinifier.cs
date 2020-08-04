using NUglify;
using Plus.Minify.Scripts;

namespace Plus.Minify.NUglify
{
    public class NUglifyJavascriptMinifier : NUglifyMinifierBase, IJavascriptMinifier
    {
        protected override UglifyResult UglifySource(string source, string fileName)
        {
            return Uglify.Js(source, fileName);
        }
    }
}