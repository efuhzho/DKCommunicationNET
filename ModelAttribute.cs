using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET
{
    [AttributeUsage ( AttributeTargets. All , Inherited = false , AllowMultiple = true )]
    internal sealed class ModelAttribute : Attribute
    {
        readonly Models model;

        // This is a positional argument
        public ModelAttribute ( Models model )
        {
            this. model = model;
        }

        public Models Model
        {
            get { return model; }
        }
    }
}
