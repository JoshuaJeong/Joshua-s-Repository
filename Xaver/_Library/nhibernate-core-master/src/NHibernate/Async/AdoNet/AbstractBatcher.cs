﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;

using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Exceptions;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Util;
using NHibernate.AdoNet.Util;

namespace NHibernate.AdoNet
{
	using System.Threading.Tasks;
	/// <content>
	/// Contains generated async methods
	/// </content>
	public abstract partial class AbstractBatcher : IBatcher
	{

		/// <summary>
		/// Prepares the <see cref="DbCommand"/> for execution in the database.
		/// </summary>
		/// <remarks>
		/// This takes care of hooking the <see cref="DbCommand"/> up to an <see cref="DbConnection"/>
		/// and <see cref="DbTransaction"/> if one exists.  It will call <c>Prepare</c> if the Driver
		/// supports preparing commands.
		/// </remarks>
		protected async Task PrepareAsync(DbCommand cmd, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			try
			{
				var sessionConnection = await (_connectionManager.GetConnectionAsync(cancellationToken)).ConfigureAwait(false);

				if (cmd.Connection != null)
				{
					// make sure the commands connection is the same as the Sessions connection
					// these can be different when the session is disconnected and then reconnected
					if (cmd.Connection != sessionConnection)
					{
						cmd.Connection = sessionConnection;
					}
				}
				else
				{
					cmd.Connection = sessionConnection;
				}

				_connectionManager.Transaction.Enlist(cmd);
				Driver.PrepareCommand(cmd);
			}
			catch (InvalidOperationException ioe)
			{
				throw new ADOException("While preparing " + cmd.CommandText + " an error occurred", ioe);
			}
		}

		public virtual async Task<DbCommand> PrepareBatchCommandAsync(CommandType type, SqlString sql, SqlType[] parameterTypes, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (sql.Equals(_batchCommandSql) && ArrayHelper.ArrayEquals(parameterTypes, _batchCommandParameterTypes))
			{
				if (Log.IsDebugEnabled)
				{
					Log.Debug("reusing command " + _batchCommand.CommandText);
				}
			}
			else
			{
				_batchCommand = await (PrepareCommandAsync(type, sql, parameterTypes, cancellationToken)).ConfigureAwait(false); // calls ExecuteBatch()
				_batchCommandSql = sql;
				_batchCommandParameterTypes = parameterTypes;
			}

			return _batchCommand;
		}

		public async Task<DbCommand> PrepareCommandAsync(CommandType type, SqlString sql, SqlType[] parameterTypes, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (OnPreparedCommandAsync(cancellationToken)).ConfigureAwait(false);

			// do not actually prepare the Command here - instead just generate it because
			// if the command is associated with an ADO.NET Transaction/Connection while
			// another open one Command is doing something then an exception will be 
			// thrown.
			return Generate(type, sql, parameterTypes);
		}

		protected virtual Task OnPreparedCommandAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			// a new DbCommand is being prepared and a new (potential) batch
			// started - so execute the current batch of commands.
			return ExecuteBatchAsync(cancellationToken);
		}

		public async Task<int> ExecuteNonQueryAsync(DbCommand cmd, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (CheckReadersAsync(cancellationToken)).ConfigureAwait(false);
			LogCommand(cmd);
			await (PrepareAsync(cmd, cancellationToken)).ConfigureAwait(false);
			Stopwatch duration = null;
			if (Log.IsDebugEnabled)
				duration = Stopwatch.StartNew();
			try
			{
				return await (cmd.ExecuteNonQueryAsync(cancellationToken)).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				e.Data["actual-sql-query"] = cmd.CommandText;
				Log.Error("Could not execute command: " + cmd.CommandText, e);
				throw;
			}
			finally
			{
				if (Log.IsDebugEnabled && duration != null)
					Log.DebugFormat("ExecuteNonQuery took {0} ms", duration.ElapsedMilliseconds);
			}
		}

		public virtual async Task<DbDataReader> ExecuteReaderAsync(DbCommand cmd, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (CheckReadersAsync(cancellationToken)).ConfigureAwait(false);
			LogCommand(cmd);
			await (PrepareAsync(cmd, cancellationToken)).ConfigureAwait(false);
			Stopwatch duration = null;
			if (Log.IsDebugEnabled)
				duration = Stopwatch.StartNew();
			DbDataReader reader = null;
			try
			{
				reader = await (cmd.ExecuteReaderAsync(cancellationToken)).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				e.Data["actual-sql-query"] = cmd.CommandText;
				Log.Error("Could not execute query: " + cmd.CommandText, e);
				throw;
			}
			finally
			{
				if (Log.IsDebugEnabled && duration != null && reader != null)
				{
					Log.DebugFormat("ExecuteReader took {0} ms", duration.ElapsedMilliseconds);
					_readersDuration[reader] = duration;
				}
			}

			if (!_factory.ConnectionProvider.Driver.SupportsMultipleOpenReaders)
			{
				reader = await (NHybridDataReader.CreateAsync(reader, cancellationToken)).ConfigureAwait(false);
			}

			_readersToClose.Add(reader);
			LogOpenReader();
			return reader;
		}

		/// <summary>
		/// Ensures that the Driver's rules for Multiple Open DataReaders are being followed.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		protected async Task CheckReadersAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			// early exit because we don't need to move an open DbDataReader into memory
			// since the Driver supports mult open readers.
			if (_factory.ConnectionProvider.Driver.SupportsMultipleOpenReaders)
			{
				return;
			}

			foreach (NHybridDataReader reader in _readersToClose)
			{
				await (reader.ReadIntoMemoryAsync(cancellationToken)).ConfigureAwait(false);
			}
		}

		public async Task ExecuteBatchAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			// if there is currently a command that a batch is
			// being built for then execute it
			if (_batchCommand != null)
			{
				var ps = _batchCommand;
				InvalidateBatchCommand();
				try
				{
					await (ExecuteBatchWithTimingAsync(ps, cancellationToken)).ConfigureAwait(false);
				}
				finally
				{
					CloseCommand(ps, null);
				}
			}
		}

		protected async Task ExecuteBatchWithTimingAsync(DbCommand ps, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stopwatch duration = null;
			if (Log.IsDebugEnabled)
				duration = Stopwatch.StartNew();
			var countBeforeExecutingBatch = CountOfStatementsInCurrentBatch;
			await (DoExecuteBatchAsync(ps, cancellationToken)).ConfigureAwait(false);
			if (Log.IsDebugEnabled && duration != null)
				Log.DebugFormat("ExecuteBatch for {0} statements took {1} ms",
					countBeforeExecutingBatch,
					duration.ElapsedMilliseconds);
		}

		protected abstract Task DoExecuteBatchAsync(DbCommand ps, CancellationToken cancellationToken);

		/// <summary>
		/// Adds the expected row count into the batch.
		/// </summary>
		/// <param name="expectation">The number of rows expected to be affected by the query.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <remarks>
		/// If Batching is not supported, then this is when the Command should be executed.  If Batching
		/// is supported then it should hold of on executing the batch until explicitly told to.
		/// </remarks>
		public abstract Task AddToBatchAsync(IExpectation expectation, CancellationToken cancellationToken);
	}
}