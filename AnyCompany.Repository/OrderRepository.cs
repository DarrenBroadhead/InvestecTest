using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.Model;
using AnyCompany.Repository.Extensions;
using AnyCompany.Repository.Interfaces;

namespace AnyCompany.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> LoadAll()
        {
            var orders = new List<Order>();

            using (SqlConnection connection = SqlExtension.GetConnection())
            {
                try
                {
                    connection.Open();

                    var sqlString = "SELECT * FROM Orders";

                    SqlDataReader reader = sqlString.GetReader(connection);

                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            OrderId = (int) reader["OrderId"],
                            CustomerId = (int) reader["CustomerId"],
                            Amount = (double) reader["Amount"],
                            VAT = (double) reader["VAT"]
                        };

                        orders.Add(order);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            return orders;
        }

        public Order Save(Order order)
        {
            using (SqlConnection connection = SqlExtension.GetConnection())
            {
                try
                {
                    connection.Open();

                    var sqlString = "INSERT INTO Orders VALUES (@CustomerId, @Amount, @VAT)";

                    var command = sqlString.GetCommand(connection);

                    command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    command.Parameters.AddWithValue("@Amount", order.Amount);
                    command.Parameters.AddWithValue("@VAT", order.VAT);

                    order.OrderId = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            return order;
        }
    }
}