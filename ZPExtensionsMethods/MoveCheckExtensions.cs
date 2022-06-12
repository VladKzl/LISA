using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using static ZPBase.ProgramBase;

namespace ZPExtensionsMethods
{
    public static class MoveCheckExtensions
    {
        public static void CheckChangePassword()
        {
            instance.SendText("{ESC}", 15); // Обход иконки смены пароля
            instance.ActiveTab.WaitDownloading();
        }
        public static HtmlElement IsElementDownload(string xPath, string message)
        {
            instance.ActiveTab.WaitDownloading();
            HtmlElement localElement = null;
            bool result;
            localElement = instance.ActiveTab.TryFindElementByXPath(xPath, out result);
            if (result == false)
            {
                Status.SetValue("Done");
                lock (mainWbLocker)
                {
                    mainWb.Save();
                }
                throw new Fatal(message);
            }
            return localElement;
        }
        public enum WaitDownload
        {
            Any,
            All
        }
        public static Dictionary<string, HtmlElement> AreElementsDownload(string xPath1, WaitDownload choose, string message, string xPath2 = null, string xPath3 = null)
        {
            instance.ActiveTab.WaitDownloading();
            Dictionary<string, HtmlElement> elements = new Dictionary<string, HtmlElement>();
            int i;
            for (i = 0; i != 10; i++)
            {
                if (choose == WaitDownload.Any)
                {
                    if (xPath1 != null)
                    {
                        HtmlElement element = instance.ActiveTab.FindElementByXPath(xPath1, 0);
                        if (!element.IsVoid)
                        {
                            elements.Add("xPath1", element);
                            break;
                        }
                    }
                    if (xPath2 != null)
                    {
                        HtmlElement element = instance.ActiveTab.FindElementByXPath(xPath2, 0);
                        if (!element.IsVoid)
                        {
                            elements.Add("xPath2", element);
                            break;
                        }
                    }
                    if (xPath3 != null)
                    {
                        HtmlElement element = instance.ActiveTab.FindElementByXPath(xPath3, 0);
                        if (!element.IsVoid)
                        {
                            elements.Add("xPath3", element);
                            break;
                        }
                    }
                }
                else if (choose == WaitDownload.All)
                {
                    HtmlElement element1 = null;
                    HtmlElement element2 = null;
                    HtmlElement element3 = null;
                    if (xPath1 != null)
                    {
                        element1 = instance.ActiveTab.FindElementByXPath(xPath1, 0);
                    }
                    if (xPath2 != null)
                    {
                        element2 = instance.ActiveTab.FindElementByXPath(xPath2, 0);
                    }
                    if (xPath3 != null)
                    {
                        element3 = instance.ActiveTab.FindElementByXPath(xPath3, 0);
                    }
                    if (!element1.IsVoid & !element2.IsVoid & !element3.IsVoid)
                    {
                        elements.Add("xPath1", element1);
                        elements.Add("xPath2", element2);
                        elements.Add("xPath3", element3);
                        break;
                    }
                }
                if (i == 10)
                {
                    Status.SetValue("Done");
                    lock (mainWbLocker)
                    {
                        mainWb.Save();
                    }
                    throw new Fatal(message);
                }
            }
            return elements;
        }
    }
}
