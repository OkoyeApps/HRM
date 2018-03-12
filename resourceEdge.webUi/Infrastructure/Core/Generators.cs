using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace resourceEdge.webUi.Infrastructure.Core
{
    public class Generators
    {
        public string CodeGeneration(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CodeGeneration(4, true));
            builder.Append(CodeGeneration(2, false));
            return builder.ToString();
        }
    }
}