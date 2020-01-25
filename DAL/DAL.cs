using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class Db : IDb
    {
        private readonly Func<SqlConnection> _dbConnectionFactory;

        public Db(Func<SqlConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        public async Task<T> CommandAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<T>> command)
        {
            using (var connection = _dbConnectionFactory.Invoke())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await command(connection, transaction, Constants.CommandTimeout);

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                      // LoggerFactory.Equals.connection.Error(ex);
                        throw;
                    }
                }
            }
        }

        public async Task<T> GetAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<T>> command)
        {
            return await CommandAsync(command);
        }

        public async Task<IList<T>> SelectAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<IList<T>>> command)
        {
            return await CommandAsync(command);
        }

        public async Task ExecuteAsync(string sql, object parameters)
        {
            await CommandAsync(async (conn, trn, timeout) =>
            {
                await conn.ExecuteAsync(sql, parameters, trn, timeout);
                return 1;
            });
        }

            public async Task<T> GetAsync<T>(string sql, object parameters)
            {
                return await CommandAsync(async (conn, trn, timeout) =>
                {
                    T result = await conn.QuerySingleAsync<T>(sql, parameters, trn, timeout);
                    return result;
                });
            }

            public async Task<IList<T>> SelectAsync<T>(string sql, object parameters)
            {
                return await CommandAsync<IList<T>>(async (conn, trn, timeout) =>
                {
                    var result = (await conn.QueryAsync<T>(sql, parameters, trn, timeout)).ToList();
                    return result;
                });
            }
        }
    }
