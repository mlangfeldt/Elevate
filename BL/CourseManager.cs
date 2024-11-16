using Elevate.BL.Models;
using Elevate.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elevate.BL
{
    public class CourseManager
    {

        public static int Insert(Course course, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {

                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Make a new tblCourse row
                    tblCourse row = new tblCourse();
                    row.Id = dc.tblCourses.Any() ? dc.tblCourses.Max(g => g.Id) + 1 : 1;
                    row.Name = course.Name;
                    row.Description = course.Description;
                    row.Cost = course.Cost;

                    // Backfill the id
                    course.Id = row.Id;

                    dc.tblCourses.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Course course, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {

                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Make a new tblMovie row
                    tblCourse row = dc.tblCourses.FirstOrDefault(p => p.Id == course.Id);

                    if (row != null)
                    {

                        row.Name = course.Name;
                        row.Description = course.Description;

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

                    tblCourse row = dc.tblCourses.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {

                        dc.tblCourses.Remove(row);

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

        public static Course LoadById(int id)
        {

            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {

                    tblCourse row = dc.tblCourses.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {

                        Course course = new Course
                        {

                            Id = row.Id,
                            Name = row.Name,
                            Description = row.Description,
                            Cost = row.Cost

                        };
                        return course;

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
        }

        public static List<Course> Load(int? genreId = null)
        {
            List<Course> rows = new List<Course>();

            using (ElevateEntities dc = new ElevateEntities())
            {

                var row = (from p in dc.tblCourses
                           select new
                           {
                               p.Id,
                               p.Name,
                               p.Description,
                               p.Cost
                           }).ToList();

                if (row != null)
                {
                    foreach (var p in row)
                    {
                        rows.Add(new Course
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Cost = p.Cost
                        });
                    }
                    return rows;
                }
                else
                {
                    throw new Exception("No movies found.");
                }
            }
        }

        public static List<Course> LoadByGenreId(int genreId)
        {

            return Load(genreId);
        }
    }

}

