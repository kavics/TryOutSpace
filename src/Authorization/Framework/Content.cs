using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public class Content
    {
        public string Name { get; set; }
        private List<PermissionEntry> Permissions { get; } = new List<PermissionEntry>();

        public void AddPermission(string role, PermissionType permission)
        {
            var existing = Permissions.FirstOrDefault(x => x.Role == role);
            if (null == existing)
                Permissions.Add(existing = new PermissionEntry { Role = role });

            existing.AddPermission(permission);
        }
        public void RemovePermission(string role, PermissionType permission)
        {
            var existing = Permissions.FirstOrDefault(x => x.Role == role);
            if (null == existing)
                return;

            if (existing.RemovePermission(permission))
                Permissions.Remove(existing);
        }
        public bool HasPermission(string role, params PermissionType[] permissions)
        {
            var existing = Permissions.FirstOrDefault(x => x.Role == role);
            if (null == existing)
                return false;

            foreach (var permission in permissions)
                if (!existing.Permissions.Any(x => x == permission))
                    return false;
            return true;
        }
    }
}
