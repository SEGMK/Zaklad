using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.CustomControls
{
    public class ButtonWithDecision : Button
    {
        public bool RedirectToEarlierPage { get; set; } = false;
    }
}
