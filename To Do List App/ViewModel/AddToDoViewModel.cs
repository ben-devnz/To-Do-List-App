using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using To_Do_List_App.Commands;
using To_Do_List_App.Models;


// TODO

namespace To_Do_List_App.ViewModel
{
    public class AddToDoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // ==================== PRIVATE FIELDS ====================
        private string _name;
        private string _details;
        private Window _window;

        // ==================== PUBLIC PROPERTIES ====================
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }
        // ==================== COMMANDS ====================

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        // ==================== CONSTRUCTOR ====================
        public AddToDoViewModel(Window window)
        {
            _window = window;
            _name = string.Empty;
            _details = string.Empty;

            // Initialize Commands
            SaveCommand = new RelayCommand(
                executeMethod: (param) => SaveNewTask(),
                CanExecuteMethod: (param) => true
            );
            CancelCommand = new RelayCommand(
                executeMethod: (param) => CancelNewTask(),
                CanExecuteMethod: (param) => true
            );

        }

        // ==================== PRIVATE METHODS ====================
        private void SaveNewTask()
        {
            if (string.IsNullOrWhiteSpace(Name)) return;

            _window.DialogResult = true; // indicates that save was clicked
            _window.Close();
        }
        private void CancelNewTask()
        {
            _window.DialogResult = false;
            _window.Close();
        }

    }
}
