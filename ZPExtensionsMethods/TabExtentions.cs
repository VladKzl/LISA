using System;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.Emulation;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections;
using System.Threading;
using LogLevels;
using static ZPBase.Base;

namespace ZPExtensionsMethods
{
    public static class TabExtentions
    {
        private static void CheckTab(this Tab tab)
        {
            tab.WaitDownloading();
            if (tab.IsVoid || tab.IsNull)
            {
                string exceptionMessage = "Таб не активировался";
                throw new Fatal(exceptionMessage);
            }
        }
        public static void TryTabsNavigate(this Tab[] tabs, int tabNum, string url, string refferer = "")
        {
            Tab tab = tabs[tabNum];
            tab.Navigate(url, refferer);
            tab.CheckTab();
        }
        public static void TryTabNavigate(this Tab tab, string url, string refferer = "")
        {
            tab.CheckTab();
            tab.Navigate(url, refferer);
        }
        public static HtmlElement TryFindElementByXPath(this Tab tab, string xpath, out bool result, int numberOfMatch = 0)
        {
            tab.CheckTab();
            HtmlElement element = null;
            result = false;
            int i;
            for (i = 0; i != 10; i++)
            {
                element = tab.FindElementByXPath(xpath, numberOfMatch);
                if (element.IsVoid)
                {
                    Thread.Sleep(4 * 1000);
                }
                else if (!element.IsVoid)
                {
                    break;
                }
            }
            if (i == 10)
            {
                result = false;
            }
            else if(i != 10)
            {
                result = true;
            }
            return element;
        }
        public static void KeyEventEx(this Tab tab, string key, string keyEvent, string keyModifer)
        {
            tab.KeyEvent(key, keyEvent, keyModifer);
            tab.CheckTab();
        }
    }
}
