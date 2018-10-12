using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskMVVM.Commands;
using TaskMVVM.Models;


namespace TaskMVVM.ViewModels
{
    class AddStudentViewModel:BaseViewModel
    {
        private Action<StudentModel> _refreshStudents;
        private StudentModel _currentStudent;

        public StudentModel CurrentStudent
        {
            get { return _currentStudent; }

            set { _currentStudent = value;
                OnPropertyChanged(nameof(CurrentStudent));
            }
        }

        public AddStudentViewModel(Action<StudentModel> action)
        {
            _refreshStudents += action;
            CurrentStudent = new StudentModel { BirthDate = DateTime.Now.Date };
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new Command(arg => OnCancelStudent()));
            }
        }

        private void OnCancelStudent()
        {
            Application.Current.Windows.Cast<Window>().FirstOrDefault(w=>w.Name=="AddView")?.Close();
        }
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(arg => OnSaveStudent()));
            }
        }

        private void OnSaveStudent()
        {
            try {
                 _refreshStudents(CurrentStudent);
                OnCancelStudent();
                 }
            catch(Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }
    }
}
