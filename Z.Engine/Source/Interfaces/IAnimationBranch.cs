using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Z.Engine.Source.Interfaces
{
    public interface IAnimationBranch
    {
        public IAnimationState TransitionsTo { get; set; }
        public bool IsSatisfied();

    }
}
