using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Cats.Models;
using Xamarin.Forms;

namespace Cats.ViewModels
{
    public class CatsViewModel : INotifyPropertyChanged
    {
        //metodo construtor 
        public CatsViewModel()
        {
            Cats = new ObservableCollection<Models.Cat>();

            //Inicializa o GetCatsCommand
            GetCatsCommand = new Command(async () => await GetCats(), () => !IsBusy);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
          [System.Runtime.CompilerServices.CallerMemberName]
          string propertyName = null) =>
          PropertyChanged?.Invoke(this,
          new PropertyChangedEventArgs(propertyName));

        private bool Busy;

        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cat> Cats { get; set; }

        

        async Task GetCats()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetCats();

                    //limpar a lista atual de objetos Cat e carregá - los a partir da coleção Items.
                    Cats.Clear();
                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }

                //Mensagem de erro.
                if (Error != null)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error!", Error.Message, "OK");
                }

                            }
            return; 
        }

        public Command GetCatsCommand { get; set; }


    }
}
