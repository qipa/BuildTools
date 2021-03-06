﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ApiCheck.Description
{
    public class ApiListing
    {
        public string AssemblyIdentity { get; set; }

        public IList<TypeDescriptor> Types { get; } = new List<TypeDescriptor>();

        [JsonIgnore]
        public IEnumerable<Func<MemberInfo, bool>> SourceFilters { get; set; }

        public TypeDescriptor FindType(string name)
        {
            foreach (var type in Types)
            {
                if (string.Equals(name, type.Name, StringComparison.Ordinal))
                {
                    return type;
                }
            }

            return null;
        }

        public ApiElement FindElement(string typeId, string memberId)
        {
            var type = Types.FirstOrDefault(t => t.Id == typeId);
            if (type == null)
            {
                return null;
            }

            if (memberId == null)
            {
                return type;
            }

            return type.FindMember(memberId);
        }
    }
}
