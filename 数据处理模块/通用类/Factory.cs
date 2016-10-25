using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public class Factory
    {
        public IProduct Create()
        {
            return new ConcreateProductA();
        }
    }
}
