using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestWPFUI.SQLiteCipher.Models;
using TestWPFUI.SQLiteCipher.ViewModels;

namespace TestWPFUI.SQLiteCipher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 退出时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
#if DEBUG
            try
            {
                //EConfiguration.Menus.Clear();
                //foreach (var item in MainViewModel.Instance.Menus.Skip(1))
                //{
                //    EConfiguration.Menus.Add(new DbMenuModel
                //    {
                //        Title = item.Title,
                //        SubTitle = item.Subtitle,
                //        Config = item.Config,
                //    });
                //}
                //EConfiguration.TrySave();
            }
            catch { }
#endif
            base.OnExit(e);
        }
    }
}
