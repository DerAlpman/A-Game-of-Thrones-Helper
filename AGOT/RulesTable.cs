using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AGOT
{
    public class RulesTable : IComparable<RulesTable>
    {
        private string _Name;
        private string[] _Headers;
        private List<string> _Rows;
        private List<string> _AdditionalText;
        private int _Number;
        private int _Chapter;

        public string Name
        {
            get { return this._Name; }
            set 
            { 
                this._Name = value;
                Regex wildcard = new Regex("[0-9]");
                string[] parts;
                int n;

                if (wildcard.IsMatch(value))
                {
                    parts = value.Split(' ', ':', '-');
                    this._Chapter = Convert.ToInt32(parts[1]);
                    this._Number = Convert.ToInt32(parts[2]);
                }
                else
                {
                    this._Chapter = 0;
                }
            }
        }

        public int Chapter
        {
            get { return this._Chapter; }
        }

        public int Number
        {
            get { return this._Number; }
        }

        public string[] Headers
        {
            get { return this._Headers; }
            set { this._Headers = value; }
        }

        public List<string> Rows
        {
            get { return this._Rows; }
            set { this._Rows = value; }
        }

        public List<string> AdditionalText
        {
            get { return this._AdditionalText; }
            set { this._AdditionalText = value; }
        }

        public RulesTable()
        {
            Rows = new List<string>();
            AdditionalText = new List<string>();
        }

        public int CompareTo(RulesTable other)
        {
            string[] thisParts = Name.Split(':');
            string[] otherParts = Name.Split(':');
            int compVal;

            if (Chapter != 0 && other.Chapter != 0)
            {
                compVal = Chapter.CompareTo(other.Chapter);
                switch (compVal)
                {
                    case 0:
                        return Number.CompareTo(other.Number);
                    default:
                        return compVal;
                }
            }
            else
            {
                if (Chapter == 0 && other.Chapter == 0)
                    return thisParts[1].CompareTo(otherParts[1]);
                else if(Chapter == 0)
                    return -1;
                else
                    return +1;
            }
        }

    }
}
