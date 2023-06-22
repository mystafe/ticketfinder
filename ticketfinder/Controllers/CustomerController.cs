﻿//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ticketfinder.Context;
//using ticketfinder.Models.DTO.CustomerDTO;
//using ticketfinder.Models.ORM;

//namespace ticketfinder.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {


//        TicketFinderContext context;
//        public CustomerController()
//        {
//            context = new TicketFinderContext();
//        }

//        [HttpGet]
//        public IActionResult  GetCustomers()
//        {
//            List<ResponseCustomerDTO> customers = context.Customers.Include(c => c.Address).Select(c => new ResponseCustomerDTO()
//            {
//                Id=c.Id,
//                Fullname =c.Middlename!=null? c.Firstname+" "+ c.Middlename+" "+c.Lastname:c.Firstname+" "+c.Lastname,
//                 Email=c.Email,
//                  FullAdress=c.Address!=null? c.Address.FullAdress:"",
//                   CityName= c.Address!=null &&c.Address.City!=null?c.Address.City.Name:"",
//                     Username=c.Username,
//                     Password=c.Password,
//                      Phone=c.Phone
               
//            }).ToList();


//            if (customers.Count>0)
//            {
//                return Ok(customers);
//            }
//            return NotFound();



      

//        }


//        [HttpGet("{id}")]
//        public IActionResult GetCustomer(int id)
//        {
//            Customer customer = context.Customers.Include(c=>c.Address).AsQueryable().FirstOrDefault(c => c.Id == id);
//            if (customer != null)
//            {
//                ResponseCustomerDTO result = new ResponseCustomerDTO()
//                {
//                    Fullname = customer.Middlename != null ? customer.Firstname + " " + customer.Middlename + " " + customer.Lastname : customer.Firstname + " " + customer.Lastname,
//                    Username = customer.Username,
//                    Password = customer.Password,
//                    Phone = customer.Phone,
//                    Email = customer.Email,
//                    FullAdress = customer.Address != null ? customer.Address.FullAdress : "",
//                    CityName = customer.Address != null && customer.Address.City != null ? customer.Address.City.Name : "",
//                };
//                return Ok(result);
//            }
//            else return NotFound();
//        }

//        [HttpPost]
//        public IActionResult Post(CreateCustomerDTO model)
//        {
//            Customer customer = new Customer();
//            customer.Firstname = model.Firstname;
//            customer.Lastname = model.Lastname;
//            customer.Email = model.Email;
//            customer.Phone = model.Phone;
//            Address address=new Address() { FullAdress=model.FullAdress,CityId=model.CityId };

         
//            if (model != null)
//            {
//                context.Addresses.Add(address);
//                context.SaveChanges();

//                int? addressid=context.Addresses.AsQueryable().FirstOrDefault(address => address.FullAdress == model.FullAdress).Id;
//                if (addressid != null)  customer.AddressId = (int)addressid;
//                context.Customers.Add(customer);

//                context.SaveChanges();

//                return Ok(customer);
//            }
//            else
//            {
//                return BadRequest();
//            }

//        }



//    }
//}