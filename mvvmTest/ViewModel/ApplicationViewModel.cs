﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using mvvmTest.Model;

namespace mvvmTest.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Phone selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Phone phone = new Phone();
                        Phones.Insert(0, phone);
                        SelectedPhone = phone;
                    }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Phone phone = obj as Phone;
                        if (phone != null)
                        {
                            Phones.Remove(phone);
                        }
                    },
                    (obj) => Phones.Count > 0));
            }
        }

        public Phone SelectedPhone
        {
            get => selectedPhone;
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<Phone>
            {
                new Phone  { Title = "iPhone 7", Company = "Apple", Price = 56000 },
                new Phone  { Title = "Galaxy S7 Edge", Company = "Samsung", Price = 60000 },
                new Phone  { Title = "Elite x3", Company = "HP", Price = 56000 },
                new Phone  { Title = "Mi5S", Company = "Xiaomi", Price = 35000 }
            };
        }
    }
}
