﻿using System;
using System.ComponentModel;

namespace FaysConcept.WMS.Common.Functions
{
    public static class EnumFunctions
    {
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) return null;
            var memberInfo = value.GetType().GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            //return (T)attributes[0]; T ye cast edip geri gönderiyoruz.
            return (T)attributes[0];
        }
        public static string ToName(this Enum value)
        {
            if (value == null) return null;
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}