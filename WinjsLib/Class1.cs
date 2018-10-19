using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinjsLib
{
    public class Class1:Base
    {
    }
    public class Base
    {
    }
    public class mmmm{
        void getClass(Base c) { }
        void main() {
            Class1 c1 = new Class1();
            Base b = new Base();
            getClass(c1);
        }

    }
}
