using Microsoft.VisualStudio.TestTools.UnitTesting;
using VacationDomain.Interfaces;

using System;

namespace DomainTests
{
    [TestClass]
    public class TestVactionSubmitTests
    {
        [TestMethod]
        public void Create_Submit_WithNoLogin()
        {
            VacationSubmit vacation = new VacationSubmit();
        }
    }
}
