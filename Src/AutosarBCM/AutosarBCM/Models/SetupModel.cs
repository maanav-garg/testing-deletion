using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Models
{
    [Serializable]
    public class SetupModel
    {
        public string Name { get; set; }

        public IntrepidSetupModel IntrepidSetup { get; set; } = new IntrepidSetupModel();
        public SerialPortSetupModel VectorSetup { get; set; } = new SerialPortSetupModel();
        public VectorSetupModel SerialPortSetupSetup { get; set; } = new VectorSetupModel();
    }

    [Serializable]
    public class IntrepidSetupModel 
    { 
        public int NetworkId { get; set; }
        public int BitRate { get; set; }
    }

    [Serializable]
    public class SerialPortSetupModel
    {

    }

    [Serializable]
    public class VectorSetupModel
    {

    }
}
