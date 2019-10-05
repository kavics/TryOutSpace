using Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public class Host
    {
        Dictionary<string, Content> _contents = new Dictionary<string, Content>();

        public void SaveContent(Content content)
        {
            _contents[content.Name] = content;
        }
        public Content GetContent(string name)
        {
            return _contents.TryGetValue(name, out var existing) ? existing : null;
        }

        public string Invoke(string route, string controller, string method, string resource)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var names = asms.Select(x => x.GetName().Name).OrderBy(x => x).ToArray();
            var a = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == route);
            if (a == null)
                return "Unknown route";
            var t = a.GetTypes().FirstOrDefault(x => x.Name == controller);
            if (t == null)
                return "Unknown controller";
            var m = t.GetMethods().FirstOrDefault(x => x.Name == method);
            if (m == null)
                return "Unknown method";
            var c = GetContent(resource);
            if (c == null)
                return "Unknown content";

            var attrs = m.GetCustomAttributes(typeof(RequiredPermissionsAttribute), true);
            foreach (RequiredPermissionsAttribute attr in attrs)
            {
                var permissions = attr.Permissions
                    .Split(',')
                    .Select(x => PermissionType.GetByName(x.Trim()))
                    .ToArray();
                if (!c.HasPermission(attr.Role, permissions))
                    return "Access denied.";
            }

            var instance = Activator.CreateInstance(t);
            var result = m.Invoke(instance, new[] { c }) as string;
            return result;
        }
    }
}
