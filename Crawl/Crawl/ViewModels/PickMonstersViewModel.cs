using Crawl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crawl.ViewModels
{
    public class PickMonstersViewModel : BaseViewModel
    {
        #region Singleton
        // Create one instance of view model
        private static PickMonstersViewModel _instance;

        public static PickMonstersViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PickMonstersViewModel();
                return _instance;
            }
        }
        #endregion Singleton

        // list of data
        public ObservableCollection<Monster> DataSet { get; set; }

        /// <summary>
        /// private constructor 
        /// </summary>
        private PickMonstersViewModel()
        {
            DataSet = new ObservableCollection<Monster>();
        }

    }
}
