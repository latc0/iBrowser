namespace iphone
{
   public class Log
    {
       public static void Write(string s)
       {
           iBrowser.instance.mainTextBox.AppendText(s);             
       }
    }
}
