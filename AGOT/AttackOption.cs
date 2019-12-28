using System;


namespace AGOT
{
    public class AttackOption : IComparable<AttackOption> 
    {
        private string _Name;
        private string _Abstract;
        private string _Description;

        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public string Abstract
        {
            get { return this._Abstract; }
            set { this._Abstract = value; }
        }

        public string Description
        {
            get { return this._Description; }
            set { this._Description = value.Replace("\n", " ").Replace("\t",""); }
        }

        public int CompareTo(AttackOption other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
