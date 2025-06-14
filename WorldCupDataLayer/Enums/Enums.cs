﻿using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Enums
{
    public enum Category
    {
        Men,
        Women
    }


    public static class CategoryHelper
    {
        /* 
            * This method allows for the Category's value to be read easily, 
            * allowing for seamless change of endpoints without changing the enum
        */
        public static string GetCategoryAsString(Category category)
            => category == Category.Men ? "men" : "women";
        public static Category GetCategory(string category)
            => category.ToLower() == "men" ? Category.Men : Category.Women;
    }
}
