using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1___POO
{
    public interface IPerson
    {
        void setName(string Name);

        string getName();

        void writeOutput();

        bool sameName(IPerson otherPerson);
    }
}
