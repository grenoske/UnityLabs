using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputReader.Interfaces
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Attack { get; }    
        void ResetOneTimeAction();
    }
}
