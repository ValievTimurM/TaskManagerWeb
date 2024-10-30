using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Extension
{
	public static class EntityCollectionExtension
	{
		public static void SyncValues<TEntity>(this IList<TEntity> source, IList<TEntity> target)
		{
			if (source is null || target is null)
				return;

			var added = target.Except(source).ToArray();

			foreach (var el in added)
			{
				source.Add(el);
			}
		}
	}
}
