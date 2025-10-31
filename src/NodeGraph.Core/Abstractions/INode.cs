using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeGraph.Core.Contexts;

namespace NodeGraph.Core.Abstractions
{
    public interface INode
    {
        string Name { get; }
        void Evaluate(Context context);
    }

}
