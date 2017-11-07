﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Data;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;
using NHibernate.Impl;
using NHibernate.Exceptions;

namespace NHibernate.Dialect.Lock
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial class SelectLockingStrategy : ILockingStrategy
	{

		#region ILockingStrategy Members

		public async Task LockAsync(object id, object version, object obj, ISessionImplementor session, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ISessionFactoryImplementor factory = session.Factory;
			try
			{
				var st = await (session.Batcher.PrepareCommandAsync(CommandType.Text, sql, lockable.IdAndVersionSqlTypes, cancellationToken)).ConfigureAwait(false);
				DbDataReader rs = null;
				try
				{
					await (lockable.IdentifierType.NullSafeSetAsync(st, id, 0, session, cancellationToken)).ConfigureAwait(false);
					if (lockable.IsVersioned)
					{
						await (lockable.VersionType.NullSafeSetAsync(st, version, lockable.IdentifierType.GetColumnSpan(factory), session, cancellationToken)).ConfigureAwait(false);
					}

					rs = await (session.Batcher.ExecuteReaderAsync(st, cancellationToken)).ConfigureAwait(false);
					try
					{
						if (!await (rs.ReadAsync(cancellationToken)).ConfigureAwait(false))
						{
							if (factory.Statistics.IsStatisticsEnabled)
							{
								factory.StatisticsImplementor.OptimisticFailure(lockable.EntityName);
							}
							throw new StaleObjectStateException(lockable.EntityName, id);
						}
					}
					finally
					{
						rs.Close();
					}
				}
				finally
				{
					session.Batcher.CloseCommand(st, rs);
				}
			}
			catch (HibernateException)
			{
				// Do not call Convert on HibernateExceptions
				throw;
			}
			catch (Exception sqle)
			{
				var exceptionContext = new AdoExceptionContextInfo
				                       	{
				                       		SqlException = sqle,
				                       		Message = "could not lock: " + MessageHelper.InfoString(lockable, id, factory),
				                       		Sql = sql.ToString(),
				                       		EntityName = lockable.EntityName,
				                       		EntityId = id
				                       	};
				throw ADOExceptionHelper.Convert(session.Factory.SQLExceptionConverter, exceptionContext);
			}
		}

		#endregion
	}
}