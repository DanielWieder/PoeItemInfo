using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO;
using System.Threading;

namespace PoeItemInfo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string name = null;
            if (args.Length > 0)
            {
                name = args[0];
            }

            ItemParser parser = new ItemParser();
            SubmitItem submit = new SubmitItem();
            var machineName = Environment.MachineName;
            Queue<string> clipboardQueue = new Queue<string>();
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    var clipText = Clipboard.GetText();

                    if (!string.IsNullOrWhiteSpace(clipText) && !clipboardQueue.Contains(clipText))
                    {

                        clipboardQueue.Enqueue(clipText);

                        if (clipboardQueue.Count > 100)
                        {
                            clipboardQueue.Dequeue();
                        }
                        try
                        {
                            var item = parser.Parse(name, machineName, clipText);
                            submit.Execute(item);
                        }
                        catch (Exception e)
                        {
                            // Not an item
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
