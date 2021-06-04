using MedicineTracking.Repo.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineTracking.Repo.Domain
{
    public class MedicineMaster
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [CurrentDate(ErrorMessage = "Expiry date must be greater than 15 days")]
        public DateTime ExpiryDate { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }
    }
}
