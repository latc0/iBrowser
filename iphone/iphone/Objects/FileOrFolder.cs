using System;
using System.Drawing;

namespace iphone
{
    public class FileOrFolder
    {
        private Bitmap image;
        private string name;
        private string size;
        private string type;
        private DateTime date;

        public FileOrFolder(Bitmap image, string name, string size, string type, DateTime date)
        {
            this.image = image;
            this.name = name;
            this.size = size;
            this.type = type;
            this.date = date;
        }

        public Bitmap Image
        {
            get { return image; }
            set { image = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Size
        {
            get { return size; }
            set { size = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
