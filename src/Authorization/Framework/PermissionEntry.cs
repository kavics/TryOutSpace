using System.Linq;

namespace Framework
{
    public class PermissionEntry
    {
        public string Role { get; set; }
        public PermissionType[] Permissions { get; private set; } = new PermissionType[0];

        /// <summary>
        /// Adds a permission if there is not yet
        /// </summary>
        internal void AddPermission(PermissionType permission)
        {
            Permissions = Permissions.Union(new[] { permission }).Distinct().ToArray();
        }
        /// <summary>
        /// Removes a permission if there is.
        /// Returns true if the entry has become empty.
        /// </summary>
        internal bool RemovePermission(PermissionType permission)
        {
            Permissions = Permissions.Except(new[] { permission }).ToArray();
            return !Permissions.Any();
        }
    }
}
