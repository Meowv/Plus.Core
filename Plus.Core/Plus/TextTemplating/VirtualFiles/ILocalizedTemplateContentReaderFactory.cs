using System.Threading.Tasks;

namespace Plus.TextTemplating.VirtualFiles
{
    public interface ILocalizedTemplateContentReaderFactory
    {
        Task<ILocalizedTemplateContentReader> CreateAsync(TemplateDefinition templateDefinition);
    }
}