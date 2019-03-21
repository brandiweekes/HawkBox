using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Crawl.Models;
using Xamarin.Forms;

namespace Crawl.ViewModels
{
    /// <summary>
    /// this class represent selected character in multi-select view
    /// </summary>
    public class MultiSelectData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Character Data { get; set; }

        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                OnPropertyChanged("Image");
            }
        }

        public MultiSelectData(Character character, bool isSelected)
        {
            Data = character;
            IsSelected = isSelected;
            Image = IsSelected ? "checkbox_checked_icon.png" : "checkbox_unchecked_icon.png";
        }

        private void OnPropertyChanged(string props)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(props));
        }

    }

    /// <summary>
    /// view model for PickCharactersPage
    /// </summary>
    public class PickCharactersViewModel : BaseViewModel
    {
        #region Singleton
        // Create one instance of view model
        private static PickCharactersViewModel _instance;

        public static PickCharactersViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PickCharactersViewModel();
                return _instance;
            }
        }
        #endregion Singleton

        public ObservableCollection<MultiSelectData> DataSet { get; set; }

        public Command LoadCommand { get; set; }

        private bool _needsRefresh;

        private PickCharactersViewModel()
        {
            DataSet = new ObservableCollection<MultiSelectData>();
            LoadCommand = new Command(async () => await ExecuteLoadCommand());
        }

        #region Refresh

        /// <summary>
        /// Return True if a refresh is needed
        /// It sets the refresh flag to false
        /// </summary>
        /// <returns></returns>
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;

        }

        /// <summary>
        /// Sets the need to refresh
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        /// <summary>
        /// Command function which is executed to refresh dataset from datastore.
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var characters = await DataStore.GetAllAsync_Character(true);
                foreach (var character in characters)
                {
                    DataSet.Add(new MultiSelectData(character, false));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                SetNeedsRefresh(false);
            }
        }

        /// <summary>
        /// called to force refresh dataset from datastore.
        /// </summary>
        public void ForceDataRefresh()
        {
            LoadCommand.Execute(null);
        }

        #endregion Refresh

        #region Data Operations

        /// <summary>
        /// Fetch selected characters from dataset.
        /// </summary>
        /// <returns></returns>
        public List<Character> GetSelectedCharacters()
        {
            return DataSet.Where(args => args.IsSelected == true).Select(args => args.Data).ToList();
        }

        #endregion Data Operations
    }
}
