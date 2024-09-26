﻿using BL.Models;
using Elevate.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Text;

namespace BL
{

    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot login with these credentials. Your IP Address has been logged.")
        {
        }
        public LoginFailureException(string message) : base(message)
        {
        }
    }

    public class UserManager
    {
        private static string GetHash(string password)
        {
            using (var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static int Insert(User user, Boolean rollback = false)
        {
            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {
                    IDbContextTransaction transaction = null;

                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser newuser = new tblUser();

                    newuser.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(u => u.Id) + 1 : 1;
                    newuser.Email = user.Email;
                    newuser.Password = GetHash(user.Password);
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;

                    dc.tblUsers.Add(newuser);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static void Seed()
        {
            using (ElevateEntities dc = new ElevateEntities())
            {
                if (!dc.tblUsers.Any())
                {
                    User user = new User() { Email = "user", FirstName = "John", LastName = "Snow", Password = "test" };
                    Insert(user);
                }
            }

        }

        public static int Update(User user, bool rollback = false)
        {

            return 0;

        }

        public static User LoadById(int id)
        {

            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {

                    tblUser row = dc.tblUsers.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {

                        User user = new User()
                        {

                            Id = row.Id,
                            Email = row.Email,
                            Password = row.Password,
                            FirstName = row.FirstName,
                            LastName = row.LastName

                        };

                        return user;

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


        public static List<User> Load()
        {
            List<User> rows = new List<User>();

            using (ElevateEntities dc = new ElevateEntities())
            {
                var tblUsers = (from p in dc.tblUsers
                                select p).ToList();

                foreach (tblUser p in tblUsers)
                {
                    rows.Add(new User
                    {
                        Id = p.Id,
                        Email = p.Email,
                        Password = p.Password,
                        FirstName = p.FirstName,
                        LastName = p.LastName

                    });
                }

                return rows;

            }

        }

        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (ElevateEntities dc = new ElevateEntities())
                        {
                            tblUser tbluser = dc.tblUsers.FirstOrDefault(u => u.Email == user.Email);
                            if (tbluser != null)
                            {
                                if (tbluser.Password == GetHash(user.Password))
                                {
                                    user.FirstName = tbluser.FirstName;
                                    user.LastName = tbluser.LastName;
                                    user.Email = tbluser.Email;
                                    user.Id = tbluser.Id;

                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new Exception("User Id could not be found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password was not set.");
                    }
                }
                else
                {
                    throw new Exception("User Id was not set.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static int DeleteAll()
        {

            try
            {
                using (ElevateEntities dc = new ElevateEntities())
                {
                    var users = dc.tblUsers.ToList();
                    dc.tblUsers.RemoveRange(users);

                    return dc.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

