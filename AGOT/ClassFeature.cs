using System;

namespace AGOT
{
    public class ClassFeature : IComparable<ClassFeature> 
    {
        private string _Class;
        private string _Name;
        private string _Description;

        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public string Class
        {
            get { return this._Class; }
            set { this._Class = value; }
        }

        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public int CompareTo(ClassFeature other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
