using MedicineTracking.Repo.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineTracking.Repo
{
    public interface IMedicineRepo
    {
        Task<Tuple<bool, string>> ManageMedicine(MedicineMaster medicine);
        Task<List<MedicineMaster>> MedicineList();
        Task<MedicineMaster> MedicineById(Guid id);
    }
}
