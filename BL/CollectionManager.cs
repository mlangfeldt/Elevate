using BL.Models;
using Elevate.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elevate.BL
{
    public class CollectionManager
    {
        public static int Insert(Collection collection, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {

                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCollection row = new tblCollection();

                    row.Id = dc.tblCollections.Any() ? dc.tblCollections.Max(g => g.Id) + 1 : 1;
                    row.CourseId = collection.CourseId;
                    row.UserId = collection.UserId;

                    // Backfill the id
                    collection.Id = row.Id;

                    dc.tblCollections.Add(row);

                    dc.SaveChanges();

                    results = collection.Id;

                    if (rollback) transaction.Rollback();

                    return results;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Collection collection, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {

                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCollection row = dc.tblCollections.FirstOrDefault(p => p.Id == collection.Id);

                    if (row != null)
                    {

                        row.CourseId = collection.CourseId;
                        row.UserId = collection.UserId;

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }

                    return results;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {

                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCollection row = dc.tblCollections.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {

                        dc.tblCollections.Remove(row);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }

                    return results;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*public static Collection LoadById(int id)
        {

            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {

                    tblCollection row = dc.tblCollections.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {
                        Collection collection = CustomerManager.LoadById(row.CustomerId);
                        string custfn = cust.LastName + ", " + cust.FirstName;

                        User user = UserManager.LoadById(row.UserId);

                        string username = user.UserId;
                        string userfullname = user.FullName;

                        Order order = new Order
                        {


                            Id = row.Id,
                            CustomerId = row.CustomerId,
                            CustomerName = custfn,
                            OrderDate = row.OrderDate,
                            UserId = row.UserId,
                            UserName = username,
                            UserFullName = userfullname,
                            ShipDate = row.ShipDate,

                        };
                        return order;

                    }
                    else
                    {

                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }*/

        public static List<Collection> Load()
        {
            try
            {
                List<Collection> list = new List<Collection>();

                using (ElevateEntities dc = new ElevateEntities())
                {
                    (from c in dc.tblCollections
                     select new
                     {
                         c.Id,
                         c.CourseId,
                         c.UserId
                     })
                     .ToList()
                     .ForEach(collection => list.Add(new Collection
                     {
                         Id = collection.Id,
                         CourseId = collection.CourseId,
                         UserId = collection.UserId
                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
