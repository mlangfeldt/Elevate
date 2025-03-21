﻿using Elevate.BL.Models;
using Elevate.PL;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Elevate.BL
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

        public static int Insert(User user, bool rollback = false)
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
                    newuser.EmailConfirmed = 0;
                    newuser.ConfirmationCode = Guid.NewGuid().ToString("N");

                    dc.tblUsers.Add(newuser);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    //string body = "Welcome to Elevate!";
                    //body += "<br /><br />Please click the following link to activate your account";
                    //body += $"<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Context.Request.Scheme, Context.Request.Authority, newuser.ConfirmationCode) + "'>Click here to activate your account.</a>";
                                        
                    //EmailService.SendConfirmationCodeEmail(body, newuser.Email, newuser.ConfirmationCode);

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
                    User user = new User()
                    {
                        Email = "user",
                        FirstName = "joe",
                        LastName = "snow",
                        Password = "test"
                    };
                    Insert(user);


                    user = new User()
                    {
                        Email = "user1",
                        FirstName = "steve",
                        LastName = "snow",
                        Password = "test123"
                    };
                    Insert(user);
                    user = new User()
                    {
                        Email = "user2",
                        FirstName = "jamal",
                        LastName = "snow",
                        Password = "test12345"
                    };
                    Insert(user);
                }
            }
        }

        public static int Update(User user, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (ElevateEntities dc = new ElevateEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row we are trying to update
                    tblUser entity = dc.tblUsers.FirstOrDefault(g => g.Id == user.Id);

                    if (entity != null)
                    {
                        entity.FirstName = user.FirstName;
                        entity.LastName = user.LastName;
                        entity.Email = user.Email;
                        //entity.Password = GetHash(user.Password);
                        entity.EmailConfirmed = user.EmailConfirmed;
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static bool EmailExists(string email)
        {

            bool FoundEmail = false;

            try
                {

                    using (ElevateEntities dc = new ElevateEntities())
                    {

                        tblUser row = dc.tblUsers.FirstOrDefault(p => p.Email == email);

                        if (row != null)
                        {

                            FoundEmail = true;
                            
                        }
                        else
                        {

                            FoundEmail = false;
                           
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            return FoundEmail;

        }


        public static User LoadByConfirmationCode(string code)
        {

            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {

                    tblUser row = dc.tblUsers.FirstOrDefault(p => p.ConfirmationCode == code);

                    if (row != null)
                    {

                        User user = new User()
                        {

                            Id = row.Id,
                            Email = row.Email,
                            Password = row.Password,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            EmailConfirmed = row.EmailConfirmed,
                            ConfirmationCode = row.ConfirmationCode


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
                            LastName = row.LastName,
                            EmailConfirmed = row.EmailConfirmed,
                            ConfirmationCode = row.ConfirmationCode


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
        public static User LoadByEmail(string email)
        {

            try
            {

                using (ElevateEntities dc = new ElevateEntities())
                {

                    tblUser row = dc.tblUsers.FirstOrDefault(p => p.Email == email);

                    if (row != null)
                    {

                        User user = new User()
                        {

                            Id = row.Id,
                            Email = row.Email,
                            Password = row.Password,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            EmailConfirmed = row.EmailConfirmed,
                            ConfirmationCode = row.ConfirmationCode

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
            try
            {
                List<User> list = new List<User>();

                using (ElevateEntities dc = new ElevateEntities())
                {
                    (from u in dc.tblUsers
                     select new
                     {
                         u.Id,
                         u.FirstName,
                         u.LastName,
                         u.Email,
                         u.Password,
                         u.EmailConfirmed
                     })
                     .ToList()
                     .ForEach(user => list.Add(new User
                     {
                         Id = user.Id,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Email = user.Email,
                         Password = user.Password,
                         EmailConfirmed = user.EmailConfirmed
                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
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
                                        user.Id = tbluser.Id;
                                        user.Email = tbluser.Email;
                                        user.FirstName = tbluser.FirstName;
                                        user.LastName = tbluser.LastName;
                                        user.EmailConfirmed = tbluser.EmailConfirmed;
                                        return true;
                                    }
                                    else
                                    {
                                        throw new LoginFailureException();
                                    }
                                
                            }
                            else
                            {
                                throw new Exception("Email could not be found.");
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
                    throw new Exception("Email was not set.");
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

        public static string GenerateResetCode(string email)
        {
            using (ElevateEntities dc = new ElevateEntities())
            {
                tblUser user = dc.tblUsers.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    Random rnd = new Random();
                    int code = rnd.Next(100000, 999999);

                    user.ResetCode = code.ToString();
                    user.ResetCodeExpiration = DateTime.Now.AddMinutes(15);

                    dc.SaveChanges();

                    EmailService.SendResetCodeEmail(user.Email, code.ToString());

                    return user.ResetCode;
                }
                return "null";
            }
        }



        public static bool ValidateResetCode(string email, string code)
        {
            using (ElevateEntities dc = new ElevateEntities())
            {

                tblUser user = dc.tblUsers.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    if (user.ResetCode == code && user.ResetCodeExpiration > DateTime.Now)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void UpdatePassword(string email, string newPassword)
        {
            using (ElevateEntities dc = new ElevateEntities())
            {
                tblUser user = dc.tblUsers.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    user.Password = GetHash(newPassword);
                    user.ResetCode = null;
                    user.ResetCodeExpiration = null;

                    dc.SaveChanges();
                }
                else
                {
                    throw new Exception("User Not Found");
                }
            }
        }


    }
}

