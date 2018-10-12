using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskMVVM.Commands;
using TaskMVVM.Models;
using TaskMVVM.Views;

namespace TaskMVVM.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        public ObservableCollection<StudentModel> StudentCollection { get; set; }
        private ICommand _minimizedCommand;
        public ICommand MinimizedCommand
        {
            get
            {
                return _minimizedCommand ?? (_minimizedCommand = new Command(arg => OnMinimizedWindow()));
            }
        }
         private void OnMinimizedWindow()
        {
            //((MainWindow)Application.Current.MainWindow).WindowState = WindowState.Minimized;
           
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }

        public MainViewModel(List<StudentModel> students)
        {
            StudentCollection = new ObservableCollection<StudentModel>(students);
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new Command(arg => OnAddStudent()));
            }
        }

        private void OnAddStudent()
        {
            //StudentCollection.Add(new StudentModel {LastName="fgh",FirstName="klwe" });
            
        
            AddStudentView studentView = new AddStudentView()
            {
                DataContext = new AddStudentViewModel(AddStudentInCollection)
        };
            studentView.ShowDialog();

        }

        private void AddStudentInCollection(StudentModel student)
        {
            StudentCollection.Add(student);
        }
    }
}
