using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToolsClientWPF.Models
{
        public class ToolDisplay
        {
        private string id;
        private string toolName;
        private string workerName;

        public ToolDisplay(string _id, string _toolName, string _workerName)
        {
            id = _id;
            toolName = _toolName;
            workerName = _workerName;
        }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string ToolName
        {
            get { return toolName; }
            set
            {
                toolName = value;
                OnPropertyChanged("ToolName");
            }
        }
        public string Worker
        {
            get { return workerName; }
            set
            {
                workerName = value;
                OnPropertyChanged("Worker");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
