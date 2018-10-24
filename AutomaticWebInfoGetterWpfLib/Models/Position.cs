using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomaticWebInfoGetterWpfLib.Models
{
    class Position : ICloneable
    {
        private int row;
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        private int column;
        public int Column
        {
            get { return column; }
            set { column = value; }
        }
        public object Clone()
        {
            return new Position
            {
                Row = Row,
                Column = Column
            };
        }
    }
}