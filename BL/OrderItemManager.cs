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
    public class OrderItemManager
    {
        //public static int Insert(OrderItem orderItem, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;
        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            IDbContextTransaction dbContextTransaction = null;
        //            if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

        //            tblOrderItem row = new tblOrderItem();

        //            row.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(s => s.Id) + 1 : 1;

        //            row.OrderId = orderItem.OrderId;
        //            row.MovieId = orderItem.MovieId;
        //            row.Quantity = orderItem.Quantity;
        //            row.Cost = orderItem.Cost;

        //            // Backfill the ID
        //            orderItem.Id = row.Id;

        //            dc.tblOrderItems.Add(row);
        //            results = dc.SaveChanges();

        //            if (rollback) dbContextTransaction.Rollback();
        //        }

        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static List<OrderItem> Load()
        //{
        //    try
        //    {
        //        List<OrderItem> rows = new List<OrderItem>();

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            (from s in dc.tblOrderItems
        //             join m in dc.tblMovies on s.MovieId equals m.Id
        //             select new
        //             {
        //                 s.Id,
        //                 s.OrderId,
        //                 s.MovieId,
        //                 s.Quantity,
        //                 s.Cost,
        //                 m.Title,
        //                 m.ImagePath
        //             })
        //             .ToList()
        //             .ForEach(orderItem => rows.Add(new OrderItem
        //             {
        //                 Id = orderItem.Id,
        //                 OrderId = orderItem.OrderId,
        //                 MovieId = orderItem.MovieId,
        //                 Quantity = orderItem.Quantity,
        //                 Cost = orderItem.Cost,
        //                 Title = orderItem.Title,
        //                 ImagePath = orderItem.ImagePath
        //             }));
        //        }

        //        return rows;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static List<OrderItem> LoadByOrderId(int orderId)
        //{
        //    try
        //    {
        //        List<OrderItem> rows = new List<OrderItem>();

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            (from s in dc.tblOrderItems
        //             join m in dc.tblMovies on s.MovieId equals m.Id
        //             where s.OrderId == orderId
        //             select new
        //             {
        //                 s.Id,
        //                 s.OrderId,
        //                 s.MovieId,
        //                 s.Quantity,
        //                 s.Cost,
        //                 m.Title,
        //                 m.ImagePath
        //             })
        //             .ToList()
        //             .ForEach(orderItem => rows.Add(new OrderItem
        //             {
        //                 Id = orderItem.Id,
        //                 OrderId = orderItem.OrderId,
        //                 MovieId = orderItem.MovieId,
        //                 Quantity = orderItem.Quantity,
        //                 Cost = orderItem.Cost,
        //                 Title = orderItem.Title,
        //                 ImagePath = orderItem.ImagePath

        //             }));
        //        }

        //        return rows;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public static OrderItem LoadById(int id)
        //{
        //    try
        //    {

        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            tblOrderItem row = dc.tblOrderItems.Where(s => s.Id == id).FirstOrDefault();

        //            if (row != null)
        //            {
        //                return new OrderItem
        //                {
        //                    Id = row.Id,
        //                    OrderId = row.OrderId,
        //                    MovieId = row.MovieId,
        //                    Quantity = row.Quantity,
        //                    Cost = row.Cost
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
        //public static int Update(OrderItem orderItem, bool rollback = false)
        //{
        //    try
        //    {
        //        int results = 0;
        //        using (ElevateEntities dc = new ElevateEntities())
        //        {
        //            IDbContextTransaction dbContextTransaction = null;
        //            if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

        //            tblOrderItem row = dc.tblOrderItems.FirstOrDefault(s => s.Id == orderItem.Id);

        //            if (true)
        //            {
        //                row.OrderId = orderItem.OrderId;
        //                row.MovieId = orderItem.MovieId;
        //                row.Quantity = orderItem.Quantity;
        //                row.Cost = orderItem.Cost;

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

        //            tblOrderItem row = dc.tblOrderItems.FirstOrDefault(s => s.Id == id);

        //            if (true)
        //            {
        //                dc.tblOrderItems.Remove(row);
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
