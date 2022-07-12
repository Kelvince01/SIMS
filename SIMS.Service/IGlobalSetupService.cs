using SIMS.Models;

namespace SIMS.Service
{
    public interface IGlobalSetupService
    {
        void Update(GlobalSetup model);

        void Save();

        GlobalSetup GetTopSetup();

        void UpdateFiscalYear(string[] model);
    }
}
