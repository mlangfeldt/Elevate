using BL.Models;
using Elevate.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL
{
    public class OrderManager
    {
        public static void Remove(Order order, OrderItem orderItem)
        {
            order.OrderItems.Remove(orderItem);
        }
        public static double GetTotal(int orderId)
        {
            double total = 0;

            //List<OrderItem> orderItem = OrderItemManager.LoadByOrderId(orderId);

            //foreach (OrderItem item in orderItem)
            //{
            //    total += item.Cost;
            //}
            //total *= 1.055;

            return total;
        }
        //public static int Insert(Order order, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            IDbContextTransaction dbContextTransaction = null;
        //            if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

        //            tblOrder row = new tblOrder();
        //            row.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(s => s.Id) + 1 : 1;
        //            row.CustomerId = order.CustomerId;
        //            row.OrderDate = order.OrderDate;
        //            row.UserId = order.UserId;
        //            row.ShipDate = order.ShipDate;
        //            // Backfill the ID
        //            order.Id = row.Id;
        //            if (order.OrderItems != null)
        //            {
        //                foreach (var orderItem in order.OrderItems)
        //                {
        //                    orderItem.OrderId = order.Id;
        //                    results += OrderItemManager.Insert(orderItem, rollback);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("No item in Order, try again with an item.");
        //            }
        //            dc.tblOrders.Add(row);
        //            results += dc.SaveChanges();
        //            if (rollback) dbContextTransaction.Rollback();
        //        }
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static List<Order> Load(int? customerId = null)
        //{
        //    try
        //    {
        //        List<Order> rows = new List<Order>();

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            (from s in dc.tblOrders
        //             join c in dc.tblCustomers on s.CustomerId equals c.Id
        //             join oi in dc.tblOrderItems on s.Id equals oi.OrderId
        //             where c.Id == customerId || customerId == null
        //             select new
        //             {
        //                 s.Id,
        //                 s.CustomerId,
        //                 s.OrderDate,
        //                 s.UserId,
        //                 s.ShipDate,
        //                 CustomerName = c.FirstName + " " + c.LastName
        //             })
        //             .Distinct().ToList()
        //             .ForEach(order => rows.Add(new Order
        //             {
        //                 Id = order.Id,
        //                 CustomerId = order.CustomerId,
        //                 OrderDate = order.OrderDate,
        //                 UserId = order.UserId,
        //                 ShipDate = order.ShipDate,
        //                 CustomerName = order.CustomerName,
        //                 Total = GetTotal(order.Id)
        //             }));
        //        }
        //        return rows;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public static Order LoadById(int id)
        //{
        //    try
        //    {

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            var row = (from s in dc.tblOrders
        //                       join oi in dc.tblOrderItems on s.Id equals oi.OrderId
        //                       join c in dc.tblCustomers on s.CustomerId equals c.Id
        //                       where s.Id == id
        //                       select new
        //                       {
        //                           s.Id,
        //                           s.CustomerId,
        //                           s.OrderDate,
        //                           s.UserId,
        //                           s.ShipDate,
        //                           c.FirstName,
        //                           c.LastName,
        //                           oi.Cost,
        //                           OrderItems = OrderItemManager.LoadByOrderId(s.Id)

        //                       }).FirstOrDefault();

        //            if (row != null)
        //            {
        //                return new Order
        //                {
        //                    Id = row.Id,
        //                    CustomerId = row.CustomerId,
        //                    OrderDate = row.OrderDate,
        //                    UserId = row.UserId,
        //                    ShipDate = row.ShipDate,
        //                    CustomerName = row.LastName + ", " + row.FirstName,
        //                    OrderItems = OrderItemManager.LoadByOrderId(row.Id),
        //                    Subtotal = row.Cost,
        //                    Total = GetTotal(id)
        //                };
        //            }
        //            else
        //            {
        //                throw new Exception("Row does not exist.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public static int Update(Order order, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;
        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            IDbContextTransaction dbContextTransaction = null;
        //            if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

        //            tblOrder row = dc.tblOrders.FirstOrDefault(s => s.Id == order.Id);

        //            if (true)
        //            {
        //                row.CustomerId = order.CustomerId;
        //                row.OrderDate = order.OrderDate;
        //                row.UserId = order.UserId;
        //                row.ShipDate = order.ShipDate;

        //                results = dc.SaveChanges();

        //                if (rollback) dbContextTransaction.Rollback();
        //            }
        //            else
        //            {
        //                throw new Exception("Row does not exist.");
        //            }
        //        }
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public static int Delete(int id, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;
        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            IDbContextTransaction dbContextTransaction = null;
        //            if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

        //            tblOrder row = dc.tblOrders.FirstOrDefault(s => s.Id == id);

        //            if (true)
        //            {
        //                dc.tblOrders.Remove(row);
        //                results = dc.SaveChanges();

        //                if (rollback) dbContextTransaction.Rollback();
        //            }
        //            else
        //            {
        //                throw new Exception("Row does not exist.");
        //            }
        //        }

        //        return results;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
