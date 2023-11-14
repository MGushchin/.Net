using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsClientWPF.Models
{
    public class ToolItem
    {
        public string Id { get; set; }
        public string ToolName { get; set; }
        public string WorkerName { get; set; }

        public ToolItem(string id, string toolName, string workerName)
        {
            Id = id;
            ToolName = toolName;
            WorkerName = workerName;
        }
    }
}
