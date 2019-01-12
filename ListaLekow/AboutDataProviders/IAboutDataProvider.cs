using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaLekow.AboutDataProviders
{
    public interface IAboutDataProvider
    {
        string Title
        {
            get;
        }

        string Version
        {
            get;
        }

        string Description
        {
            get;
        }

        string Product
        {
            get;
        }

        string Copyright
        {
            get;
        }

        string Company
        {
            get;
        }

        string LinkText
        {
            get;
        }

        string LinkUri
        {
            get;
        }

    }
}
