using System.Linq;

namespace WarehouseReport.Input.Mapping
{
    public abstract class InputMapper<TResult> : IMapper<string, TResult>
    {
        protected string[] SplitInput { get; set; }

        public virtual TResult Map(string input)
        {
            Split(input);
            return ValidateSplitInput() ? Map() : default(TResult);
        }

        protected abstract void Split(string input);

        protected virtual bool ValidateSplitInput() => SplitInput.Any();

        protected abstract TResult Map();
    }
}
