using Elevate.BL.Models;

namespace Elevate.BL
{
    public class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Course course)
        {
            cart.Items.Add(course);
        }
        public static void Remove(ShoppingCart cart, Course course)
        {
            cart.Items.Remove(course);
        }
        public static void Checkout(ShoppingCart cart, int userId)
        {
            try
            {
                foreach (Course course in cart.Items)
                {

                    Collection collection = new Collection
                    {
                        CourseId = course.Id,
                        UserId = userId
                    };

                    CollectionManager.Insert(collection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during checkout: " + ex.Message, ex);
            }
        }


    }
}
