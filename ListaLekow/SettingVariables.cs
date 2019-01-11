using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaLekow
{
    [Serializable]
    public class SettingVariables
    {

        public string DataFileLocation { get; set; }
        public bool UseDefaultDataLocation { get; set; }

        public SettingVariables(string datafilelocation, bool usedefaultdatalocation)
        {
            DataFileLocation = datafilelocation;
            UseDefaultDataLocation = usedefaultdatalocation;
        }

    }
}
