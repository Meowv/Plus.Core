namespace Plus.Validation
{
    public interface IObjectValidationContributor
    {
        void AddErrors(ObjectValidationContext context);
    }
}