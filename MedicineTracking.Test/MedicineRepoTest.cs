using MedicineTracking.Repo;
using MedicineTracking.Repo.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MedicineTracking.Test
{
    public class MedicineRepoTest
    {
        [Fact]
        public async Task GivenValidGuidGetRecord()
        {
            var loggerMock = new Mock<ILogger<MedicineRepo>>();
            //Create the Service instance
            var service = new MedicineRepo(loggerMock.Object);

            loggerMock.Object.LogInformation("Unit test started");

            //Act - Call the method being tested
            var medicine = await service.MedicineById(Guid.Parse("e818585e-98d4-445b-9d4d-3b363978d095"));

            //Assert
            Assert.Equal(Guid.Parse("e818585e-98d4-445b-9d4d-3b363978d095"), medicine.Id);
        }

        [Fact]
        public void ExpectExceptionIfEarlyExpire15Days()
        {
            var request = new MedicineMaster
            {
                Id = Guid.Empty,
                Brand = "New Brand",
                ExpiryDate = DateTime.Now.AddDays(10),
                FullName = "New name",
                Notes = "test these notes",
                Price = 100,
                Quantity = 10
            };
            var loggerMock = new Mock<ILogger<MedicineRepo>>();
            //Create the Service instance
            var service = new MedicineRepo(loggerMock.Object);

            loggerMock.Object.LogInformation("Unit test started");

            Assert.Throws<AggregateException>(() => service.ManageMedicine(request).Result);


        }

        [Fact]
        public async Task GetAllShouldReturnValidRecord()
        {
            var loggerMock = new Mock<ILogger<MedicineRepo>>();
            //Create the Service instance
            var service = new MedicineRepo(loggerMock.Object);

            loggerMock.Object.LogInformation("Unit test started");

            //Act - Call the method being tested
            var medicine = await service.MedicineList();

            //Assert
            Assert.NotEmpty(medicine);
        }


        [Fact]
        public async Task GivenValidDataShouldUpdate()
        {
            var request = new MedicineMaster
            {
                Id = Guid.Parse("e818585e-98d4-445b-9d4d-3b363978d095"),
                Brand = "New Brand",
                ExpiryDate = DateTime.Now.AddDays(16),
                FullName = "New name",
                Notes = "test these notes",
                Price = 100,
                Quantity = 10
            };
            var loggerMock = new Mock<ILogger<MedicineRepo>>();
            //Create the Service instance
            var service = new MedicineRepo(loggerMock.Object);

            loggerMock.Object.LogInformation("Unit test started");

            var medicine = await service.ManageMedicine(request);

            Assert.True(medicine.Item1);


        }


        [Fact]
        public void GivenNullDataShouldFail()
        {
            var loggerMock = new Mock<ILogger<MedicineRepo>>();
            //Create the Service instance
            var service = new MedicineRepo(loggerMock.Object);

            loggerMock.Object.LogInformation("Unit test started");

            Assert.Throws<AggregateException>(() => service.ManageMedicine(null).Result);
        }
    }
}
