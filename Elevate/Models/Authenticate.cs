using Elevate.BL.Models;
using Elevate.UI.Extensions;

namespace Elevate.UI.Models
{
    public class Authenticate
    {
            public static bool IsAuthenticated(HttpContext context)
            {
                if (context.Session.GetObject<User>("user") != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

