using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Model;
using AnyCompany.Repository.Extensions;
using AnyCompany.Repository.Interfaces;

namespace AnyCompany.Repository
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId)
        {
            var customer = new Customer();

            using (SqlConnection connection = SqlExtension.GetConnection())
            {
                try
                {
                    connection.Open();

                    var sqlString = $"SELECT * FROM Customer WHERE CustomerId = {customerId}";

                    SqlDataReader reader = sqlString.GetReader(connection);

                    while (reader.Read())
                    {
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
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

            return customer;
        }

        public static List<Customer> LoadAll()
        {
            IOrderRepository orderRepository = new OrderRepository();

            List<Order> orders = orderRepository.LoadAll();

            var customers = new List<Customer>();

            using (SqlConnection connection = SqlExtension.GetConnection())
            {
                try
                {
                    connection.Open();

                    var sqlString = "SELECT * FROM Customer";

                    SqlDataReader reader = sqlString.GetReader(connection);

                    while (reader.Read())
                    {
                        var customer = new Customer
                        {
                            CustomerId = (int)reader["CustomerId"],
                            Name = reader["Name"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Country = reader["Country"].ToString(),
                            Orders = orders.Where(e => e.CustomerId == (int)reader["CustomerId"]).ToList()
                        };

                        customers.Add(customer);
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

            return customers;
        }

        public static Customer Save(Customer customer)
        {
            using (SqlConnection connection = SqlExtension.GetConnection())
            {
                try
                {
                    connection.Open();

                    var sqlString = "INSERT INTO Customer OUTPUT INSERTED.CustomerId VALUES (@Name, @Country, @DateOfBirth)";

                    var command = sqlString.GetCommand(connection);

                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Country", customer.Country);
                    command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);

                    customer.CustomerId= (int)command.ExecuteScalar();
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

            return customer;
        }
    }
}