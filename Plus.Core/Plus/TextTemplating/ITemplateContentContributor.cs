using System.Threading.Tasks;

namespace Plus.TextTemplating
{
    public interface ITemplateContentContributor
    {
        Task<string> GetOrNullAsync(TemplateContentContributorContext context);
    }
}