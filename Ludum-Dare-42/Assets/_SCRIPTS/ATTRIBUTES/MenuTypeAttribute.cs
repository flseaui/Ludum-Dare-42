using System;
using DATA;

namespace ATTRIBUTES
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuTypeAttribute : Attribute
    {
        public MenuTypeAttribute(MenuTag menuType)
        {
            MenuType = menuType;
        }

        public MenuTag MenuType { get; set; }
    }
}