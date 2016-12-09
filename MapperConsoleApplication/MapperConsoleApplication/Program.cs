using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassMapper;

namespace MapperConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper mapper = new Mapper();

            SmallElephant elephant1 = new SmallElephant()
            {
                Weight = 10
            };

            BigElephant elephant2 = mapper.Map<SmallElephant, BigElephant>(elephant1);

            Console.WriteLine(elephant2.Weight.ToString());
            Console.Read();
        }
    }
}
