using Elevate.BL.Models;
using Elevate.PL;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol.Plugins;

namespace Elevate.BL
{
    public class CustomerManager
    {
        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblCustomer row = new tblCustomer();

                    row.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(s => s.Id) + 1 : 1;

                    row.FirstName = customer.FirstName;
                    row.LastName = customer.LastName;
                    row.Email = customer.Email;
                    row.UserId = customer.UserId;

                    // Backfill the ID
                    customer.Id = row.Id;

                    dc.tblCustomers.Add(row);
                    results = dc.SaveChanges();

                    if (rollback) dbContextTransaction.Rollback();
                }

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Customer> Load()
        {
            try
            {
                List<Customer> rows = new List<Customer>();

                using (ElevateEntities dc = new ElevateEntities())
                {
                    (from s in dc.tblCustomers
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.UserId
                     })
                     .ToList()
                     .ForEach(customer => rows.Add(new Customer
                     {
                         Id = customer.Id,
                         FirstName = customer.FirstName,
                         LastName = customer.LastName,
                         UserId = customer.UserId
                     }));
                }

                return rows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool FindByUserId(int userid)
        {
            
            bool CustomerFound = false;

            try
            {
                

                using (ElevateEntities dc = new ElevateEntities())
                {
                    tblCustomer row = dc.tblCustomers.Where(s => s.UserId == userid).FirstOrDefault();

                    if (row != null)
                    {
                        CustomerFound = true;
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CustomerFound;

        }
        public static Customer LoadById(int id)
        {
            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {
                    tblCustomer row = dc.tblCustomers.Where(s => s.Id == id).FirstOrDefault();

                    if (row != null)
                    {
                        return new Customer
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Customer LoadByUserId(int userid)
        {
            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {
                    tblCustomer row = dc.tblCustomers.Where(s => s.UserId == userid).FirstOrDefault();

                    if (row != null)
                    {
                        return new Customer
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            UserId = row.UserId,
                            Email = row.Email
                            
                        };
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblCustomer row = dc.tblCustomers.FirstOrDefault(s => s.Id == customer.Id);

                    if (true)
                    {
                        row.FirstName = customer.FirstName;
                        row.LastName = customer.LastName;
                        row.UserId = customer.UserId;

                        results = dc.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblCustomer row = dc.tblCustomers.FirstOrDefault(s => s.Id == id);

                    if (true)
                    {
                        dc.tblCustomers.Remove(row);
                        results = dc.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
