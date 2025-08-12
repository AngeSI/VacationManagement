using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VacationDomain.Interfaces;
using VacationDomain.Models;

using VacationDomain.Services;

using VacationDomainTests.TestModels;

namespace VacationDomainTests.VacaTionSubmit
{
    public class VacationSubmitUC2
    {
        [Fact]
        public async Task ValidateVacationSubmit_ThatDoesNotExists()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { VacationId = 1 };
            string expectedMessage = "Vacation does not exist";

            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);
            dbservice.Setup(x => x.GetVacationSubmit(vacation.VacationId)).Returns(null as TestVacationSubmit);            

            var exception = await Assert.ThrowsAsync<Exception>(async () => await service.Object.ValidateVacation(vacation.VacationId, vacation.EmployeeLogin, true, string.Empty));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task ValidateVacationSubmit_WithoutHRLogin()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { VacationId = 1 };
            string expectedMessage = "HR login cannot be empty";

            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);
            dbservice.Setup(x => x.GetVacationSubmit(vacation.VacationId)).Returns(new TestVacationSubmit());

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Object.ValidateVacation(vacation.VacationId, vacation.EmployeeLogin, true, string.Empty));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task ValidateVacationSubmit_ByItsSubmiter()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { VacationId = 1, EmployeeLogin = "asilue" };
            vacation.ValidationHRLogin = vacation.EmployeeLogin;
            string expectedMessage = "HR login cannot be the same as employee login";

            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetVacationSubmit(vacation.VacationId)).Returns(vacation);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Object.ValidateVacation(vacation.VacationId, vacation.ValidationHRLogin, true, string.Empty));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task ValidateVacationSubmit_WithNoExistingHR()
        {
            TestVacationSubmit vacation = new TestVacationSubmit { VacationId = 1, EmployeeLogin = "asilue", ValidationHRLogin = "aopeuli" };
            string expectedMessage = "HR does not exist";

            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            dbservice.Setup(x => x.GetVacationSubmit(vacation.VacationId)).Returns(vacation);
            dbservice.Setup(x => x.GetEmployee(vacation.ValidationHRLogin)).Returns((TestEmployee)null);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Object.ValidateVacation(vacation.VacationId, vacation.ValidationHRLogin, true, string.Empty));

            Assert.Contains(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task ValidateVacationSubmit_SubmittedByFakeHR()
        {
            var dbservice = new Mock<IVacationDbService>();
            var service = new Mock<VacationService>(dbservice.Object);

            TestVacationSubmit vacation = new TestVacationSubmit { VacationId = 1, EmployeeLogin = "asilue", ValidationHRLogin = "aopeuli" };
            TestEmployee fakeHR = new TestEmployee();
            string expectedMessage = "HR is not a HR";

            dbservice.Setup(x => x.GetVacationSubmit(vacation.VacationId)).Returns(vacation);
            dbservice.Setup(x => x.GetEmployee(vacation.ValidationHRLogin)).Returns(fakeHR);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Object.ValidateVacation(vacation.VacationId, vacation.ValidationHRLogin, true, string.Empty));

            Assert.Contains(expectedMessage, exception.Message);

        }

    }
}
