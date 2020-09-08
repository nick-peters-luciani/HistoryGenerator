using System.Linq;
using System.Reflection;

namespace HistoryGenerator.Utility
{
	public static class MiscUtil
	{
		public static void CopyProperties(object source, object dest)
        {
            var sourceProps = source.GetType().GetProperties().Where(x => x.CanRead).ToList();
            var destProps = dest.GetType().GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if(p.CanWrite)
                    {
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }
	}
}
