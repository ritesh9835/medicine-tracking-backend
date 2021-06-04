using MedicineTracking.Repo.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineTracking.Repo
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly string jsonFile = "AppData/medicine.json";
        private readonly ILogger<MedicineRepo> _logger;

        public MedicineRepo(ILogger<MedicineRepo> logger)
        {
            _logger = logger;
        }

        public async Task<Tuple<bool,string>> ManageMedicine(MedicineMaster medicine)
        {
            if(medicine == null)
                throw new ArgumentException("Invalid request");

            if (medicine.ExpiryDate < DateTime.Now.AddDays(15))
                throw new ArgumentException("Invalid Days");

            //read json and deserialize
            var json = await File.ReadAllTextAsync(jsonFile);
            var jObject = JObject.Parse(json);
            var jsonObj = JsonConvert.DeserializeObject<List<MedicineMaster>>(JsonConvert.SerializeObject(jObject["medicines"]));

            //add if id is empty
            if (medicine.Id == Guid.Empty)
            {
                medicine.Id = Guid.NewGuid();
                jsonObj.Add(medicine);
            }
            else
            {
                //edit
                var medicineData = jsonObj.FirstOrDefault(c => c.Id == medicine.Id);
                if (medicineData != null)
                {
                    medicineData.FullName = medicine.FullName;
                    medicineData.Brand = medicine.Brand;
                    medicineData.ExpiryDate = medicine.ExpiryDate;
                    medicineData.Notes = medicine.Notes;
                    medicineData.Price = medicine.Price;
                    medicineData.Quantity = medicine.Quantity;
                }
                else
                    return Tuple.Create(false, "Data not found");
            }
            //append to note
            jObject["medicines"] = JArray.FromObject(jsonObj);
            string newJsonResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            await File.WriteAllTextAsync(jsonFile, newJsonResult);
            return Tuple.Create(true, "Record saved");
        }

        public async Task<List<MedicineMaster>> MedicineList()
        {
            var json = await File.ReadAllTextAsync(jsonFile);
            var jObject = JObject.Parse(json);
            var jsonObj = JsonConvert.DeserializeObject<List<MedicineMaster>>(JsonConvert.SerializeObject(jObject["medicines"]));
            return jsonObj;
        }

        public async Task<MedicineMaster> MedicineById(Guid id)
        {
            var json = await File.ReadAllTextAsync(jsonFile);
            var jObject = JObject.Parse(json);
            var jsonObj = JsonConvert.DeserializeObject<List<MedicineMaster>>(JsonConvert.SerializeObject(jObject["medicines"]));
            return jsonObj.FirstOrDefault(c=>c.Id == id);
        }
    }
}
