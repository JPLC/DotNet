using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1___POO
{
    public abstract class Student : IPerson
    {
        private string name;
        private int studentNumber;
    
        public void setName(string Name)
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            throw new NotImplementedException();
        }

        public void writeOutput()
        {
            throw new NotImplementedException();
        }

        public bool sameName(IPerson otherPerson)
        {
            throw new NotImplementedException();
        }

        public void setNumber(int number)
        {
            throw new System.NotImplementedException();
        }

        public int getNumber()
        {
            throw new System.NotImplementedException();
        }

        public bool equals(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
