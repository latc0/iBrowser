using System;
using System.Collections;
using System.Windows.Forms;

namespace iphone
{
    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = 0;
            switch (col)
            {
                case 0: //name
                    {
                        string xText = ((ListViewItem)x).SubItems[col].Text;
                        string yText = ((ListViewItem)y).SubItems[col].Text;
                        returnVal = String.Compare(xText, yText);
                        break;
                    }
                case 1: //size
                    {
                        string xText = ((ListViewItem)x).SubItems[col].Text;
                        string yText = ((ListViewItem)y).SubItems[col].Text;
                        if (xText == string.Empty || yText == string.Empty)//folders
                        {
                            return 0;
                        }

                        //remove kb or mb at end of file size
                        xText = xText.Remove(xText.Length - 3).Replace(".", "");
                        yText = yText.Remove(yText.Length - 3).Replace(".", "");

                        double xSize = (double.Parse(xText) / 100) * 1024 * 1024;
                        double ySize = (double.Parse(yText) / 100) * 1024 * 1024;
                        if (order == SortOrder.Ascending)
                            return (int)(xSize - ySize);
                        else return (int)(ySize - xSize);
                    }
                case 2: //filetype
                    {
                        string xText = ((ListViewItem)x).SubItems[col].Text;
                        string yText = ((ListViewItem)y).SubItems[col].Text;
                        returnVal = String.Compare(xText, yText);
                        break;
                    }
                case 3: //date
                    {
                        System.DateTime firstDate = DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                        System.DateTime secondDate = DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                        returnVal = DateTime.Compare(firstDate, secondDate);
                        break;
                    }
            }
            if (order == SortOrder.Descending)
                returnVal *= -1;
            return returnVal;
        }
    }
}
