namespace ITSupportService.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ITSupportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ITSupportContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            var MusaId = Guid.NewGuid();
            var Musa = new Employee
            {
                UserId = MusaId,
                Firstname = "Musa Anday",
                Lastname = "Arzu",
                EmailAddress = "musanday@gmail.com",
                isQoS = false,
                Phonenumber = "06307211194",
                StreetAddress = "Teresz korut 2"
            };
            var MiraId = Guid.NewGuid();
            var Mira = new Employee
            {
                UserId = MiraId,
                Firstname = "Mira",
                Lastname = "Abbassi",
                EmailAddress = "miraabbasi@gmail.com",
                isQoS = false,
                Phonenumber = "06307211193",
                StreetAddress = "Teresz korut 5"
            };
            var AmmarId = Guid.NewGuid();
            var Ammar = new Employee
            {
                UserId = AmmarId,
                Firstname = "Ammar",
                Lastname = "Darwesh",
                EmailAddress = "ammardarwesh@gmail.com",
                isQoS = false,
                Phonenumber = "06332411324",
                StreetAddress = "Kossut utca 25"
            };

            //context.Employees.AddOrUpdate(Musa, Mira ,Ammar);

            context.Employees.Add(Mira);
            context.Employees.Add(Ammar);
            context.Employees.Add(Musa);



            var customer1 = new Customer { CustomerId = Guid.NewGuid(), Firstname = "Gregory", Lastname = "Morse", EmailAddress = "gregorymorse@gmail.com", Phonenumber = "0630658213", StreetAddress = "Andrassy Utca 21" };
            var customer2 = new Customer { CustomerId = Guid.NewGuid(), Firstname = "Alan", Lastname = "Chen", EmailAddress = "alanchen@gmail.com", Phonenumber = "06703212344", StreetAddress = "Dobb utca 53" };
            var customer3 = new Customer { CustomerId = Guid.NewGuid(), Firstname = "Srini", Lastname = "Venkat", EmailAddress = "srinivenkay@gmail.com", Phonenumber = "0670423214", StreetAddress = "Teresz korut 12" };
            var customer4 = new Customer { CustomerId = Guid.NewGuid(), Firstname = "Alexis", Lastname = "Blaise", EmailAddress = "alexisblaise@gmail.com", Phonenumber = "0630885632", StreetAddress = "Sas utca 29" };
            var customer5 = new Customer { CustomerId = Guid.NewGuid(), Firstname = "Ahmadali", Lastname = "Ghaffari", EmailAddress = "AhmadaliGhaffari@gmail.com", Phonenumber = "06704513265", StreetAddress = "Jokai Utca 42" };

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);
            context.Customers.Add(customer3);
            context.Customers.Add(customer4);
            context.Customers.Add(customer5);





            context.SaveChanges();




            var ticket1 = new Ticket
            {
                Subject = "OS problem. Will be formatted",
                AssignedTo = Musa,
                Customer = customer3,
                CustomerId = customer3.CustomerId,
                EstimatedWorkHour = 3,
                EstimatedCost = 20,
                IssueType = 2,
                ReceivedOn = DateTime.Parse("2017-05-08 14:10:22.510"),
                ApprovedOn = DateTime.Parse("2017-06-01 10:30:22.510"),
                CompletedOn = DateTime.UtcNow,
                isPaymentDone = false,
                DeliveryMethod = 1,
            };

            var ticket2 = new Ticket
            {
                Subject = "Mainboard crack. Need to purchase new one.",
                AssignedTo = Musa,
                Customer = customer5,
                CustomerId = customer5.CustomerId,
                EstimatedWorkHour = 5,
                EstimatedCost = 300,
                IssueType = 2,
                ReceivedOn = DateTime.Parse("2017-05-08 14:10:22.510"),
                ApprovedOn = DateTime.Parse("2017-06-05 09:45:22.510"),
                isPaymentDone = true,
                DeliveryMethod = 1,
            };


            var ticket3 = new Ticket
            {
                Subject = "OS problem. Will be formatted",
                AssignedTo = Ammar,
                Customer = customer4,
                CustomerId = customer4.CustomerId,
                EstimatedWorkHour = 3,
                EstimatedCost = 20,
                IssueType = 2,
                ReceivedOn = DateTime.UtcNow,
                isPaymentDone = false,
                DeliveryMethod = 1,
            };

            context.Tickets.AddOrUpdate(ticket1,ticket2,ticket3);

            context.SaveChanges();



            var feedback1 = new Feedback()
            {
                RelatedTicket = ticket1,
                Description = "Good work!",
                Rate = 8,
                RelatedTicketId = ticket1.TicketId,
            };

            var feedback2 = new Feedback()
            {
                RelatedTicket = ticket2,
                Description = "It was okay.",
                Rate = 5,
                RelatedTicketId = ticket1.TicketId,
            };

            var feedback3 = new Feedback()
            {
                RelatedTicket = ticket3,
                Description = "Didn't like it!",
                Rate = 3,
                RelatedTicketId = ticket1.TicketId,
            };


            context.Feedbacks.AddOrUpdate(feedback1, feedback2, feedback3);

            context.SaveChanges();

        }
    }
}
