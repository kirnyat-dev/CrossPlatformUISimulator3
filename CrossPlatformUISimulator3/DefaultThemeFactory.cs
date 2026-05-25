using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class DefaultThemeFactory : IThemeFactory
    {
        public string GetThemeName() => "DefaultTheme";
    }
}