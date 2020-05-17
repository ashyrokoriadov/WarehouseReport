using System.Collections.Generic;

namespace WarehouseReport.Input.Reading
{
    public interface  IInputReader
    {
        IEnumerable<string> Read();
    }
}
