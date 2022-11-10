using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace SuperHero
{
    public class derivedHero : SuperHero
    {
        public bool IsAcceccible()
        {
            SuperHero b = new SuperHero();
            bool minfo = b.GetType().GetMethod("MyMethod", BindingFlags.Instance | BindingFlags.NonPublic).IsFamily;


            return minfo;

        }
    }
}
