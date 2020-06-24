using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UserIdentity.Core.Domain.Identity;
using UserIdentity.ViewModels.Authentication.Claims;

namespace UserIdentity.Services.Helper
{
    public static class HelperMethodService<T>
    {
        public static bool IsAnyNullOrEmpty(T myObject)
        {
            foreach (var pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType != typeof(string))
                    continue;
                var value = (string)pi.GetValue(myObject);
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
