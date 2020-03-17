using NUnit.Framework;
using MovieRentalProject.Controllers;
using System.Web.Mvc;
using System.Data.Entity;
using MovieRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MovieRentalProject.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Security.Principal;
using System.Security.Claims;
using System.Web;

namespace Tests
{
    [TestFixture]
    public class CustomersTests
    {

        public IQueryable<Customer> TestCustomerList()
        {
            var data = new List<Customer>
            {
                new Customer {Id=1, Name="", isSubscribedToNewsletter=true, MembershipTypeId=2, Birthdate=DateTime.Parse("10/30/2001")},
                new Customer {Id=2, Name="", isSubscribedToNewsletter=true, MembershipTypeId=2, Birthdate=DateTime.Parse("10/30/2001")},
                new Customer {Id=3, Name="", isSubscribedToNewsletter=true, MembershipTypeId=2, Birthdate=DateTime.Parse("10/30/2001")},
                new Customer {Id=4, Name="Test", isSubscribedToNewsletter=true, MembershipTypeId=2, Birthdate=DateTime.Parse("10/30/2001")},
            }.AsQueryable();
            return data;
        }
        public IQueryable<MembershipType> TestMembershipTypeList()
        {
            var data = new List<MembershipType>
            {
                new MembershipType{Id=1, SignupFee=0, DurationInMonths=0,DiscountRate=0, Name="None"},
                new MembershipType{Id=2, SignupFee=30, DurationInMonths=1,DiscountRate=10, Name="Monthly"},
                new MembershipType{Id=3, SignupFee=90, DurationInMonths=3,DiscountRate=15, Name="Quarterly"},
                new MembershipType{Id=4, SignupFee=300, DurationInMonths=12,DiscountRate=20, Name="Yearly"}
            }.AsQueryable();
            return data;
        }

        //index
        [Test]
        public void CustomerViewReturnsReadOnlyFormWhenNotLoggedIn()
        {
            var context = new Mock<IApplicationDbContext>();
            var controller = new CustomersController(context.Object);
            var user = new Mock<IPrincipal>();
            user.Setup(m => m.IsInRole("CanManageMovies")).Returns(false);
            var mock = new Mock<HttpContextBase>();
            mock.Setup(m => m.User).Returns(user.Object);
            controller.ControllerContext = new ControllerContext() { HttpContext = mock.Object };
            var result = (ViewResult)controller.Index();
            Assert.AreEqual(result.ViewName, "CustomerReadOnlyList");
        }
        [Test]
        public void CustomerViewReturnsFormWhenAdmin()
        {
            var context = new Mock<IApplicationDbContext>();
            var controller = new CustomersController(context.Object);
            var user = new Mock<IPrincipal>();
            user.Setup(m => m.IsInRole("CanManageMovies")).Returns(true);
            var mock = new Mock<HttpContextBase>();
            mock.Setup(m => m.User).Returns(user.Object);
            controller.ControllerContext = new ControllerContext() { HttpContext = mock.Object };
            var result = (ViewResult)controller.Index();
            Assert.AreEqual(result.ViewName, "CustomerList");
        }
        //details
        [Test]
        public void DetailsReturnsHttpNotFoundWhenCustomerIsNull()
        {
            var context = new Mock<IApplicationDbContext>();
            var editCustomer = new Customer { Id = -1, Name = "Test", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            mockedDbCustomers.Setup(m => m.Include(It.IsAny<string>())).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = controller.Details(editCustomer.Id);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }
        [Test]
        public void DetailsReturnsCustomerWhenCustomerIsValid()
        {
            var context = new Mock<IApplicationDbContext>();
            var editCustomer = new Customer { Id = 4, Name = "Test", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            mockedDbCustomers.Setup(m => m.Include(It.IsAny<string>())).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = (ViewResult)controller.Edit(editCustomer.Id);
            Assert.AreEqual(result.ViewName, "CustomerForm");
            var SingleMethodResult = mockedDbCustomers.Object.Single(c => c.Id == editCustomer.Id);
            Assert.AreEqual(SingleMethodResult.Name, editCustomer.Name);
        }
        //new
        [Test]
        public void NewCustomerReturnsNewCustomerForm()
        {
            var context = new Mock<IApplicationDbContext>();
            var mockedDb = new Mock<DbSet<MembershipType>>();
            var data = TestMembershipTypeList();
            mockedDb.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDb.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDb.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDb.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDb.Object);
            var controller = new CustomersController(context.Object);

            var result = (ViewResult)controller.New();
            Assert.AreEqual(result.ViewName, "CustomerForm");
        }
        //save       
        [Test]
        public void SaveCreatesNewCustomerWhenIdIsZero()
        {
            var context = new Mock<IApplicationDbContext>();
            var newCustomer = new Customer { Id = 0, Name = "New Name", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = controller.Save(newCustomer);
            context.Verify(c => c.Customers.Add(It.IsAny<Customer>()), Times.Once());

            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
            context.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void SaveUpdatesExistingCustomerWhenIdExists()
        {
            var context = new Mock<IApplicationDbContext>();
            var newCustomer = new Customer { Id = 4, Name = "New Name", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = controller.Save(newCustomer);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));

            context.Verify(m => m.SaveChanges(), Times.Once);
            var SingleMethodResult = mockedDbCustomers.Object.Single(c => c.Id == newCustomer.Id);
            Assert.AreEqual(SingleMethodResult.Name, newCustomer.Name);
            context.Verify(m => m.SaveChanges(), Times.Once);

        }
        //edit
        [Test]
        public void EditReturnsHttpNotFoundWhenCustomerIsNull()
        {
            var context = new Mock<IApplicationDbContext>();
            Customer editCustomer = new Customer { Id = -1 };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = controller.Edit(editCustomer.Id);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }
        [Test]
        public void EditReturnsCustomerInfoWhenModelIsValid()
        {
            var context = new Mock<IApplicationDbContext>();
            var editCustomer = new Customer { Id = 4, Name = "Test", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = (ViewResult)controller.Edit(editCustomer.Id);
            Assert.AreEqual(result.ViewName, "CustomerForm");
            var SingleMethodResult = mockedDbCustomers.Object.Single(c => c.Id == editCustomer.Id);
            Assert.AreEqual(SingleMethodResult.Name, editCustomer.Name);
        }
        //delete
        [Test]
        public void DeleteRemovesCustomer()
        {
            var context = new Mock<IApplicationDbContext>();
            var deletedCustomer = new Customer { Id = 4, Name = "Test", isSubscribedToNewsletter = true, MembershipType = new MembershipType(), MembershipTypeId = 2, Birthdate = DateTime.Parse("10/30/2001") };
            var mockedDbMemberships = new Mock<DbSet<MembershipType>>();
            var mockedDbCustomers = new Mock<DbSet<Customer>>();
            var membershipTypeData = TestMembershipTypeList();
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Provider).Returns(membershipTypeData.Provider);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.Expression).Returns(membershipTypeData.Expression);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.ElementType).Returns(membershipTypeData.ElementType);
            mockedDbMemberships.As<IQueryable<MembershipType>>().Setup(m => m.GetEnumerator()).Returns(membershipTypeData.GetEnumerator());
            context.Setup(m => m.MembershipTypes).Returns(mockedDbMemberships.Object);
            var customerData = TestCustomerList();
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerData.Provider);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            mockedDbCustomers.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerData.GetEnumerator());
            context.Setup(m => m.Customers).Returns(mockedDbCustomers.Object);
            var controller = new CustomersController(context.Object);

            var result = controller.Delete(deletedCustomer.Id);
            Assert.AreEqual(result.GetType(), typeof(RedirectToRouteResult));
            var SingleMethodResult = mockedDbCustomers.Object.Single(c => c.Id == deletedCustomer.Id);
            Assert.AreEqual(SingleMethodResult.Name, deletedCustomer.Name);
            context.Verify(c => c.Customers.Remove(It.IsAny<Customer>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once());
        }
    }
}
