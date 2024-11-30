using System.Runtime.CompilerServices;

namespace CinnamonFlavour.Extensions
{
    public class ProjectileHitAdditionalData
    {
        public bool WillBrand { get; set; } = false;
    }

    public static class ProjectileHitExtension
    {
        public static readonly ConditionalWeakTable<ProjectileHit, ProjectileHitAdditionalData> data = new();

        public static ProjectileHitAdditionalData GetAdditionalData(this ProjectileHit hit)
        {
            return data.GetOrCreateValue(hit);
        }
    }
}