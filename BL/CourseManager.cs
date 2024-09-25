using BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevate.PL;

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
                            Description = row.Description


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

        /*
         * This is for 
         * 
         * public static List<Course> Load(int? genreId = null)
        {
            List<Course> rows = new List<Course>();

            using (ElevateEntities dc = new ElevateEntities())
            {

                var row = (from p in dc.tblMovies
                           join mg in dc.tblMovieGenres on p.Id equals mg.Id
                           join mr in dc.tblRatings on p.RatingId equals mr.Id
                           join md in dc.tblDirectors on p.DirectorId equals md.Id
                           join mf in dc.tblFormats on p.FormatId equals mf.Id
                           where genreId == null || mg.GenreId == genreId
                           orderby p.Title
                           select new
                           {
                               p.Id,
                               p.Title,
                               p.Description,
                               p.Cost,
                               p.RatingId,
                               p.FormatId,
                               p.DirectorId,
                               p.InStkQty,
                               p.ImagePath,
                               RatingDescription = mr.Description,
                               FormatDescription = mf.Description,
                               DirectorFullName = md.FirstName + " " + md.LastName

                           }).ToList();

                if (row != null)
                {
                    foreach (var p in row)
                    {
                        rows.Add(new Movie
                        {

                            Id = p.Id,
                            Title = p.Title,
                            Description = p.Description,
                            Cost = p.Cost,
                            RatingId = p.RatingId,
                            FormatId = p.FormatId,
                            DirectorId = p.DirectorId,
                            InStkQty = p.InStkQty,
                            ImagePath = p.ImagePath,
                            RatingDescription = p.RatingDescription,
                            FormatDescription = p.FormatDescription,
                            DirectorFullName = p.DirectorFullName

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
        }*/
    }

}

