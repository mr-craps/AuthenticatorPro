// Copyright (C) 2021 jmh
// SPDX-License-Identifier: GPL-3.0-only

using AuthenticatorPro.Shared.Data;
using System.Text.RegularExpressions;

namespace AuthenticatorPro.Droid.Shared.Data
{
    public class IconResolver : IIconResolver
    {
        public const string Default = "default";

        public static int GetService(string key, bool isDark)
        {
            if (isDark)
            {
                if (key == null)
                {
                    return IconMap.ServiceDark[Default];
                }

                if (IconMap.ServiceDark.ContainsKey(key))
                {
                    return IconMap.ServiceDark[key];
                }

                return IconMap.Service.ContainsKey(key) ? IconMap.Service[key] : IconMap.ServiceDark[Default];
            }

            if (key == null)
            {
                return IconMap.Service[Default];
            }

            return IconMap.Service.ContainsKey(key) ? IconMap.Service[key] : IconMap.Service[Default];
        }

        public string FindServiceKeyByName(string name)
        {
            static string Simplify(string input)
            {
                input = input.ToLower();
                input = Regex.Replace(input, @"[^a-z0-9]", "");
                return input.Trim();
            }

            var key = Simplify(name);

            if (IconMap.Service.ContainsKey(key))
            {
                return key;
            }

            var firstWordKey = Simplify(name.Split(new[] { ' ', '.' }, 2)[0]);

            return IconMap.Service.ContainsKey(firstWordKey)
                ? firstWordKey
                : null;
        }
    }
}