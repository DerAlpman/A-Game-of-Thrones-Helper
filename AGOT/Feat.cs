using System;

namespace AGOT
{
    /// <summary>
    /// Class for any type of feat.
    /// </summary>
    public class Feat : IComparable<Feat> 
    {
        private string _Type;
        private string _Name;
        private string _Prerequisites;
        private string _Abstract;
        private string _Benefit;
        private string _Special;
        private string _Description;

        public string Type
        {
            get { return this._Type; }
            set { this._Type = value; }
        }

        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public string Prerequisites
        {
            get { return this._Prerequisites; }
            set { this._Prerequisites = value; }
        }

        public string Abstract
        {
            get { return this._Abstract; }
            set { this._Abstract = value; }
        }

        public string Benefit
        {
            get { return this._Benefit; }
            set { this._Benefit = value; }
        }

        public string Special
        {
            get { return this._Special; }
            set { this._Special = value; }
        }

        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public static Comparison<Feat> TypeComparison =
            delegate(Feat f1, Feat f2)
            {
                return f1.Type.CompareTo(f2.Type);
            };

        public int CompareTo(Feat other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
