using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows;

namespace ToolsClientWPF.Models
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ToolDisplay selectedTool;

        public ObservableCollection<ToolDisplay> Tools { get; set; }
        public ToolDisplay SelectedTool
        {
            get { return selectedTool; }
            set
            {
                selectedTool = value;
                OnPropertyChanged("SelectedTool");
            }
        }

        public ApplicationViewModel()
        {
            Tools = new ObservableCollection<ToolDisplay>();
            Get();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand getCommand;
        public RelayCommand GetCommand
        {
            get
            {
                return getCommand ??
                    (getCommand = new RelayCommand(obj =>
                    {
                        Get();
                    }));
            }
        }

        public async void Get()
        {
            List<ToolItem> items = await HttpClientConnector.GetProductAsync("https://localhost:7297/api/toolitems");
            Tools.Clear();
            foreach (ToolItem item in items)
            {
                Tools.Add(new ToolDisplay(item.Id, item.ToolName, item.WorkerName));
            }
        }

        private RelayCommand postCommand;
        public RelayCommand PostCommand
        {
            get
            {
                return postCommand ??
                    (postCommand = new RelayCommand(obj =>
                    {
                        Post(new ToolItem(selectedTool.Id.ToString(), selectedTool.ToolName, selectedTool.Worker));
                    }));
            }
        }

        public async void Post(ToolItem item)
        {
            await HttpClientConnector.CreateProductAsync(item);
            Get();
        }

        private RelayCommand putCommand;
        public RelayCommand PutCommand
        {
            get
            {
                return putCommand ??
                    (putCommand = new RelayCommand(obj =>
                    {
                        Put(new ToolItem(selectedTool.Id.ToString(), selectedTool.ToolName, selectedTool.Worker));
                    }));
            }
        }
        public async void Put(ToolItem item)
        {
            await HttpClientConnector.UpdateProductAsync(item);
            Get();
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        Delete(selectedTool.Id.ToString());
                    },
                    (obj) => Tools.Count > 0));
            }
        }
        public async void Delete(string itemIndex)
        {
            await HttpClientConnector.DeleteProductAsync(itemIndex);
            Get();
        }
    }
}
