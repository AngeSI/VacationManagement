using Moq;

using System;

using VacationDomain.Interfaces;
using VacationDomain.Models;
using VacationDomain.Services;

using VacationDomainTests.TestModels;
namespace VacationDomainTests.VacaTionSubmit
{
    public class VacationSubmitUC1
    {

        [Fact]
        public async Task Create_VacationSubmit_WithNoLoginAsync()
        {
            TestVacationSubmit vacation = new TestVacationSubmit();
            string expectedMessage = "Employee login cannot be empty";
            var service = new Mock<VacationService>();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithNoStartTime()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange" };
            string expectedMessage = "Start time cannot be empty";
            var service = new Mock<VacationService>();

            Exception exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithNoEndTime()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange", StartTime = DateTime.Now };
            string expectedMessage = "End time cannot be empty";
            var service = new Mock<VacationService>();

            Exception exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithNotExistingEmployee()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange", StartTime = DateTime.Now, EndTime = DateTime.Now };
            string expectedMessage = "Employee does not exist";
            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetEmployee(vacation.EmployeeLogin)).Returns((TestEmployee)null);

            Exception exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithBadDates()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(-1) };
            string expectedMessage = "Start time must be before or equal to end time";
            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetEmployee(vacation.EmployeeLogin)).Returns(new TestEmployee
            {
                Login = vacation.EmployeeLogin,
            });

            Exception exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithLongComment()
        {
            string longComment = new string('a', 120);
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange", StartTime = DateTime.Now, EndTime = DateTime.Now, EmployeeComment = longComment };
            string expectedMessage = "Comment must be smaller than 100";
            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetEmployee(vacation.EmployeeLogin)).Returns(new TestEmployee { Login = vacation.EmployeeLogin});

            Exception exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Create_VacationSubmit_WithExistingVacationOnSamePeriod()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { EmployeeLogin = "ange", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };
            string expectedMessage = "There is already a vacation in the same period";
            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetEmployee(vacation.EmployeeLogin)).Returns(new TestEmployee { Login = vacation.EmployeeLogin });
            dbservice.Setup(x => x.GetVacationApprovedOrNotYet(vacation.EmployeeLogin)).Returns(new List<IVacationSubmit> { new TestVacationSubmit { EmployeeLogin = vacation.EmployeeLogin, StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now.AddDays(2) } });

            Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.Object.CreateVacation(vacation));

            Assert.Contains(expectedMessage, exception.Message);

        }


    }
}