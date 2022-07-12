using System.Data;

namespace SIMS.Service
{
    public class Result
    {
        public DataTable Data;

        public bool ExecutionState { get; set; }

        public string Error { get; set; }
    }
}
