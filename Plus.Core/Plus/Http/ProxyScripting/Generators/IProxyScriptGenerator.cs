using Plus.Http.Modeling;

namespace Plus.Http.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}