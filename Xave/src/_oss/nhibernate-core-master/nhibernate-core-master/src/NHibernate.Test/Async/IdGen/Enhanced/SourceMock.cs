﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NHibernate.Id.Enhanced;

namespace NHibernate.Test.IdGen.Enhanced
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial class SourceMock : IAccessCallback
	{

		public Task<long> GetNextValueAsync(CancellationToken cancellationToken)
		{
			try
			{
				try
				{
					if (_timesCalled == 0)
					{
						InitValue();
						return Task.FromResult<long>(_val);
					}
					else
					{
						//return value.add( increment ).copy();
						_val += _increment;
						return Task.FromResult<long>(_val);
					}
				}
				finally
				{
					_timesCalled++;
				}
			}
			catch (System.Exception ex)
			{
				return Task.FromException<long>(ex);
			}
		}
	}
}